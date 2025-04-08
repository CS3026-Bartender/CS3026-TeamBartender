using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class customer : MonoBehaviour
{
    public int Max_satisfaction = 100;
    public float Current_satisfaction;
    public bool CanbeServed = true;
    private Drink preferredDrink;



    // Colors for each customer type
    [SerializeField] private Color regularColor;
    [SerializeField] private Color regularBackgroundColor;
   
    private List<Drink> equippedDrinks = DrinkData.Instance.GetAllDrinksAsList();


    private CustomerController CustomerControllerScript;
    public Slider HealthBarSlider;

    [SerializeField] private int MoneyPayed;

    // Health bar images 
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Image healthBarBackground;

    private void Awake()
    {
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

        WantsDrink();
    }

    

    public void IncreaseSatisfaction(float Satisfaction)
    {
        Current_satisfaction += Satisfaction;
        HealthBarSlider.value = Current_satisfaction;
        if (Current_satisfaction >= Max_satisfaction && CustomerControllerScript != null)
        {
            CustomerControllerScript.RemoveCustomer(gameObject);
            CurrencyManager.Instance.AddMoney(MoneyPayed);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void WantsDrink()
    {
        CanbeServed = true;

        if (equippedDrinks != null && equippedDrinks.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, equippedDrinks.Count);
            preferredDrink = equippedDrinks[randomIndex];
        }
    }

    public void Serve()
    {
        CanbeServed = false;

    }
}