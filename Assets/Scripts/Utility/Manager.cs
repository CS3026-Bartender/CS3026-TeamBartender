using Unity.VisualScripting;
using UnityEngine;

public abstract class Manager<T> : MonoBehaviour where T : Manager<T>
{
    public static T Instance { get; private set; } = null;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
            Debug.Log("got an instance");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("destroyed a new one");
        }
    }
}
