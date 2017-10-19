using UnityEngine;

public class SingletonBehavior<T> : MonoBehaviour where T : Component
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            Debug.Log(typeof(T).Name + " was null. Instancing...");
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void Init()
    {
    }
}

