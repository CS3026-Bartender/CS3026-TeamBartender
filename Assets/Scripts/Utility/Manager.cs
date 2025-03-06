using Unity.VisualScripting;
using UnityEngine;

public abstract class Manager<T> : MonoBehaviour where T : Manager<T>
{
    // public static T instance = null;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = (T)this;
        //    DontDestroyOnLoad(gameObject);
        //    Debug.Log("got an instance");
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    Debug.Log("destroyed a new one");
        //}
    }
}
