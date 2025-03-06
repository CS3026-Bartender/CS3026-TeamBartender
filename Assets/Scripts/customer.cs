using UnityEngine;
using UnityEngine.UI;

public class customer : MonoBehaviour
{

    public int Max_satisfaction = 100;
    public int Current_satisfaction;

    private Image HealthBarImage;
    private Image HealthBarBackground;


    public enum CustomerType
    {
        Regular,    // Type 0
        Premium,    // Type 1
        VIP         // Type 2
    }

    [SerializeField] private CustomerType currentType;

    // Colors for each customer type
    [SerializeField] private Color regularColor;
    [SerializeField] private Color regularBackgroundColor;
    [SerializeField] private Color premiumColor;
    [SerializeField] private Color premiumBackgroundColor;
    [SerializeField] private Color vipColor;
    [SerializeField] private Color vipBackgroundColor;


    private CustomerController CustomerControllerScript;

    public Slider HealthBarSlider;


    private void Awake()
    {

        //Setup Healthbar
        Image[] images = GetComponentsInChildren<Image>();
        HealthBarImage = images[1];
        HealthBarBackground = images[0];
        HealthBarSlider.maxValue = Max_satisfaction;

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
        HealthBarSlider.value = Current_satisfaction;
        AssignRandomCustomerType(); 
    }

    private void AssignRandomCustomerType()
    {
        int typeIndex = Random.Range(0, 3);
        SetCustomerType((CustomerType)typeIndex);
    }

    // Set customer type and update visuals accordingly
    public void SetCustomerType(CustomerType type)
    {
        currentType = type;

        // Set health bar color based on type
        switch (currentType)
        {
            case CustomerType.Regular:
                HealthBarImage.color = regularColor;
                HealthBarBackground.color = regularBackgroundColor;
                break;
            case CustomerType.Premium:
                HealthBarImage.color = premiumColor;
                HealthBarBackground.color = premiumBackgroundColor;
                break;
            case CustomerType.VIP:
                HealthBarImage.color = vipColor;
                HealthBarBackground.color = vipBackgroundColor;

                break;
        }
    }

    public CustomerType GetCustomerType()
    {
        return currentType;
    }

    public void IncreaseSatisfaction(int Satisfaction)
    {
        Current_satisfaction += Satisfaction;
        HealthBarSlider.value = Current_satisfaction;
        if (Current_satisfaction >= Max_satisfaction) CustomerControllerScript.RemoveCustomer(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
