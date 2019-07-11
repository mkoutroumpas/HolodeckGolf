﻿using UnityEngine;

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

        Camera.main.transform.localEulerAngles = new Vector3(-yRot, xRot, 0);
    }

    private void Start()
    {
        
    }
}


