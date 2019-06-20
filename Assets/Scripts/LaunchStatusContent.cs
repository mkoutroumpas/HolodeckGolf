using UnityEngine;
using UnityEngine.UI;

public class LaunchStatusContent : MonoBehaviour {

    public void Update()
    {
        
    }

    public void OnLaunchStatus(string data, bool startCountdown = false)
    {
        if (!string.IsNullOrEmpty(data))
        {
            Text text = GetComponent<Text>();
            text.text = data;
        }
    }
}
