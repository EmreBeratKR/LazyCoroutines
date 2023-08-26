# LazyCoroutines

## Links

[<img src="https://makaka.org/wp-content/uploads/2022/02/new-unity-asset-store-badge-full.png" width="200" />][assetstore]

[<img src="https://images.squarespace-cdn.com/content/v1/5bbc502865019fe7b132cdc0/1619022573920-HXS3VG6DNLBH6NYX2963/discord-button.png" width="200" />][discord]

[<img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" width="200" />][coffee]

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

## Debugger

### How to Access to Debugger Panel

<img src ="https://github.com/EmreBeratKR/ImageContainer/blob/main/LazyCoroutines/debugger_instruction.png" />

- From Unity Menu Item's ```Tools```>```EmreBeratKR```>```Lazy Coroutines```>```Debugger```

### Debugger Panel

<img src="https://github.com/EmreBeratKR/ImageContainer/blob/main/LazyCoroutines/debugger.png" />

## API

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
