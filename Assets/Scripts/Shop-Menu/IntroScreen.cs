using UnityEngine;

public class IntroScreen : MonoBehaviour
{
    public static bool seen = false;

    public void Close()
    {
        seen = true;
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (seen)
        {
            gameObject.SetActive(false);
        }
    }
}
