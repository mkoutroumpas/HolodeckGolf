using System;
using UnityEngine;

public class PlayerRotationLimiter : MonoBehaviour {

    public int FieldOfView = 180;

    private void LateUpdate()
    {
        ClampCameraRotation();
    }

    private void ClampCameraRotation()
    {
        string _rot = "x: " + Camera.main.transform.localEulerAngles.x + ", y: " + Camera.main.transform.localEulerAngles.y + ", z: " + Camera.main.transform.localEulerAngles.z;

        if (!(Camera.main.transform.localEulerAngles.y > 360 - (FieldOfView / 2) && Camera.main.transform.localEulerAngles.y <= 360 ||
            Camera.main.transform.localEulerAngles.y < (FieldOfView / 2) && Camera.main.transform.localEulerAngles.y >= 0))  // y: (270 , 90)
        {
            Console.WriteLine(_rot);

            if (Camera.main.transform.localEulerAngles.y <= 360 - (FieldOfView / 2) && Camera.main.transform.localEulerAngles.y > 180)
            {
                Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, 360 - (FieldOfView / 2), Camera.main.transform.localEulerAngles.z);
            }
            else if (Camera.main.transform.localEulerAngles.y < 180 && Camera.main.transform.localEulerAngles.y >= FieldOfView / 2)
            {
                Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, FieldOfView / 2, Camera.main.transform.localEulerAngles.z);
            }
        }
    }

    private void Start()
    {
        
    }
}


