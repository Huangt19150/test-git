using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool doorOpened;
    private bool coroutineAllowed;
    // Start is called before the first frame update
    void Start()
    {
        doorOpened = false;
        coroutineAllowed = true;
        PushButton.ButtonPressed += RunCoroutine;
    }

    private void RunCoroutine()
    {
        StartCoroutine("OpenThatDoor");
    }

    private IEnumerator OpenThatDoor()
     {
        coroutineAllowed = false;
        if(!doorOpened)
        {
            for(float i=0f; i<100f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(-90f, 0f, i);
                yield return new WaitForSeconds(0f);
            }
            doorOpened = true;
        }
        else
        {
            for (float i = 100f; i >= 0f; i -= 10f )
            {
                transform.rotation = Quaternion.Euler(-90f, 0f, i);
                yield return new WaitForSeconds(0f);
            }
            doorOpened = false;
        }
        coroutineAllowed = true;
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        PushButton.ButtonPressed -= RunCoroutine;
    }
}
