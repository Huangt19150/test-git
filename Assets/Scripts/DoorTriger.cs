using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriger : MonoBehaviour
{
    [SerializeField]
    GameObject Door;

    bool isOpened = false;
    void OnTriggerEnter(Collider col)
    {
        if(!isOpened)
        {
            isOpened = true;
            Door.transform.position += new Vector3(0, 1, 0);
        }
        
    }

   
}
