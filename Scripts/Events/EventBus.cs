using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    Save,
    Load,
    NewDay
}

public class EventBus : MonoBehaviour
{
    private Dictionary<string, List<ILocalEvent>> _events; 

    public static EventBus Instance { get; private set; }

    private void Awake()
    {
        //if (Instance == null)
        //{
            Instance = this;
            //DontDestroyOnLoad(this);
        //}

        _events = new Dictionary<string, List<ILocalEvent>>();

        _events.Add(EventType.Save.ToString(), new List<ILocalEvent>());
        _events.Add(EventType.Load.ToString(), new List<ILocalEvent>());
        _events.Add(EventType.NewDay.ToString(), new List<ILocalEvent>());
    }

    public void Subscribe(EventType eventType, ILocalEvent localEvent)
    {
        _events[eventType.ToString()].Add(localEvent);
    }

    public void Subscribe(Type type, ILocalEvent localEvent)
    {
        if (_events.ContainsKey(type.ToString()) == false)
        {
            _events.Add(type.ToString(), new List<ILocalEvent>());
        }

        _events[type.ToString()].Add(localEvent);
    }

    public void Invoke(EventType eventType)
    {
        foreach (var e in _events[eventType.ToString()])
        {
            e.Invoke();
        }
    }

    public void Invoke(Type type)
    {
        var tmp = new List<ILocalEvent>();

        foreach (var e in _events[type.ToString()]) 
            tmp.Add(e);

        foreach (var e in tmp) //events[type.ToString()]
            e.Invoke();      
    }

    public void Unsub(Type type, ILocalEvent localEvent)
    {
        _events[type.ToString()].Remove(localEvent);
    }
}
