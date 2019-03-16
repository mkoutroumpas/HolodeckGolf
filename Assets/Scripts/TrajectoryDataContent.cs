using System;
using UnityEngine;
using UnityEngine.UI;

public class TrajectoryDataContent : MonoBehaviour {

    private static string _contentText;
    private static int _lineCount;

    private void Update()
    {
        if (!string.IsNullOrEmpty(_contentText))
        {
            Text text = GetComponent<Text>();

            if (_lineCount == 0)
            {
                text.text = string.Empty;
            }

            text.text += _contentText + Environment.NewLine;

            _lineCount++;
            _contentText = null;
        }
    }

    public static void OnLaunchEvent(string data)
    {
        _contentText = data;
    }
}
