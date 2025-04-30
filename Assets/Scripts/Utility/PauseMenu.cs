using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.InputSystem;

public class PauseMenu : Manager<PauseMenu>
{
    public bool GamePaused { get; private set; } = false;

    public void LoadMainMenu()
    {
        AudioManager.Instance.PlaySound("return_to_menu");
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        AudioManager.Instance.PlaySound("pause");
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Unpause()
    {
        AudioManager.Instance.PlaySound("unpause");
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void TogglePause(InputAction.CallbackContext cc)
    {
        if (!cc.performed) return;

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
