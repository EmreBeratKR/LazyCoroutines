using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EmreBeratKR.LazyCoroutines
{
    public static class LazyCoroutines
    {
        private const string RunnerObjectName = "[LazyCoroutineRunner]";


        private static readonly Dictionary<uint, Coroutine> Coroutines = new();


        private static LazyCoroutineRunner ms_Runner;
        private static uint ms_NextID;
        
        
        public static Coroutine StartCoroutine(IEnumerator routine)
        {
            var id = GetID();
            var coroutine = GetRunner().StartCoroutine(routine);
            
            Coroutines.Add(id, coroutine);
            
            return coroutine;
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
            var id = ms_NextID;
            return StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                yield return null;
                Invoke(action, GetCoroutineByID(id));
            }
        }

        public static Coroutine WaitForFrames(int count, Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                for (var i = 0; i < count; i++)
                {
                    yield return null;
                }
                
                Invoke(action, GetCoroutineByID(id));
            }
        }
        
        public static Coroutine WaitForSeconds(float delay, Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                var startTime = Time.time;

                while (Time.time - startTime < delay) yield return null;
                
                Invoke(action, GetCoroutineByID(id));
            }
        }
        
        public static Coroutine WaitForSecondsRealtime(float delay, Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                var startTime = Time.unscaledTime;

                while (Time.unscaledTime - startTime < delay) yield return null;
                
                Invoke(action, GetCoroutineByID(id));
            }
        }

        public static Coroutine DoEverySeconds(float seconds, Action action)
        {
            return DoEverySeconds(() => seconds, action);
        }
        
        public static Coroutine DoEverySeconds(Func<float> secondsGetter, Action action)
        {
            var id = ms_NextID;
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
                    
                    Invoke(action, GetCoroutineByID(id));

                    yield return null;
                }
            }
        }

        public static Coroutine DoWhile(Func<bool> condition, Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine());


            IEnumerator Routine()
            {
                while (condition.Invoke())
                {
                    Invoke(action, GetCoroutineByID(id));
                    yield return null;
                }
            }
        }
        
        public static Coroutine DoUntil(Func<bool> condition, Action action)
        {
            var id = ms_NextID;
            return StartCoroutine(Routine());


            IEnumerator Routine()
            {
                while (!condition.Invoke())
                {
                    Invoke(action, GetCoroutineByID(id));
                    yield return null;
                }
            }
        }


        private static void Kill(Coroutine coroutine)
        {
            var id = GetCoroutineID(coroutine);
            Kill(id);
        }

        private static void Kill(uint id)
        {
            Coroutines.Remove(id, out var coroutine); 
            StopCoroutine(coroutine);
        }
        
        private static uint GetID()
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