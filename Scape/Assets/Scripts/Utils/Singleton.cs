using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    public static T instance { get; private set; }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("An instance already exists: " + (this as T).name);
            Destroy(this);
            return;
        }
        else
        {
            instance = this as T;
        }
    }
}
