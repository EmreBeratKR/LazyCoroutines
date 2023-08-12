using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EmreBeratKR.LazyCoroutines
{
    public static class LazyCoroutines
    {
        private const string RunnerObjectName = "[LazyCoroutineRunner]";


        private static LazyCoroutineRunner ms_Runner;
        
        
        public static Coroutine StartCoroutine(IEnumerator routine)
        {
            return GetRunner().StartCoroutine(routine);
        }

        public static void StopCoroutine(Coroutine coroutine)
        {
            if (coroutine == null) return;
            
            GetRunner().StopCoroutine(coroutine);
        }

        public static void StopAllCoroutines()
        {
            GetRunner().StopAllCoroutines();
        }

        public static Coroutine WaitForFrame(Action action)
        {
            return StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                yield return null;
                action?.Invoke();
            }
        }

        public static Coroutine WaitForFrames(int count, Action action)
        {
            return StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                for (var i = 0; i < count; i++)
                {
                    yield return null;
                }
                
                action?.Invoke();
            }
        }
        
        public static Coroutine WaitForSeconds(float delay, Action action)
        {
            return StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                var startTime = Time.time;

                while (Time.time - startTime < delay) yield return null;
                
                action?.Invoke();
            }
        }
        
        public static Coroutine WaitForSecondsRealtime(float delay, Action action)
        {
            return StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                var startTime = Time.unscaledTime;

                while (Time.unscaledTime - startTime < delay) yield return null;
                
                action?.Invoke();
            }
        }

        public static Coroutine DoEverySeconds(float seconds, Action action)
        {
            return DoEverySeconds(() => seconds, action);
        }
        
        public static Coroutine DoEverySeconds(Func<float> secondsGetter, Action action)
        {
            return StartCoroutine(Routine());
            

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
                    
                    action?.Invoke();

                    yield return null;
                }
            }
        }

        public static Coroutine DoWhile(Func<bool> condition, Action action)
        {
            return StartCoroutine(Routine());


            IEnumerator Routine()
            {
                while (condition.Invoke())
                {
                    action?.Invoke();
                    yield return null;
                }
            }
        }
        
        public static Coroutine DoUntil(Func<bool> condition, Action action)
        {
            return StartCoroutine(Routine());


            IEnumerator Routine()
            {
                while (!condition.Invoke())
                {
                    action?.Invoke();
                    yield return null;
                }
            }
        }

        
        private static LazyCoroutineRunner GetRunner()
        {
            if (ms_Runner) return ms_Runner;
            
            ms_Runner = new GameObject(RunnerObjectName).AddComponent<LazyCoroutineRunner>();
            Object.DontDestroyOnLoad(ms_Runner);

            return ms_Runner;
        }
        
        
        private class LazyCoroutineRunner : MonoBehaviour {}
    }
}