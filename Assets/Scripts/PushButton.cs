using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PushButton : MonoBehaviour
{
    public static event Action ButtonPressed = delegate { };
    private bool coroutineAllowed = true;

    private void OnMouseDown()
    {
        if (coroutineAllowed)
        {
            StartCoroutine("PushTheButton");
        }
    }

    private IEnumerator PushTheButton()
    {
        ButtonPressed();
        coroutineAllowed = false;
        for (int i = 0; i <= 20; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f);
            yield return null;
        }
        for (int i = 0; i <= 20; i++)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
            yield return null;
        }
        coroutineAllowed = true;
    }

}
