using UnityEngine;

public enum ModifierType
{
  Additive,
  Multiplicative
}

public class IngredientMod
{
  public ModifierType ModifierType { get; }
  public string StatID { get; }
  public float Value { get; }

  public IngredientMod(ModifierType modifierType, string statID, float value)
  {
    ModifierType = modifierType;
    StatID = statID;
    Value = value;
  }
}
