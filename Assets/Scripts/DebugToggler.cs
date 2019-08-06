using System;
using System.Collections;
using UnityEngine;

public class DebugToggler : MonoBehaviour {

    private readonly float _delay = 3f;

    private void Start()
    {
        
    }

    private void Update()
    {

    }

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

        // Toggle debug info.
    }
}
