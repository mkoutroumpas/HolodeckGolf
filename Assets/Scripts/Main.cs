using UnityEngine;

public class Main : MonoBehaviour {

    void Start ()
    {
        ////QualitySettings.vSyncCount = 0;
        ///
        Camera.main.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
}
