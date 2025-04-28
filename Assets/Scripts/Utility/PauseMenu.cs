using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class PauseMenu : Manager<PauseMenu>
{
    public bool GamePaused { get; private set; } = false;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Unpause()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void TogglePause()
    {
        if (GamePaused)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }
}
