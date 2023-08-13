namespace EmreBeratKR.LazyCoroutines
{
    public static partial class LazyCoroutines
    {
        public static UnityEngine.Coroutine DoEveryFrame(System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, nameof(DoEveryFrame));


            System.Collections.IEnumerator Routine()
            {
                while (true)
                {
                    yield return null;
                    Invoke(action, GetCoroutineByID(id));
                }
            }
        }
        
        public static UnityEngine.Coroutine DoEveryFixedUpdate(System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, nameof(DoEveryFixedUpdate));


            System.Collections.IEnumerator Routine()
            {
                while (true)
                {
                    yield return WaitForFixedUpdateObj;
                    Invoke(action, GetCoroutineByID(id));
                }
            }
        }
        
        public static UnityEngine.Coroutine DoEverySeconds(float seconds, System.Action action, string tag = "")
        {
            return DoEverySeconds(() => seconds, action, tag);
        }
        
        public static UnityEngine.Coroutine DoEverySeconds(System.Func<float> secondsGetter, System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, $"{nameof(DoEverySeconds)} ({secondsGetter.Invoke()} seconds) (0 iterations)");
            

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

        public static UnityEngine.Coroutine DoWhile(System.Func<bool> condition, System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, nameof(DoWhile));


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
        
        public static UnityEngine.Coroutine DoUntil(System.Func<bool> condition, System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, nameof(DoUntil));


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