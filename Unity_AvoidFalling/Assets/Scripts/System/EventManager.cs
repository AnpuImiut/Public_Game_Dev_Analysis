using System.Collections;
using System.Collections.Generic;

using UnityEngine.Events;

public class EventManager
{
    // Singleton Pattern
    private Dictionary<string, UnityAction> event_list = new Dictionary<string, UnityAction>();
    private static readonly EventManager instance = new EventManager();

    static EventManager() {}
    private EventManager() {}
    public static EventManager get_instance()
    {
        return instance;
    }

    public static void register(string event_name, UnityAction method)
    {
        if(!instance.event_list.ContainsKey(event_name))
        {
            instance.event_list.Add(event_name, method);
        }
        else
        {
            instance.event_list[event_name] += method;
        }
    }

    public static void unregister(string event_name, UnityAction method)
    {
        instance.event_list[event_name] -= method;
    }

    public static void trigger_event(string event_name)
    {   
        if(instance.event_list.ContainsKey(event_name) && instance.event_list[event_name] != null)
        {
            instance.event_list[event_name].Invoke();
        }
    }
}
