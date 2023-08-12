using System;
using System.Collections;
using UnityEngine;

namespace EmreBeratKR.LazyCoroutines
{
    public static partial class LazyCoroutines
    {
        public static Coroutine DoEverySeconds(float seconds, Action action)
        {
            return DoEverySeconds(() => seconds, action);
        }
        
        public static Coroutine DoEverySeconds(Func<float> secondsGetter, Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), $"{nameof(DoEverySeconds)} ({secondsGetter.Invoke()} seconds)");
            

            IEnumerator Routine()
            {
                var startTime = Time.time;
                var interval = secondsGetter.Invoke();
                
                while (true)
                {
                    if (Time.time - startTime < interval)
                    {
                        yield return null;
                        continue;
                    }

                    startTime = Time.time;
                    interval = secondsGetter.Invoke();
                    
                    Invoke(action, GetCoroutineByID(id));

                    yield return null;
                }
            }
        }

        public static Coroutine DoWhile(Func<bool> condition, Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), nameof(DoWhile));


            IEnumerator Routine()
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
        
        public static Coroutine DoUntil(Func<bool> condition, Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), nameof(DoUntil));


            IEnumerator Routine()
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