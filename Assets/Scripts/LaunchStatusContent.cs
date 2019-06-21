using UnityEngine;
using UnityEngine.UI;

public class LaunchStatusContent : MonoBehaviour {
    private bool _startCountdown;

    public void Update()
    {
        
    }

    public void OnLaunchStatus(string data, bool startCountdown = false)
    {
        _startCountdown = startCountdown;

        if (!string.IsNullOrEmpty(data))
        {
            Text text = GetComponent<Text>();
            text.text = data;
        }
    }
}
