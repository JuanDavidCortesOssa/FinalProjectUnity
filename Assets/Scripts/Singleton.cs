using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
                throw new InvalidOperationException($"No instance of {typeName} found in the scene.");
            return instance;
        }
    }

    private static string typeName => typeof(T).ToString();

    protected virtual void Awake()
    {
        Register();
    }

    protected virtual void OnEnable()
    {
        if (instance == null)
            Register();
    }

    private void Register()
    {
        if (instance != null)
        {
            Debug.LogError($"More than one singleton object of type {typeName} exists, destroying duplicate '{gameObject.name}'");
            Destroy(gameObject);
            return;
        }
        instance = (T)this;
    }

    public static bool HasInstance => instance != null;

    private void OnApplicationQuit()
    {
        instance = null;
    }
}