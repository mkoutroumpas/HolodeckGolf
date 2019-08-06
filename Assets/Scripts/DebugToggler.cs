using System.Collections;
using UnityEngine;

public class DebugToggler : MonoBehaviour {

    private readonly float _delay = 3f;
    private bool _active = true;

    public GameObject[] DebugDisplays;

    public void ToggleDebug()
    {
        StartCoroutine(DoToggleDebug());
    }

    private IEnumerator DoToggleDebug()
    {
        yield return new WaitForSeconds(_delay);

        if (!Common.IsPointerLookingToGameObject(gameObject))
        {
            yield break;
        }

        _active = !_active;

        for (int _i = 0; _i < DebugDisplays.Length; _i++)
        {
            if (DebugDisplays[_i] != null)
                DebugDisplays[_i].SetActive(_active);
        }
    }
}
