namespace EmreBeratKR.LazyCoroutines
{
    public static partial class LazyCoroutines
    {
        private const string RunnerObjectName = "[LazyCoroutineRunner]";


        private static readonly UnityEngine.WaitForFixedUpdate WaitForFixedUpdateObj = new();
        private static readonly UnityEngine.WaitForEndOfFrame WaitForEndOfFrameObj = new();
        private static readonly System.Collections.Generic.Dictionary<uint, UnityEngine.Coroutine> Coroutines = new();


#if UNITY_EDITOR

        private static readonly System.Collections.Generic.Dictionary<uint, Routine> Routines = new();

#endif


        private static Runner ms_Runner;
        private static uint ms_NextID;
        
        
        public static UnityEngine.Coroutine StartCoroutine(System.Collections.IEnumerator routine, string tag = "", string name = "UNNAMED ROUTINE")
        {
            var id = GetAndIncrementID();
            var coroutine = GetRunner().StartCoroutine(routine);
            
            Coroutines.Add(id, coroutine);

#if UNITY_EDITOR
            
            Routines.Add(id, new Routine
            {
                name = name,
                coroutine = coroutine,
                tag = tag
            });
            
#endif
            
            return coroutine;
        }

        public static void StopCoroutine(UnityEngine.Coroutine coroutine)
        {
            if (coroutine == null) return;
            
            Kill(coroutine);
        }

        public static void StopAllCoroutines()
        {
            GetRunner().StopAllCoroutines();
            Coroutines.Clear();

#if UNITY_EDITOR
            
            Routines.Clear();
            
#endif
        }


        private static void Kill(UnityEngine.Coroutine coroutine)
        {
            var id = GetCoroutineID(coroutine);
            Kill(id);
        }

        private static void Kill(uint id)
        {
            Coroutines.Remove(id, out var coroutine); 
            
#if UNITY_EDITOR

            Routines.Remove(id);

#endif
            
            GetRunner().StopCoroutine(coroutine);
        }
        
        private static uint GetAndIncrementID()
        {
            var id = ms_NextID;
            ms_NextID += 1;
            return id;
        }

        private static uint GetCoroutineID(UnityEngine.Coroutine coroutine)
        {
            foreach (var (id, _coroutine) in Coroutines)
            {
                if (_coroutine == coroutine) return id;
            }

            return uint.MaxValue;
        }

        private static UnityEngine.Coroutine GetCoroutineByID(uint id)
        {
            return Coroutines[id];
        }
        
        private static void Invoke(System.Action action, UnityEngine.Coroutine coroutine)
        {
            try
            {
                action?.Invoke();
            }
            catch (System.Exception e)
            {
                Kill(coroutine);
                LogWarning($"The following error caught inside a routine. The routine is being killed!\n{e}");
            }
        }

        private static void LogWarning(string msg)
        {
            UnityEngine.Debug.LogWarning($"[LazyCoroutineWarning]: {msg}");
        }

        private static Runner GetRunner()
        {
            if (ms_Runner) return ms_Runner;
            
            ms_Runner = new UnityEngine.GameObject(RunnerObjectName).AddComponent<Runner>();
            UnityEngine.Object.DontDestroyOnLoad(ms_Runner);

            return ms_Runner;
        }


        public class Runner : UnityEngine.MonoBehaviour {}
        
#if UNITY_EDITOR
        
        [System.Serializable]
        public class Routine
        {
            public string name;
            public string tag;
            public UnityEngine.Coroutine coroutine;


            public bool IsTagged => !string.IsNullOrEmpty(tag);
        }


        private static void RenameRoutine(uint id, string name)
        {
            var routine = Routines[id];
            routine.name = name;
        }
        
        [UnityEditor.MenuItem("EmreBeratKR/Lazy Coroutines/Debugger")]
        private static void SelectRunner()
        {
            if (!UnityEngine.Application.isPlaying)
            {
                LogWarning("You must enter play mode to access to debugger!");
                return;
            }

            var runner = GetRunner();
            
            UnityEditor.Selection.objects = new UnityEngine.Object[] {runner};
            UnityEditor.EditorGUIUtility.PingObject(runner);
        }
        
#endif   
    }
}