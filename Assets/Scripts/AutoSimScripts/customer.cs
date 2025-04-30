using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Customer : MonoBehaviour
{
    public int Max_satisfaction = 100;
    public float Current_satisfaction;
    public bool CanBeServed = true;
    public Drink preferredDrink;
    public TextMeshProUGUI DisplayText;

    // Colors for each customer type
    [SerializeField] private Color regularColor;
    [SerializeField] private Color regularBackgroundColor;

    private List<Drink> equippedDrinks;
    private CustomerController CustomerControllerScript;
    public Slider SatisfactionBarSlider;
    public float MoneyPayed { get; private set; }

    // Health bar images 
    [SerializeField] private Image SatisfactionBarImage;
    [SerializeField] private Image satisfactionBarBackground;

    private Coroutine drinkServiceCoroutine;

    // Customer states
    public enum CustomerState
    {
        WaitingForDrink,
        BeingServed,
        EnjoyingDrink,
        Paying
    }
    // Random Sprite Selection

    public Sprite[] sprites;
    // Current state
    private CustomerState currentState;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        equippedDrinks = DrinkData.Instance.GetAllDrinksAsList();

        //Setup Interaction with CustomerController
        GameObject ControllerObject = GameObject.Find("CustomerController");
        if (ControllerObject != null)
        {
            CustomerControllerScript = ControllerObject.GetComponent<CustomerController>();
        }

        
    }

    // Start is called once before the first execution of Update
    void Start()
    {
        Current_satisfaction = 0;

        // Check if health bar components are assigned
        if (SatisfactionBarImage == null || satisfactionBarBackground == null)
        {
            Debug.LogError("Health bar images not assigned in inspector!");
        }

        SatisfactionBarImage.color = regularColor;
        satisfactionBarBackground.color = regularBackgroundColor;
        SatisfactionBarSlider.value = Current_satisfaction;

        //sprite selection
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component

        if (sprites != null && sprites.Length > 0)
        {
            // Choose a random sprite index
            int randomIndex = Random.Range(0, sprites.Length);

            // Assign the random sprite to the SpriteRenderer
            spriteRenderer.sprite = sprites[randomIndex];
        }
        else
        {
            Debug.LogWarning("No sprites assigned to the RandomSpriteSelector script.");
        }

        MoneyPayed = 0;

        WantsDrink();
    }

    public void IncreaseSatisfaction(float Satisfaction)
    {
        Current_satisfaction += Satisfaction;
        SatisfactionBarSlider.value = Current_satisfaction;

        if (Current_satisfaction >= Max_satisfaction && CustomerControllerScript != null)
        {
            Pay();
        }
    }

    private void Pay()
    {
        // Change state to paying
        SetState(CustomerState.Paying);

        DisplayText.text = "Paying";
        StartCoroutine(PayAndLeave());
    }

    private IEnumerator PayAndLeave()
    {
        // Wait for a second before leaving
        yield return new WaitForSeconds(1f);

        // Add money and remove customer
        AudioManager.Instance.PlaySound("payment");
        CurrencyManager.Instance.AddMoney(MoneyPayed);
        CustomerControllerScript.RemoveCustomer(gameObject);
    }

    public void WantsDrink()
    {
        CanBeServed = true;

        if (equippedDrinks != null && equippedDrinks.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, equippedDrinks.Count);
            preferredDrink = equippedDrinks[randomIndex];
        }

        // Set state to waiting for drink
        SetState(CustomerState.WaitingForDrink);

        DisplayText.text = "Wants: " + (IngredientData.GetIngValue(preferredDrink.GetIngID(0))).DisplayName;

        // Add self to the queue for service
        if (CustomerControllerScript != null)
        {
            CustomerControllerScript.AddCustomerToQueue(gameObject);
        }
    }

    public void Serve()
    {
        // Change state to being served
        SetState(CustomerState.BeingServed);

        // Only change display text to "Waiting for" at this point
        DisplayText.text = "Waiting for " + (IngredientData.GetIngValue(preferredDrink.GetIngID(0))).DisplayName;
        CanBeServed = false;
    }

    public void ServeAfterCooldown()
    {
        AudioManager.Instance.PlaySound("drink_made");

        // Stop any existing serving coroutine if it's running
        if (drinkServiceCoroutine != null)
        {
            StopCoroutine(drinkServiceCoroutine);
        }

        // Change state to enjoying drink
        SetState(CustomerState.EnjoyingDrink);

        DisplayText.text = "Enjoying " + (IngredientData.GetIngValue(preferredDrink.GetIngID(0))).DisplayName;

        MoneyPayed += preferredDrink.GetCalculatedSellPrice();

        drinkServiceCoroutine = StartCoroutine(FillSatisfactionOverTime());
    }

    private IEnumerator FillSatisfactionOverTime()
    {
        float drinkTime = preferredDrink.GetCalculatedCustomerDrinkTime();
        float potency = preferredDrink.GetCalculatedPotency();
        float startSatisfaction = Current_satisfaction;
        float endSatisfaction = startSatisfaction + potency;
        float elapsedTime = 0f;

        // Fill satisfaction smoothly over time
        while (elapsedTime < drinkTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / drinkTime; // Normalized time (0 to 1)

            // Calculate current satisfaction using linear interpolation
            Current_satisfaction = Mathf.Lerp(startSatisfaction, endSatisfaction, t);
            SatisfactionBarSlider.value = Current_satisfaction;

            // Check if customer is fully satisfied during the process
            if (Current_satisfaction >= Max_satisfaction)
            {
                AudioManager.Instance.PlaySound("finish_drink");
                Pay();
                yield break; // Exit the coroutine early
            }

            yield return null; // Wait for the next frame
        }

        // Ensure we reach the exact target satisfaction at the end
        Current_satisfaction = endSatisfaction;
        SatisfactionBarSlider.value = Current_satisfaction;

        AudioManager.Instance.PlaySound("finish_drink");

        // Check if fully satisfied after drinking
        if (Current_satisfaction >= Max_satisfaction)
        {
            Pay();
        }
        else
        {
            // If not fully satisfied, ask for another drink
            WantsDrink();
        }
    }

    // Helper method to set the state and handle any state-specific logic
    private void SetState(CustomerState newState)
    {
        // Update the current state
        currentState = newState;

        // Debug log for state changes
        Debug.Log($"Customer state changed to: {newState}");
    }
}