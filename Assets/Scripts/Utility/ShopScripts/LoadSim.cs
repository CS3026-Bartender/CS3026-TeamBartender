using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSim : MonoBehaviour
{
    public void StartAutoSim()
    {
        SceneManager.LoadScene("Autosimulation_Scene");
    }
}
