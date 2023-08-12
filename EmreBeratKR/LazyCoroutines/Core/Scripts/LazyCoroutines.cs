using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EmreBeratKR.LazyCoroutines
{
    public static partial class LazyCoroutines
    {
        private const string RunnerObjectName = "[LazyCoroutineRunner]";


        private static readonly Dictionary<uint, Coroutine> Coroutines = new();


#if UNITY_EDITOR

        private static readonly Dictionary<uint, Routine> Routines = new();

#endif


        private static Runner ms_Runner;
        private static uint ms_NextID;
        
        
        public static Coroutine StartCoroutine(IEnumerator routine, string name = "UNNAMED ROUTINE")
        {
            var id = GetAndIncrementID();
            var coroutine = GetRunner().StartCoroutine(routine);
            
            Coroutines.Add(id, coroutine);

#if UNITY_EDITOR
            
            Routines.Add(id, new Routine
            {
                name = name,
                coroutine = coroutine
            });
            
#endif
            
            return coroutine;
        }

        public static void StopCoroutine(Coroutine coroutine)
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


        private static void Kill(Coroutine coroutine)
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

        private static uint GetCoroutineID(Coroutine coroutine)
        {
            foreach (var (id, _coroutine) in Coroutines)
            {
                if (_coroutine == coroutine) return id;
            }

            return uint.MaxValue;
        }

        private static Coroutine GetCoroutineByID(uint id)
        {
            return Coroutines[id];
        }
        
        private static void Invoke(Action action, Coroutine coroutine)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                Kill(coroutine);
                LogWarning($"The following error caught inside a routine. The routine is being killed!\n{e}");
            }
        }

        private static void LogWarning(string msg)
        {
            Debug.LogWarning($"[LazyCoroutineWarning]: {msg}");
        }

        private static Runner GetRunner()
        {
            if (ms_Runner) return ms_Runner;
            
            ms_Runner = new GameObject(RunnerObjectName).AddComponent<Runner>();
            Object.DontDestroyOnLoad(ms_Runner);

            return ms_Runner;
        }


        public class Runner : MonoBehaviour {}
        
#if UNITY_EDITOR
        
        [Serializable]
        public class Routine
        {
            public string name;
            public Coroutine coroutine;
        }
        
#endif   
    }
}