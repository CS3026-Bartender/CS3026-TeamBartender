using UnityEngine;
using UnityEngine.InputSystem;

public class IntroScreen : MonoBehaviour
{
    public static bool seen = false;



    public void Close(InputAction.CallbackContext cc)
    {
        if (!cc.performed) return;

        AudioManager.Instance.PlaySound("close_screen");
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
