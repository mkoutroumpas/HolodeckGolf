using UnityEngine;

public class Main : MonoBehaviour {

    void Start ()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}
