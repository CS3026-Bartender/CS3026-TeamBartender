using UnityEngine;
using UnityEngine.UI;
public class customer : MonoBehaviour
{
    public int Max_satisfaction = 100;
    public int Current_satisfaction;

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

    // Health bar images - only need one set of variables
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

        AssignRandomCustomerType();
        HealthBarSlider.value = Current_satisfaction;
    }

    private void AssignRandomCustomerType()
    {
        int typeIndex = Random.Range(0, 3);
        currentType = (CustomerType)typeIndex;
        SetCustomerType(currentType);
    }

    // Set customer type and update visuals accordingly
    public void SetCustomerType(CustomerType type)
    {
        currentType = type;

        // Make sure health bar images are not null before accessing
        if (healthBarImage != null && healthBarBackground != null)
        {
            // Set health bar color based on type
            switch (currentType)
            {
                case CustomerType.Regular:
                    healthBarImage.color = regularColor;
                    healthBarBackground.color = regularBackgroundColor;
                    break;
                case CustomerType.Premium:
                    healthBarImage.color = premiumColor;
                    healthBarBackground.color = premiumBackgroundColor;
                    break;
                case CustomerType.VIP:
                    healthBarImage.color = vipColor;
                    healthBarBackground.color = vipBackgroundColor;
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Cannot set customer type visuals: health bar images not assigned!");
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
        if (Current_satisfaction >= Max_satisfaction && CustomerControllerScript != null)
        {
            CustomerControllerScript.RemoveCustomer(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}