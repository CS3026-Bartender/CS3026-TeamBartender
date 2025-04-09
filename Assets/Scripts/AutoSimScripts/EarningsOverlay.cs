using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EarningsOverlay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject overlayPanel;
    [SerializeField] private TextMeshProUGUI earningsText;
    [SerializeField] private TextMeshProUGUI totalMoneyText;
    [SerializeField] private TextMeshProUGUI continueText;

    [Header("Animation Settings")]
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float continueBlinkRate = 0.7f;
    [SerializeField] private float dimIntensity = 0.4f; // How dim the text gets (0.0-1.0)

    // Store the starting money amount
    private float startingMoney;
    private float endingMoney;
    private float earningsDifference;
    private Coroutine blinkCoroutine;
    private Color originalTextColor;

    private void Awake()
    {
        // Make sure the overlay is hidden at the start
        if (overlayPanel != null)
        {
            overlayPanel.SetActive(false);
        }

        // Store the original text color if continueText exists
        if (continueText != null)
        {
            originalTextColor = continueText.color;
        }
    }

    private void Start()
    {
        // Store the starting money at the beginning of the day
        if (CurrencyManager.Instance != null)
        {
            startingMoney = CurrencyManager.Instance.Money;
            Debug.Log($"Starting money: {startingMoney}");
        }
        else
        {
            Debug.LogError("CurrencyManager instance not found!");
        }
    }

    // This method should be called by CustomerController when the simulation ends
    public void ShowEarningsOverlay()
    {
        if (CurrencyManager.Instance != null)
        {
            // Get the final money amount
            endingMoney = CurrencyManager.Instance.Money;

            // Calculate earnings
            earningsDifference = endingMoney - startingMoney;

            Debug.Log($"Day finished! Starting: {startingMoney}, Ending: {endingMoney}, Earned: {earningsDifference}");

            // Update UI text
            if (earningsText != null)
            {
                earningsText.text = $"Day Earnings: ${earningsDifference:F2}";
            }

            if (totalMoneyText != null)
            {
                totalMoneyText.text = $"Total Money: ${endingMoney:F2}";
            }

            // Show the overlay
            if (overlayPanel != null)
            {
                overlayPanel.SetActive(true);
                StartCoroutine(FadeInOverlay());
            }

            // Start pulsing "press any key" text
            if (continueText != null && blinkCoroutine == null)
            {
                blinkCoroutine = StartCoroutine(PulseContinueText());
            }

            // Start listening for input
            StartCoroutine(WaitForInput());
        }
    }

    private IEnumerator FadeInOverlay()
    {
        // Get all canvas groups in the overlay
        CanvasGroup[] canvasGroups = overlayPanel.GetComponentsInChildren<CanvasGroup>(true);

        // If no canvas groups found, add one to the overlay panel
        if (canvasGroups.Length == 0)
        {
            CanvasGroup mainGroup = overlayPanel.AddComponent<CanvasGroup>();
            canvasGroups = new CanvasGroup[] { mainGroup };
        }

        // Start all at 0 alpha
        foreach (CanvasGroup group in canvasGroups)
        {
            group.alpha = 0;
        }

        float elapsedTime = 0;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeInDuration);

            // Fade in all canvas groups
            foreach (CanvasGroup group in canvasGroups)
            {
                group.alpha = alpha;
            }

            yield return null;
        }
    }

    private IEnumerator PulseContinueText()
    {
        if (continueText == null) yield break;

        // Make sure text is visible to start
        continueText.enabled = true;

        while (true)
        {
            // Fade out to dim state
            float elapsedTime = 0;
            Color startColor = originalTextColor;
            Color dimColor = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, originalTextColor.a * dimIntensity);

            while (elapsedTime < continueBlinkRate / 2)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / (continueBlinkRate / 2);
                continueText.color = Color.Lerp(startColor, dimColor, t);
                yield return null;
            }

            // Fade in to full brightness
            elapsedTime = 0;
            while (elapsedTime < continueBlinkRate / 2)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / (continueBlinkRate / 2);
                continueText.color = Color.Lerp(dimColor, startColor, t);
                yield return null;
            }
        }
    }

    private IEnumerator WaitForInput()
    {
        // Wait a small amount of time to prevent accidental input
        yield return new WaitForSeconds(0.5f);

        // Wait for any key or click
        while (true)
        {
            // Check for any key press
            if (Input.anyKeyDown)
            {
                SwitchToShopScene();
                break;
            }

            // Check for mouse click
            if (Input.GetMouseButtonDown(0))
            {
                SwitchToShopScene();
                break;
            }

            yield return null;
        }
    }

    private void SwitchToShopScene()
    {
        // Stop all coroutines
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }

        StopAllCoroutines();

        // Reset text color before switching scene
        if (continueText != null)
        {
            continueText.color = originalTextColor;
        }

        // Load the shop scene
        SceneManager.LoadScene("Shop_Scene");
    }
}