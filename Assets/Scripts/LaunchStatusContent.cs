using UnityEngine;
using UnityEngine.UI;

public class LaunchStatusContent : MonoBehaviour {
    private bool _startCountdown;
    private float _launchDelay;
    private int _nextActionTime = 0;
    private int _interval = 1;

    private float _lastLaunchedTime = 0f;

    public void Update()
    {
        if (_startCountdown)
        {
            if (Time.time > _nextActionTime)
            {
                _nextActionTime += _interval;

                DisplayText("Launching in " + (_launchDelay - ((int)Time.time - (int)_lastLaunchedTime)) + " ...");
            }
        }
    }

    public void Start()
    {
        _nextActionTime = 0;
    }

    public void OnLaunchStatus(string data = "", int launchDelay = 0)
    {
        if (launchDelay > 0) 
        {
            _launchDelay = launchDelay;
            _lastLaunchedTime = Time.time;

            _startCountdown = true;
        }
        else 
        {
            _startCountdown = false;

            if (!string.IsNullOrEmpty(data))
            {
                DisplayText(data);
            }
        }
    }

    private void DisplayText(string data)
    {
        Text text = GetComponent<Text>();
        if (text != null)
            text.text = data;
    }
}
