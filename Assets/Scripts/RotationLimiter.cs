using System;
using UnityEngine;

public class RotationLimiter : MonoBehaviour {

    void LateUpdate()
    {
        clampCameraRotation();
    }

    public float xSensitivity = 400.0f;
    public float ySensitivity = 400.0f;

    public float yMinLimit = -85.0f;
    public float yMaxLimit = 85.0f;

    public float xMinLimit = -90.0f;
    public float xMaxLimit = 90.0f;

    float yRot = 0.0f;
    float xRot = 0.0f;

    void clampCameraRotation()
    {
        string _rot = "x: " + Camera.main.transform.localEulerAngles.x + ", y: " + Camera.main.transform.localEulerAngles.y + ", z: " + Camera.main.transform.localEulerAngles.z;

        if (!(Camera.main.transform.localEulerAngles.y > 270 && Camera.main.transform.localEulerAngles.y <= 360 ||
            Camera.main.transform.localEulerAngles.y < 90 && Camera.main.transform.localEulerAngles.y >= 0))  // y: (270 , 90)
        {
            Console.WriteLine(_rot);

            if (Camera.main.transform.localEulerAngles.y <= 270 && Camera.main.transform.localEulerAngles.y > 180)
            {
                Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, 270, Camera.main.transform.localEulerAngles.z);
            }
            if (Camera.main.transform.localEulerAngles.y < 180 && Camera.main.transform.localEulerAngles.y >= 90)
            {
                Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, 90, Camera.main.transform.localEulerAngles.z);
            }
        }
    }

    private void Start()
    {
        
    }
}


