using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class Common
{
    public readonly static string ObjectFilterLiteral = "Ball";

    public static bool IsPointerLookingToGameObject(GameObject gameObject)
    {
        if (gameObject == null)
            return false;

        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        var _focusedGameObjects = raycastResults.Where(r => r.gameObject.name == gameObject.name);

        return _focusedGameObjects != null && _focusedGameObjects.Count() > 0;
    }
}
