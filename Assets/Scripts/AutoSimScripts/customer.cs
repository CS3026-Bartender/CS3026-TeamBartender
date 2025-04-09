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
    public Slider HealthBarSlider;
    [SerializeField] private float MoneyPayed;
    // Health bar images 
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image healthBarBackground;
    private Coroutine drinkServiceCoroutine;

    [Header("States")]

    //States
    public CustomerOrderingState orderingState;
    public CustomerWaitingState waitingState;
    public CustomerDrinkingState drinkingState;
    public CustomerPayingState payingState;

    //Current State
    CustomerState state;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Current_satisfaction = 0;
        // Check if health bar components are assigned
        if (healthBarImage == null || healthBarBackground == null)
        {
            Debug.LogError("Health bar images not assigned in inspector!");
        }
        healthBarImage.color = regularColor;
        healthBarBackground.color = regularBackgroundColor;
        HealthBarSlider.value = Current_satisfaction;
        MoneyPayed = 0;
        WantsDrink();
    }

    public void IncreaseSatisfaction(float Satisfaction)
    {
        Current_satisfaction += Satisfaction;
        HealthBarSlider.value = Current_satisfaction;
        if (Current_satisfaction >= Max_satisfaction && CustomerControllerScript != null)
        {
            Pay();
        }
    }

    private void Pay()
    {
        //enter new state
        state = payingState;
        state.Enter();


        DisplayText.text = "Paying";
        StartCoroutine(PayAndLeave());
    }

    private IEnumerator PayAndLeave()
    {
        // Wait for a second before leaving
        yield return new WaitForSeconds(1f);

        // Add money and remove customer
        CurrencyManager.Instance.AddMoney(MoneyPayed);
        CustomerControllerScript.RemoveCustomer(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void WantsDrink()
    {
        CanBeServed = true;
        if (equippedDrinks != null && equippedDrinks.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, equippedDrinks.Count);
            preferredDrink = equippedDrinks[randomIndex];
        }
        DisplayText.text = "Wants: " + (IngredientData.GetIngValue(preferredDrink.GetIngID(0))).DisplayName;

        //Enter state
        state = orderingState;
        state.Enter();
    }

    public void Serve()
    {
        // Only change display text to "Waiting for" at this point
        DisplayText.text = "Waiting for " + (IngredientData.GetIngValue(preferredDrink.GetIngID(0))).DisplayName;
        CanBeServed = false;
        // Note: The actual serving process will be triggered by ServeAfterCooldown
        state = waitingState;
        state.Enter();
    }

    public void ServeAfterCooldown()
    {
        // Stop any existing serving coroutine if it's running
        if (drinkServiceCoroutine != null)
        {
            StopCoroutine(drinkServiceCoroutine);
        }

        DisplayText.text = "Enjoying " + (IngredientData.GetIngValue(preferredDrink.GetIngID(0))).DisplayName;

        MoneyPayed += preferredDrink.GetCalculatedSellPrice();

        drinkServiceCoroutine = StartCoroutine(FillSatisfactionOverTime());

        //enter drinking state
        state = drinkingState;
        state.Enter();
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
            HealthBarSlider.value = Current_satisfaction;

            // Check if customer is fully satisfied during the process
            if (Current_satisfaction >= Max_satisfaction)
            {
                Pay();
                yield break; // Exit the coroutine early
            }

            yield return null; // Wait for the next frame
        }

        // Ensure we reach the exact target satisfaction at the end
        Current_satisfaction = endSatisfaction;
        HealthBarSlider.value = Current_satisfaction;

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
}