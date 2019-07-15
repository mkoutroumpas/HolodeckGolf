using UnityEngine;

public class PlayerRotationLimiter : MonoBehaviour {

    public int FOVHorizontal = 180;
    public int FOVVertical = 180;

    private void Start()
    {
        if (FOVHorizontal < 0 || FOVHorizontal > 180)
            FOVHorizontal = 180;
        if (FOVVertical < 0 || FOVVertical > 180)
            FOVVertical = 180;
    }

    private void LateUpdate()
    {
        ClampCameraRotation();
    }

    private void ClampCameraRotation()
    {
        ClampRotation(Camera.main.transform.localEulerAngles, FOVHorizontal, Common.RotationAxis.Y);
        ClampRotation(Camera.main.transform.localEulerAngles, FOVVertical, Common.RotationAxis.X);
    }

    private void ClampRotation(Vector3 angles, int fov, Common.RotationAxis axis)
    {
        float angle = 0;
        Vector3 rotateBy = default(Vector3);
        Vector3 rotateByRev = default(Vector3);

        switch (axis)
        {
            case Common.RotationAxis.X:
                angle = angles.x;
                rotateBy = new Vector3(360 - (fov / 2), angles.y, angles.z);
                rotateByRev = new Vector3(fov / 2, angles.y, angles.z);
                break;
            case Common.RotationAxis.Y:
                angle = angles.y;
                rotateBy = new Vector3(angles.x, 360 - (fov / 2), angles.z);
                rotateByRev = new Vector3(angles.x, fov / 2, angles.z);
                break;
        }

        if (!(angle > 360 - (fov / 2) || angle < (fov / 2)))
        {
            if (angle < 360 - (fov / 2) && angle > 180)
            {
                Camera.main.transform.localEulerAngles = rotateBy;
            }
            else if (angle < 180 && angle > fov / 2)
            {
                Camera.main.transform.localEulerAngles = rotateByRev;
            }
        }
    }
}


