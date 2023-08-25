namespace EmreBeratKR.LazyCoroutines
{
    public static partial class LazyCoroutines
    {
        /// <summary>
        /// Waits for the next frame and then invokes the provided action.
        /// </summary>
        /// <param name="action">The action to invoke after waiting.</param>
        /// <param name="tag">Optional tag for the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="WaitForFrames"/>
        /// <seealso cref="WaitForFixedUpdate"/>
        /// <seealso cref="WaitForFixedUpdates"/>
        /// <seealso cref="WaitForEndOfFrame"/>
        public static UnityEngine.Coroutine WaitForFrame(System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, nameof(WaitForFrame));
            
            
            System.Collections.IEnumerator Routine()
            {
                yield return null;
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }

        
        /// <summary>
        /// Waits for a specified number of frames and then invokes the provided action.
        /// </summary>
        /// <param name="count">The number of frames to wait for.</param>
        /// <param name="action">The action to invoke after waiting.</param>
        /// <param name="tag">Optional tag for the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="WaitForFrame"/>
        /// <seealso cref="WaitForFixedUpdate"/>
        /// <seealso cref="WaitForFixedUpdates"/>
        /// <seealso cref="WaitForEndOfFrame"/>
        public static UnityEngine.Coroutine WaitForFrames(int count, System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, $"{nameof(WaitForFrames)} ({count}/{count} frames)");
            
            
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
        
        
        /// <summary>
        /// Waits for a FixedUpdate and then invokes the provided action.
        /// </summary>
        /// <param name="action">The action to invoke after waiting.</param>
        /// <param name="tag">Optional tag for the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="WaitForFrame"/>
        /// <seealso cref="WaitForFrames"/>
        /// <seealso cref="WaitForFixedUpdates"/>
        /// <seealso cref="WaitForEndOfFrame"/>
        public static UnityEngine.Coroutine WaitForFixedUpdate(System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, nameof(WaitForFixedUpdate));
            
            
            System.Collections.IEnumerator Routine()
            {
                yield return WaitForFixedUpdateObj;
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
        
        /// <summary>
        /// Waits for a specified number of FixedUpdates and then invokes the provided action.
        /// </summary>
        /// <param name="count">The number of FixedUpdates to wait for.</param>
        /// <param name="action">The action to invoke after waiting.</param>
        /// <param name="tag">Optional tag for the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="WaitForFrame"/>
        /// <seealso cref="WaitForFrames"/>
        /// <seealso cref="WaitForFixedUpdate"/>
        /// <seealso cref="WaitForEndOfFrame"/>
        public static UnityEngine.Coroutine WaitForFixedUpdates(int count, System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, $"{nameof(WaitForFixedUpdates)} ({count}/{count} fixed updates)");
            
            
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
        
        /// <summary>
        /// Waits until the end of the current frame and then invokes the provided action.
        /// </summary>
        /// <param name="action">The action to invoke after waiting.</param>
        /// <param name="tag">Optional tag for the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="WaitForFrame"/>
        /// <seealso cref="WaitForFrames"/>
        /// <seealso cref="WaitForFixedUpdate"/>
        /// <seealso cref="WaitForFixedUpdates"/>
        public static UnityEngine.Coroutine WaitForEndOfFrame(System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, nameof(WaitForEndOfFrame));
            
            
            System.Collections.IEnumerator Routine()
            {
                yield return WaitForEndOfFrameObj;
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
        
        
        /// <summary>
        /// Waits for a specified amount of time in seconds and then invokes the provided action.
        /// </summary>
        /// <param name="delay">The time in seconds to wait for.</param>
        /// <param name="action">The action to invoke after waiting.</param>
        /// <param name="tag">Optional tag for the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="WaitForSecondsRealtime"/>
        public static UnityEngine.Coroutine WaitForSeconds(float delay, System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, $"{nameof(WaitForSeconds)} ({delay}/{delay} seconds)");
            
            
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
        
        /// <summary>
        /// Waits for a specified amount of real time in seconds and then invokes the provided action.
        /// </summary>
        /// <param name="delay">The real time in seconds to wait for.</param>
        /// <param name="action">The action to invoke after waiting.</param>
        /// <param name="tag">Optional tag for the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="WaitForSeconds"/>
        public static UnityEngine.Coroutine WaitForSecondsRealtime(float delay, System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, $"{nameof(WaitForSecondsRealtime)} ({delay}/{delay} seconds)");
            
            
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
        
        /// <summary>
        /// Waits while a given condition is true and then invokes the provided action.
        /// </summary>
        /// <param name="condition">The condition to wait for.</param>
        /// <param name="action">The action to invoke after waiting.</param>
        /// <param name="tag">Optional tag for the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="WaitUntil"/>
        public static UnityEngine.Coroutine WaitWhile(System.Func<bool> condition, System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, nameof(WaitWhile));
            
            
            System.Collections.IEnumerator Routine()
            {
                yield return null;
                
                while (condition.Invoke()) yield return null;
                
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
        
        /// <summary>
        /// Waits until a given condition is true and then invokes the provided action.
        /// </summary>
        /// <param name="condition">The condition to wait for.</param>
        /// <param name="action">The action to invoke after waiting.</param>
        /// <param name="tag">Optional tag for the coroutine.</param>
        /// <returns>The started coroutine.</returns>
        /// <seealso cref="DoWhile"/>
        public static UnityEngine.Coroutine WaitUntil(System.Func<bool> condition, System.Action action, string tag = "")
        {
            var id = ms_NextID;
            return StartCoroutine(Routine(), tag, nameof(WaitUntil));
            
            
            System.Collections.IEnumerator Routine()
            {
                yield return null;
                
                while (!condition.Invoke()) yield return null;
                
                Invoke(action, GetCoroutineByID(id));
                Kill(id);
            }
        }
    }
}