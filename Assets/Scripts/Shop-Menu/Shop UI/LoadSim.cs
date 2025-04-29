using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSim : MonoBehaviour
{
    public void StartAutoSim()
    {
        AudioManager.Instance.PlaySound("start_sim");
        SceneManager.LoadScene("Autosimulation_Scene");
    }
}
