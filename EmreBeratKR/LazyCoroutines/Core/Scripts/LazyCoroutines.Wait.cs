namespace EmreBeratKR.LazyCoroutines
{
    public static partial class LazyCoroutines
    {
        public static UnityEngine.Coroutine WaitForFrame(System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), nameof(WaitForFrame));
            
            
            System.Collections.IEnumerator Routine()
            {
                yield return null;
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }

        public static UnityEngine.Coroutine WaitForFrames(int count, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), $"{nameof(WaitForFrames)} ({count} frames)");
            
            
            System.Collections.IEnumerator Routine()
            {
                for (var i = 0; i < count; i++)
                {
                    yield return null;
                }
                
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
        
        public static UnityEngine.Coroutine WaitForSeconds(float delay, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), $"{nameof(WaitForSeconds)} ({delay} seconds)");
            
            
            System.Collections.IEnumerator Routine()
            {
                var startTime = UnityEngine.Time.time;

                while (UnityEngine.Time.time - startTime < delay) yield return null;
                
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
        
        public static UnityEngine.Coroutine WaitForSecondsRealtime(float delay, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), $"{nameof(WaitForSecondsRealtime)} ({delay} seconds)");
            
            
            System.Collections.IEnumerator Routine()
            {
                var startTime = UnityEngine.Time.unscaledTime;

                while (UnityEngine.Time.unscaledTime - startTime < delay) yield return null;
                
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
        
        public static UnityEngine.Coroutine WaitWhile(System.Func<bool> condition, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), nameof(WaitWhile));
            
            
            System.Collections.IEnumerator Routine()
            {
                while (condition.Invoke()) yield return null;
                
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
        
        public static UnityEngine.Coroutine WaitUntil(System.Func<bool> condition, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), nameof(WaitUntil));
            
            
            System.Collections.IEnumerator Routine()
            {
                while (!condition.Invoke()) yield return null;
                
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
    }
}