using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FramerateCounter : MonoBehaviour {

    public Text text;
    private float count;

    public IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            if (Time.timeScale == 1)
            {
                yield return new WaitForSeconds(0.1f);
                count = (1 / Time.deltaTime);
                text.text = "FPS :" + (Mathf.Round(count));
            }
            else
            {
                text.text = "Pause";
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
