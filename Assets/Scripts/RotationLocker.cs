using System;
using UnityEngine;

public class RotationLocker : MonoBehaviour {

    private void Start () {
		
	}
	
	private void Update () {
		
	}

    private void LateUpdate()
    {
        LockPlayerRotation();
    }

    private void LockPlayerRotation()
    {
        transform.Rotate(new Vector3(0, Camera.main.transform.rotation.y, 0));
    }
}
