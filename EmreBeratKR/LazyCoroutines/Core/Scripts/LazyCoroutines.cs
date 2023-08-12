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


        private static LazyCoroutineRunner ms_Runner;
        private static uint ms_NextID;
        
        
        public static Coroutine StartCoroutine(IEnumerator routine)
        {
            var id = GetAndIncrementID();
            var coroutine = GetRunner().StartCoroutine(routine);
            
            Coroutines.Add(id, coroutine);
            
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
        }


        private static void Kill(Coroutine coroutine)
        {
            var id = GetCoroutineID(coroutine);
            Kill(id);
        }

        private static void Kill(uint id)
        {
            Coroutines.Remove(id, out var coroutine); 
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