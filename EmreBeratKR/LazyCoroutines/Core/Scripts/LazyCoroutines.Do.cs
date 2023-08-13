namespace EmreBeratKR.LazyCoroutines
{
    public static partial class LazyCoroutines
    {
        public static UnityEngine.Coroutine DoEverySeconds(float seconds, System.Action action)
        {
            return DoEverySeconds(() => seconds, action);
        }
        
        public static UnityEngine.Coroutine DoEverySeconds(System.Func<float> secondsGetter, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), $"{nameof(DoEverySeconds)} ({secondsGetter.Invoke()} seconds) (0 iterations)");
            

            System.Collections.IEnumerator Routine()
            {
                var startTime = UnityEngine.Time.time;
                var interval = secondsGetter.Invoke();
#if UNITY_EDITOR
                var iteration = 0;
#endif
                
                while (true)
                {
                    if (UnityEngine.Time.time - startTime < interval)
                    {
                        yield return null;
                        continue;
                    }

                    startTime = UnityEngine.Time.time;
                    interval = secondsGetter.Invoke();
                    
                    Invoke(action, GetCoroutineByID(id));
#if UNITY_EDITOR
                    iteration += 1;
                    RenameRoutine(id, $"{nameof(DoEverySeconds)} ({secondsGetter.Invoke()} seconds) ({iteration} iterations)");
#endif

                    yield return null;
                }
            }
        }

        public static UnityEngine.Coroutine DoWhile(System.Func<bool> condition, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), nameof(DoWhile));


            System.Collections.IEnumerator Routine()
            {
                while (condition.Invoke())
                {
                    yield return null;
                    Invoke(action, GetCoroutineByID(id));
                }

                yield return null;
                
                Kill(id);
            }
        }
        
        public static UnityEngine.Coroutine DoUntil(System.Func<bool> condition, System.Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), nameof(DoUntil));


            System.Collections.IEnumerator Routine()
            {
                while (!condition.Invoke())
                {
                    yield return null;
                    Invoke(action, GetCoroutineByID(id));
                }
                
                yield return null;
                
                Kill(id);
            }
        }
    }
}