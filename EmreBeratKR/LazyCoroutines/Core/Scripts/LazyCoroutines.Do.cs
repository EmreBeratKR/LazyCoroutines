namespace EmreBeratKR.LazyCoroutines
{
    public static partial class LazyCoroutines
    {
        /// <summary>
        /// Executes the specified action every frame.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="tag">An optional tag to associate with the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="DoEveryFixedUpdate(System.Action, string)"/>
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
        
        /// <summary>
        /// Executes the specified action every FixedUpdate.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="tag">An optional tag to associate with the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="DoEveryFrame(System.Action, string)"/>
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
        
        /// <summary>
        /// Executes the specified action every specified number of seconds.
        /// </summary>
        /// <param name="seconds">The time interval in seconds.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="tag">An optional tag to associate with the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="DoEverySeconds(System.Func{float}, System.Action, string)"/>
        public static UnityEngine.Coroutine DoEverySeconds(float seconds, System.Action action, string tag = "")
        {
            return DoEverySeconds(() => seconds, action, tag);
        }
        
        /// <summary>
        /// Executes the specified action every specified number of seconds.
        /// Useful whenever the duration is changing.
        /// </summary>
        /// <param name="secondsGetter">A function that returns the time interval in seconds.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="tag">An optional tag to associate with the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="DoEverySeconds(float, System.Action, string)"/>
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
        
        /// <summary>
        /// Executes the specified action while the specified condition is true.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="tag">An optional tag to associate with the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="DoUntil"/>
        public static UnityEngine.Coroutine DoWhile(System.Func<bool> condition, System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, nameof(DoWhile));


            System.Collections.IEnumerator Routine()
            {
                yield return null;
                
                while (condition.Invoke())
                {
                    yield return null;
                    Invoke(action, GetCoroutineByID(id));
                }

                yield return null;
                
                Kill(id);
            }
        }
        
        /// <summary>
        /// Executes the specified action until the specified condition is true.
        /// </summary>
        /// <param name="condition">The condition to check.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="tag">An optional tag to associate with the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="DoWhile"/>
        public static UnityEngine.Coroutine DoUntil(System.Func<bool> condition, System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, nameof(DoUntil));


            System.Collections.IEnumerator Routine()
            {
                yield return null;
                
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