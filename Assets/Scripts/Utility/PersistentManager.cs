using UnityEngine;

public class PersistentManager<T> : Manager<T> where T: PersistentManager<T>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
