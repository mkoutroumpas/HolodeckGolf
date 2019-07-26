using System;
using UnityEngine;

public class PlayerRotationLocker : MonoBehaviour {

    public GameObject Player;

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
        if (Player == null)
            return;

        Player.transform.localEulerAngles = new Vector3(0, Camera.main.transform.rotation.y, 0);
    }
}
