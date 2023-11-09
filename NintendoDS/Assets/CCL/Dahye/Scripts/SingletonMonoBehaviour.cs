using UnityEngine;
/// <summary>
/// mono behaviour singleton pattern
/// </summary>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Externally accessible instances
    /// </summary>
    public static T Instance
    {
        get
        {
            // If it does not exist when obtaining an instance, it is created and retrieved.
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }

            return instance;
        }
    }

    /// <summary>
    /// Called when an object is created in the scene.
    /// </summary>
    protected virtual void Awake()
    {
        // If creation has already occurred and is called again, it is deleted.
        if (instance != null)
        {
            Destroy(instance);
            return;
        }

        // If you are creating it for the first time, give the instance properties to prevent it from being destroyed.
        instance = this as T;
        DontDestroyOnLoad(instance);
        Init();
    }

    /// <summary>
    /// init during Awake()
    /// </summary>
    protected virtual void Init() { }

    static T instance;

}