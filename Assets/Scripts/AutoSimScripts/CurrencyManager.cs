using UnityEngine;
using System;
using System.IO;

public class CurrencyManager : MonoBehaviour
{
    // Events
    public static event Action<float> OnMoneyChanged;

    // Singleton instance
    private static CurrencyManager _instance;
    public static CurrencyManager Instance
    {
        get { return _instance; }
    }

    // Game data
    [SerializeField] private float _money = 0;
    public float Money
    {
        get { return _money; }
        private set
        {
            if (_money != value)
            {
                _money = value;
                OnMoneyChanged?.Invoke(_money);
             
            }
        }
    }

    private float startingMoney;

    private void Awake()
    {
        // Singleton pattern
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startingMoney = Money;
        // Trigger an initial money changed event to update any UI
        OnMoneyChanged?.Invoke(_money);
    }

    // Resets the money to whatever it was at the start
    public void ResetMoney()
    {
        Money = startingMoney;
    }

    // Add money
    public void AddMoney(float amount)
    {
        if (amount > 0)
        {
            Money += amount;
            Debug.Log($"Added {amount} money. New total: {Money}");
        }
    }

    // Spend money, returns true if successful
    public bool SpendMoney(float amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Cannot spend negative or zero money");
            return false;
        }

        if (Money >= amount)
        {
            Money -= amount;
            Debug.Log($"Spent {amount} money. Remaining: {Money}");
            return true;
        }
        else
        {
            Debug.LogWarning($"Not enough money to spend {amount}. Current money: {Money}");
            return false;
        }
    }

 
}