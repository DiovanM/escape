﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Events
{

    public sealed class Listener<T1, T2>
    {
        public Action<T1, T2> action;
        public MonoBehaviour objectListener;
        public int priority;

        public Listener(Action<T1, T2> action, MonoBehaviour objectListener, int priority = 0)
        {
            this.action = action;
            this.objectListener = objectListener;
            this.priority = priority;
        }
    }


    public class Event<T1, T2>
    {
        public List<Listener<T1, T2>> listeners;

        public Event()
        {
            if (listeners == null)
                listeners = new List<Listener<T1, T2>>();
        }

        public void Trigger(T1 param1, T2 param2)
        {
            var initialListenersCount = listeners.Count;
            for (var i = 0; i < listeners.Count; i++)
            {
                var listener = listeners[i];
                if (listener == null || listener.objectListener == null || listener.action == null)
                    Remove(listener.action);
                else
                    listener.action?.Invoke(param1, param2);
                if (initialListenersCount != listeners.Count)
                {
                    i--;
                    initialListenersCount = listeners.Count;
                    OrderListeners();
                }
            }
        }

        public void Insert(Action<T1, T2> action, MonoBehaviour objectListener, int priority = 0)
        {
            if (listeners == null)
                listeners = new List<Listener<T1, T2>>();

            var listener = new Listener<T1, T2>(action, objectListener, priority);
            listeners.Add(listener);
            OrderListeners();
        }

        public void Remove(Action<T1, T2> action)
        {
            var listener = listeners.FirstOrDefault(x => x.action == action);
            listeners?.Remove(listener);
            OrderListeners();
        }
        public void RemoveAllInstances(Action<T1, T2> action)
        {
            listeners?.RemoveAll(x => x.action == action);
            OrderListeners();
        }

        private void OrderListeners() => listeners = listeners.OrderByDescending(x => x.priority).ToList();
        public void Clear() => listeners.Clear();
    }

}