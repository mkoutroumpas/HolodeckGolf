using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class Common
{
    public readonly static string ObjectFilterLiteral = "Ball";

    public enum RotationAxis
    {
        X, Y
    }

    public static bool IsPointerLookingToGameObject(GameObject gameObject)
    {
        if (gameObject == null)
            return false;

        Vector3 p = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane));
        
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        Vector3 mp = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        var focusedGameObjects = raycastResults.Where(r => r.gameObject.name == gameObject.name);
        var ret = focusedGameObjects != null && focusedGameObjects.Count() > 0;

        return ret;
    }
}
