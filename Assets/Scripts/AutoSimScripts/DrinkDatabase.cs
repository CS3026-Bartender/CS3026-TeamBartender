using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DrinkDatabase", menuName = "Game/Drink Database")]
public class DrinkDatabase : ScriptableObject
{
    public List<Drink> drinks = new List<Drink>();
}


