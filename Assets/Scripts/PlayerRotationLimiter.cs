using UnityEngine;

public class PlayerRotationLimiter : MonoBehaviour {

    public int FieldOfView = 180;

    private void LateUpdate()
    {
        ClampCameraRotation();
    }

    private void ClampCameraRotation()
    {
        float X = Camera.main.transform.localEulerAngles.x;
        float Y = Camera.main.transform.localEulerAngles.y;
        float Z = Camera.main.transform.localEulerAngles.z;

        string _rot = "x: " + X + ", y: " + Y + ", z: " + Z;

        if (!(Y > 360 - (FieldOfView / 2) && Y <= 360 || Y < (FieldOfView / 2) && Y >= 0))  // y: (270 , 90)
        {
            if (Y <= 360 - (FieldOfView / 2) && Y > 180)
            {
                Camera.main.transform.localEulerAngles = new Vector3(X, 360 - (FieldOfView / 2), Z);
            }
            else if (Y < 180 && Y >= FieldOfView / 2)
            {
                Camera.main.transform.localEulerAngles = new Vector3(X, FieldOfView / 2, Z);
            }
        }
    }

    private void Start()
    {
        
    }
}


