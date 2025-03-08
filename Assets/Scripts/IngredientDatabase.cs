using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "IngredientDatabase", menuName = "Game/IngredientDatabase")]
public class IngredientDatabase : ScriptableObject
{
    public List<Ingredient> drinks = new List<Ingredient>();
}
