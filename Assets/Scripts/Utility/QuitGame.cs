using UnityEngine;

public class QuitGame : Manager<QuitGame>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
