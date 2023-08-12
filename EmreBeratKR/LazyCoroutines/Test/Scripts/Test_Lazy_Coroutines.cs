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
        }
    }
}