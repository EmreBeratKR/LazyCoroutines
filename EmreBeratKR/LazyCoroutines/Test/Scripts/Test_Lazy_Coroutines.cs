using System.Collections;
using UnityEngine;

namespace EmreBeratKR.LazyCoroutines.Test
{
    public class Test_Lazy_Coroutines : MonoBehaviour
    {
        private void Start()
        {
            var go = new GameObject();

            LazyCoroutines.DoEverySeconds(1f, () =>
            {
                Debug.Log(go.name);
            });

            /*GameObject a = null;

            LazyCoroutines.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                yield return null;
                Debug.Log(a.name);
            }*/

            /*GameObject a = null;

            LazyCoroutines.WaitForFrame(() =>
            {
                Debug.Log(a.name);
            });*/

            var i = 0;

            LazyCoroutines.DoUntil(() => i > 5000, () => { i++;});
            LazyCoroutines.DoWhile(() => i < 3000, () => {});
            LazyCoroutines.WaitUntil(() => i > 4000, () => { });
            LazyCoroutines.WaitWhile(() => i < 3500, () => { });
            LazyCoroutines.WaitForFrame(() => { });
            LazyCoroutines.WaitForEndOfFrame(() => { });
            LazyCoroutines.WaitForFixedUpdate(() => { });
            LazyCoroutines.WaitForFrames(5000, () => { });
            LazyCoroutines.WaitForFixedUpdates(500, () => { });
            LazyCoroutines.WaitForSeconds(10, () => { });
            LazyCoroutines.WaitForSecondsRealtime(15, () => { }, "i am tagged!");

            LazyCoroutines.DoEveryFrame(() => Debug.Log("update"));
            LazyCoroutines.DoEveryFixedUpdate(() => Debug.Log("fixed update"), "yes sir");
        }
    }
}