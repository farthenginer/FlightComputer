/*
    03.08.2026 - 13:33 Created by Omer Faruk Simsek
*/

using UnityEngine;

/// <summary>
/// Generic MonoBehaviour singleton base class.
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    [SerializeField] private bool dontDestroyOnLoad = false;

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy gameObject.
            return;
        }

        Instance = this as T;

        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}