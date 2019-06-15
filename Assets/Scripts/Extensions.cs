using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class ExtensionMethods // See: https://answers.unity.com/questions/854251/how-do-you-add-an-ui-eventtrigger-by-script.html
{
    private static readonly System.Random _random = new System.Random();

    public static void AddListener(this EventTrigger trigger, EventTriggerType eventType, System.Action<PointerEventData> listener)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener(data => listener.Invoke((PointerEventData)data));
        trigger.triggers.Add(entry);
    }

    public static float DeviateBy(this float f, float maxDeviation)
    {
        if (maxDeviation < 0 || Mathf.Abs(f) < maxDeviation)
            return f;

        return (float)(_random.NextDouble() * 2 * maxDeviation + (f - maxDeviation));
    }

    public static Vector3 DeviateBy(this Vector3 v, float maxDeviation)
    {
        return new Vector3(v.x.DeviateBy(maxDeviation), v.y.DeviateBy(maxDeviation), v.z.DeviateBy(maxDeviation));
    }
}