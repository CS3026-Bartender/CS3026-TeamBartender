using UnityEngine;

public enum ModifierType
{
  Add,
  Multiply
}

public class IngredientMod
{
  public ModifierType ModifierType { get; private set; }
  public string StatID { get; private set; }
  public float ModifierValue { get; private set; }

  public IngredientMod(ModifierType modifierType, string statID, float modifierValue)
  {
    ModifierType = modifierType;
    StatID = statID;
    ModifierValue = ModifierValue;
  }
}
