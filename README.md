# LazyCoroutines

## Links

[<img src="https://makaka.org/wp-content/uploads/2022/02/new-unity-asset-store-badge-full.png" width="200" />][assetstore]

[<img src="https://images.squarespace-cdn.com/content/v1/5bbc502865019fe7b132cdc0/1619022573920-HXS3VG6DNLBH6NYX2963/discord-button.png" width="200" />][discord]

[<img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" width="200" />][coffee]

## Navigate

- <a href="#about">About</a>
- <a href="#how-to-install">How to Install</a>
- <a href="#debugger">Debugger</a>
- <a href="#api">API</a>

[assetstore]: https://assetstore.unity.com/
[discord]: https://discord.gg/mKG9vkyEDX
[coffee]: https://www.buymeacoffee.com/emreberat
[releases]: https://github.com/EmreBeratKR/LazyCoroutines/releases
[download]: https://github.com/EmreBeratKR/LazyCoroutines/releases

## About

An Open-source Extension Library for Unity Coroutines

## How to Install

- Import it from [Asset Store][assetstore]
- Import [LazyCoroutines.unitypackage][releases] from **releases**
- Clone or [Download][download] this repository and move to your Unity project's **Assets** folder
- Add from git URL for UPM (Unity Package Manager)
```
https://github.com/EmreBeratKR/LazyCoroutines.git
```

## Debugger

### How to Access to Debugger Panel

<img src ="https://github.com/EmreBeratKR/ImageContainer/blob/main/LazyCoroutines/debugger_instruction.png" />

- From Unity Menu Item's ```Tools```>```EmreBeratKR```>```Lazy Coroutines```>```Debugger```

### Debugger Panel

<img src="https://github.com/EmreBeratKR/ImageContainer/blob/main/LazyCoroutines/debugger.png" />

## API

- <a href="#startcoroutine">StartCoroutine</a>
- <a href="#stopcoroutine">StopCoroutine</a>
- <a href="#stopallcoroutines">StopAllCoroutines</a>
### Do Prefix
- <a href="#doeveryframe">DoEveryFrame</a>
- <a href="#doeveryfixedupdate">DoEveryFixedUpdate</a>
- <a href="#doeveryseconds">DoEverySeconds</a>
- <a href="#doeveryseconds-with-func">DoEverySeconds (with Func)</a>
- <a href="#dowhile">DoWhile</a>
- <a href="#dountil">DoUntil</a>
### Wait Prefix
- <a href="#waitforframe">WaitForFrame</a>
- <a href="#waitforframes">WaitForFrames</a>
- <a href="#waitforfixedupdate">WaitForFixedUpdate</a>
- <a href="#waitforfixedupdates">WaitForFixedUpdates</a>
- <a href="#waitforendofframe">WaitForEndOfFrame</a>
- <a href="#waitforseconds">WaitForSeconds</a>
- <a href="#waitforsecondsrealtime">WaitForSecondsRealtime</a>
- <a href="#waitwhile">WaitWhile</a>
- <a href="#waituntil">WaitUntil</a>

### StartCoroutine

- Starts a new coroutine and associates it with a unique ID.
- Returns the started coroutine.

```cs
using System.Collections;
using UnityEngine;
using EmreBeratKR.LazyCoroutines;


public class Test : MonoBehaviour
{
    private void Start()
    { 
        LazyCoroutines.StartCoroutine(Routine());

        IEnumerator Routine()
        {
            yield return null;
            Debug.Log("some routine");
        }
    }
}
```

### StopCoroutine

- Stops the specified coroutine.

```cs
using System.Collections;
using UnityEngine;
using EmreBeratKR.LazyCoroutines;


public class Test : MonoBehaviour
{
    private void Start()
    { 
        var coroutine = LazyCoroutines.StartCoroutine(Routine());

        IEnumerator Routine()
        {
            yield return null;
            Debug.Log("some routine");
        }
        
        LazyCoroutines.StopCoroutine(coroutine);
    }
}
```

### StopAllCoroutines

- Stops all running coroutines.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.StopAllCoroutines();
    }
}
```

## Do Prefix

### DoEveryFrame

- Executes the specified action every frame.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.DoEveryFrame(() =>
        {
            Debug.Log("Log every frame!");
        });
    }
}
```

### DoEveryFixedUpdate

- Executes the specified action every FixedUpdate.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.DoEveryFixedUpdate(() =>
        {
            Debug.Log("Log every FixedUpdate!");
        });
    }
}
```

### DoEverySeconds

- Executes the specified action every specified number of seconds.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.DoEverySeconds(0.5f, () =>
        {
            Debug.Log("Log every 0.5 seconds!");
        });
    }
}
```

### DoEverySeconds (with Func)

- Executes the specified action every specified number of seconds.
- Useful whenever the duration is changing.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        var duration = 0.75f;
        
        LazyCoroutines.DoEverySeconds(() => duration, () =>
        {
            // change duration randomly
            duration = Random.Range(0.1f, 1f);
            Debug.Log("Log every [duration] seconds!");
        });
    }
}
```

### DoWhile

- Executes the specified action while the specified condition is true.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.DoWhile(() => Input.GetKey(KeyCode.Space), () =>
        {
            Debug.Log("Log while space key is pressed!");
        });
    }
}
```

### DoUntil

- Executes the specified action until the specified condition is true.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.DoUntil(() => Input.GetKeyDown(KeyCode.Space), () =>
        {
            Debug.Log("Log until space key is pressed!");
        });
    }
}
```

## Wait Prefix

### WaitForFrame

- Waits for the next frame and then invokes the provided action.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.WaitForFrame(() =>
        {
            Debug.Log("Waited for a frame!");
        });
    }
}
```

### WaitForFrames

- Waits for a specified number of frames and then invokes the provided action.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.WaitForFrames(10, () =>
        {
            Debug.Log("Waited for 10 frames!");
        });
    }
}
```

### WaitForFixedUpdate

- Waits for a FixedUpdate and then invokes the provided action.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.WaitForFixedUpdate(() =>
        {
            Debug.Log("Waited for a FixedUpdate!");
        });
    }
}
```

### WaitForFixedUpdates

- Waits for a specified number of FixedUpdates and then invokes the provided action.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.WaitForFixedUpdates(5, () =>
        {
            Debug.Log("Waited for 5 FixedUpdates!");
        });
    }
}
```

### WaitForEndOfFrame

- Waits until the end of the current frame and then invokes the provided action.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.WaitForEndOfFrame(() =>
        {
            Debug.Log("Waited for 5 end of the frame!");
        });
    }
}
```

### WaitForSeconds

- Waits for a specified amount of time in seconds and then invokes the provided action.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.WaitForSeconds(3.67f, () =>
        {
            Debug.Log("Waited for 3.67 seconds");
        });
    }
}
```

### WaitForSecondsRealtime

- Waits for a specified amount of real time in seconds and then invokes the provided action.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private void Start()
    {
        LazyCoroutines.WaitForSecondsRealtime(10, () =>
        {
            Debug.Log("Waited for 10 seconds");
        });
    }
}
```

### WaitWhile

- Waits while a given condition is true and then invokes the provided action.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private bool m_IsLevelComplete;
    
    private void Start()
    {
        LazyCoroutines.WaitWhile(() => !m_IsLevelComplete, () =>
        {
            Debug.Log("Wait while level is not completed yet!");
        });
    }
}
```

### WaitUntil

- Waits until a given condition is true and then invokes the provided action.
- Returns the started coroutine.

```cs
using EmreBeratKR.LazyCoroutines;
using UnityEngine;


public class Test : MonoBehaviour
{
    private int m_CoinCount;
    
    private void Start()
    {
        LazyCoroutines.WaitUntil(() => m_CoinCount > 5, () =>
        {
            Debug.Log("Wait until we have more than 5 coins!");
        });
    }
}
```
