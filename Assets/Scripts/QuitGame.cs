using UnityEngine;

public class QuitGame : Manager<QuitGame>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
