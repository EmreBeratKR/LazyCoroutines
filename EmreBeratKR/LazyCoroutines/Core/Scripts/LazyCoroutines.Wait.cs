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
        
        public static UnityEngine.Coroutine WaitForEndOfFrame(System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), nameof(WaitForEndOfFrame));
            
            
            System.Collections.IEnumerator Routine()
            {
                yield return WaitForEndOfFrameObj;
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }

        public static UnityEngine.Coroutine WaitForFrames(int count, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), $"{nameof(WaitForFrames)} ({count}/{count} frames)");
            
            
            System.Collections.IEnumerator Routine()
            {
                for (var i = 0; i < count; i++)
                {
                    yield return null;
#if UNITY_EDITOR
                    RenameRoutine(id, $"{nameof(WaitForFrames)} ({count - i - 1}/{count} frames)");
#endif
                }
                
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
        
        public static UnityEngine.Coroutine WaitForFixedUpdate(System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), nameof(WaitForFixedUpdate));
            
            
            System.Collections.IEnumerator Routine()
            {
                yield return WaitForFixedUpdateObj;
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
        
        public static UnityEngine.Coroutine WaitForFixedUpdates(int count, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), $"{nameof(WaitForFixedUpdates)} ({count}/{count} fixed updates)");
            
            
            System.Collections.IEnumerator Routine()
            {
                for (var i = 0; i < count; i++)
                {
                    yield return WaitForFixedUpdateObj;
#if UNITY_EDITOR
                    RenameRoutine(id, $"{nameof(WaitForFixedUpdates)} ({count - i - 1}/{count} fixed updates)");
#endif
                }
                
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
        
        public static UnityEngine.Coroutine WaitForSeconds(float delay, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), $"{nameof(WaitForSeconds)} ({delay}/{delay} seconds)");
            
            
            System.Collections.IEnumerator Routine()
            {
                var startTime = UnityEngine.Time.time;

                while (UnityEngine.Time.time - startTime < delay)
                {
                    yield return null;
#if UNITY_EDITOR
                    var elapsedTime = UnityEngine.Time.time - startTime;
                    var timeLeft = delay - elapsedTime;
                    RenameRoutine(id, $"{nameof(WaitForSeconds)} ({timeLeft:0.00}/{delay} seconds)");
#endif
                }
                
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
        
        public static UnityEngine.Coroutine WaitForSecondsRealtime(float delay, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), $"{nameof(WaitForSecondsRealtime)} ({delay}/{delay} seconds)");
            
            
            System.Collections.IEnumerator Routine()
            {
                var startTime = UnityEngine.Time.unscaledTime;

                while (UnityEngine.Time.unscaledTime - startTime < delay)
                {
                    yield return null;
#if UNITY_EDITOR
                    var elapsedTime = UnityEngine.Time.unscaledTime - startTime;
                    var timeLeft = delay - elapsedTime;
                    RenameRoutine(id, $"{nameof(WaitForSecondsRealtime)} ({timeLeft:0.00}/{delay} seconds)");
#endif
                }
                
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