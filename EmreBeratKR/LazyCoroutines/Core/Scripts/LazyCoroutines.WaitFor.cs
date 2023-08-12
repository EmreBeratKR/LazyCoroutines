using System;
using System.Collections;
using UnityEngine;

namespace EmreBeratKR.LazyCoroutines
{
    public static partial class LazyCoroutines
    {
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
    }
}