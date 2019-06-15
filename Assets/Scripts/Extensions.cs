using UnityEngine.EventSystems;

public static class ExtensionMethods // See: https://answers.unity.com/questions/854251/how-do-you-add-an-ui-eventtrigger-by-script.html
{
    public static void AddListener(this EventTrigger trigger, EventTriggerType eventType, System.Action<PointerEventData> listener)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener(data => listener.Invoke((PointerEventData)data));
        trigger.triggers.Add(entry);
    }
}