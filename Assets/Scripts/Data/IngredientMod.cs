using UnityEngine;

public class IngredientMod
{
  public ModifierType Type { get; }
  public string StatID { get; }
  public float Value { get; }

  public IngredientMod(ModifierType type, string statID, float value)
  {
    Type = type;
    StatID = statID;
    Value = value;
  }
}
