using UnityEngine;

namespace EmreBeratKR.LazyCoroutines
{
    public static partial class LazyCoroutines
    {
        public static Coroutine WaitForFrame(System.Action action)
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

        public static Coroutine WaitForFrames(int count, System.Action action)
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
        
        public static Coroutine WaitForSeconds(float delay, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), $"{nameof(WaitForSeconds)} ({delay} seconds)");
            
            
            System.Collections.IEnumerator Routine()
            {
                var startTime = Time.time;

                while (Time.time - startTime < delay) yield return null;
                
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
        
        public static Coroutine WaitForSecondsRealtime(float delay, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), $"{nameof(WaitForSecondsRealtime)} ({delay} seconds)");
            
            
            System.Collections.IEnumerator Routine()
            {
                var startTime = Time.unscaledTime;

                while (Time.unscaledTime - startTime < delay) yield return null;
                
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
    }
}