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
        _active = !_active;

        StatusContent sc = Camera.main.GetComponentInChildren<StatusContent>();

        sc.OnStatusChange((_active ? "Activating " : "Deactivating ") + "debug info", (int)_delay);

        yield return new WaitForSeconds(_delay);

        if (!Common.IsPointerLookingToGameObject(gameObject))
        {
            sc.OnStatusChange("Idle.");

            yield break;
        }

        for (int _i = 0; _i < DebugDisplays.Length; _i++)
        {
            if (DebugDisplays[_i] != null)
                DebugDisplays[_i].SetActive(_active);
        }

        sc.OnStatusChange("Idle.");
    }
}
