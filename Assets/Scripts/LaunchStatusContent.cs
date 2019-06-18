using UnityEngine;
using UnityEngine.UI;

public class LaunchStatusContent : MonoBehaviour {

    public void OnLaunchStatus(string data)
    {
        if (!string.IsNullOrEmpty(data))
        {
            Text text = GetComponent<Text>();
            text.text = data;
        }
    }
}
