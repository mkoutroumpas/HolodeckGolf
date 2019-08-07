using UnityEngine;
using UnityEngine.UI;

public class StatusContent : MonoBehaviour {
    private bool _startCountdown;
    private float _delay;
    private int _nextActionTime = 0;
    private int _interval = 1;
    private string _data = "";
    private float _lastStatusChangeTime = 0f;

    public GameObject SFX;
    public GameObject Player;

    public void Update()
    {
        if (_startCountdown)
        {
            if (Time.time > _nextActionTime)
            {
                _nextActionTime += _interval;

                if (!string.IsNullOrEmpty(_data))
                {
                    DisplayText(_data.Trim() + " " + (_delay - ((int)Time.time - (int)_lastStatusChangeTime)) + " ...");
                }
            }
        }
    }

    public void OnStatusChange(string data = "", int launchDelay = 0)
    {
        _data = data;

        if (launchDelay > 0) 
        {
            _delay = launchDelay;
            _lastStatusChangeTime = Time.time;

            _startCountdown = true;
        }
        else 
        {
            _startCountdown = false;

            DisplayText(_data);
        }
    }

    private void DisplayText(string data)
    {
        Text text = GetComponent<Text>();
        if (text != null)
            text.text = data;
    }

    public void OnStatusSFXPlay(bool launch)
    {
        if (!launch || SFX == null)
            return;

        AudioSource audioSource = SFX.GetComponent<AudioSource>();

        if (audioSource == null)
            return;

        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.Play();
    }

    public void OnStartPlayerAnimation(bool animate)
    {
        if (!animate || Player == null)
            return;

        Animator animator = Player.GetComponent<Animator>();
        animator.SetTrigger("DriverStrike"); //-> This works if an Idle animation is present.
    }
}
