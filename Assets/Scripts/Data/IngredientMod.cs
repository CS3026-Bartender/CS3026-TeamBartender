using UnityEngine;

public class IngredientMod
{
  public ModifierType Type { get; private set; }
  public string StatID { get; private set; }
  public float Value { get; private set; }

  public IngredientMod(ModifierType type, string statID, float value)
  {
    Type = type;
    StatID = statID;
    Value = value;
  }
}
