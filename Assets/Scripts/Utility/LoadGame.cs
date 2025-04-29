using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void Load()
    {
        AudioManager.Instance.PlaySound("play_game");
        ResetGame.Instance.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
