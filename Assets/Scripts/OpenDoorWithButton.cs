using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorWithButton : MonoBehaviour
{
    public GameObject Door;
    public bool isOpened = false;

    public float doorOpenAngle = 100.0f; //Set either positive or negative number to open the door inwards or outwards
    //float speedOfRotation = 30f;
    float openTime = 0;
    float openSpeed = 1.0f;  //Increasing this value will make the door open faster

    float defaultRotationAngle;
    float currentRotationAngle;
 

    void Start()
    {
        defaultRotationAngle = Door.transform.localEulerAngles.z;
        currentRotationAngle = Door.transform.localEulerAngles.z;
    }


    private void Update()
    {
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeed;
        }
        Door.transform.localEulerAngles = new Vector3(Door.transform.localEulerAngles.x, Door.transform.localEulerAngles.z, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (isOpened ? doorOpenAngle : 0), openTime));

        if (Door.transform.localEulerAngles.z > 100f)
        {
            isOpened = false;
            currentRotationAngle = Door.transform.localEulerAngles.z;
            openTime = 0;
        }

        /*if (isOpened == true)
        {

            Door.transform.Rotate(Vector3.forward * Time.deltaTime * speedOfRotation);
            if (Door.transform.rotation.eulerAngles.z > 100f)
            {
                isOpened = false;
            }*/

}
    private void OnMouseDown()
    {
        isOpened = true;
    }
}
