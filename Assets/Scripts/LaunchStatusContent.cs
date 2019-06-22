using UnityEngine;
using UnityEngine.UI;

public class LaunchStatusContent : MonoBehaviour {
    private bool _startCountdown;
    private float _launchDelay;
    private float _nextActionTime = 0f;
    private int _interval = 1;

    public void Update()
    {
        if (_startCountdown)
        {
            if (Time.time > _nextActionTime)
            {
                _nextActionTime += _interval;
                DisplayText("Launching in " + (_launchDelay - _nextActionTime) + " ...");
            }
        }
        else
        {
            _nextActionTime = 0f;
        }
    }

    public void OnLaunchStatus(string data = "", float launchDelay = 0f)
    {
        _startCountdown = false;

        if (launchDelay > 0) 
        {
            _startCountdown = true;
            _launchDelay = launchDelay;
        }
        else if (_launchDelay == 0)
        {
            if (!string.IsNullOrEmpty(data))
            {
                DisplayText(data);
            }
        }
    }

    private void DisplayText(string data)
    {
        Text text = GetComponent<Text>();
        text.text = data;
    }
}
