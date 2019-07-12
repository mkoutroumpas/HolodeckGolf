using System;
using UnityEngine;

public class RotationLimiter : MonoBehaviour {

    void LateUpdate()
    {
        ////Cursor.lockState = CursorLockMode.Locked;

        rotateCamera();
    }

    public float xSensitivity = 400.0f;
    public float ySensitivity = 400.0f;

    public float yMinLimit = -85.0f;
    public float yMaxLimit = 85.0f;

    public float xMinLimit = -90.0f;
    public float xMaxLimit = 90.0f;

    float yRot = 0.0f;
    float xRot = 0.0f;

    void rotateCamera()
    {
        xRot += Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        yRot += Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, yMinLimit, yMaxLimit);
        xRot = Mathf.Clamp(xRot, xMinLimit, xMaxLimit);

        // Maybe calculations need to be properly mapped before rotating Camera.
        // Or, check if the current Camera rotation is withing allowed range.

        ////Camera.main.transform.localEulerAngles = new Vector3(-yRot, xRot, 0);

        string _rot = "x: " + Camera.main.transform.localEulerAngles.x + ", y: " + Camera.main.transform.localEulerAngles.y + ", z: " + Camera.main.transform.localEulerAngles.z;

        if (!(Camera.main.transform.localEulerAngles.y > 270 && Camera.main.transform.localEulerAngles.y <= 360 ||
            Camera.main.transform.localEulerAngles.y < 90 && Camera.main.transform.localEulerAngles.y >= 0))  // y: (90 , 270)
        {
            Console.WriteLine(_rot);
        }
    }

    private void Start()
    {
        
    }
}


