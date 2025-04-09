using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
public class DataReader : Manager<DataReader>
{
    [SerializeField] private string spritesFolderPath;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private TextAsset ingredientsFile; // id,displayName,price,sellPrice,description,serveTimeMod,customerDrinkTimeMod,potencyMod
    [SerializeField] private TextAsset spiritsFile; // id,displayName,price,sellPrice,description,serveTime,customerDrinkTime,potency
    public void LoadIngredients()
    {
        LoadFile(ingredientsFile, LoadIngredient);
        LoadFile(spiritsFile, LoadSpirit);
    }
    private void LoadFile(TextAsset file, UnityAction<string[]> action)
    {
        string[] lines = file.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            action.Invoke(values);
        }
    }
    private void LoadIngredient(string[] values)
    {
        float.TryParse(values[2], out float price);
        float.TryParse(values[3], out float sellPrice);
        float serveTimeMod = 0f;
        float customerDrinkTimeMod = 0f;
        float potencyMod = 0f;
        // Parse modifiers if they exist in the data
        if (values.Length > 5 && !string.IsNullOrEmpty(values[5]))
            float.TryParse(values[5], out serveTimeMod);
        if (values.Length > 6 && !string.IsNullOrEmpty(values[6]))
            float.TryParse(values[6], out customerDrinkTimeMod);
        if (values.Length > 7 && !string.IsNullOrEmpty(values[7]))
            float.TryParse(values[7], out potencyMod);
        IngredientData.AddIngredient(values[0], values[1], price, sellPrice, values[4], FindSprite(values[0]),
                                     serveTimeMod, customerDrinkTimeMod, potencyMod);
    }
    private void LoadSpirit(string[] values)
    {
        float.TryParse(values[2], out float price);
        float.TryParse(values[3], out float sellPrice);
        float.TryParse(values[5], out float serveTime);
        float.TryParse(values[6], out float customerDrinkTime);
        float.TryParse(values[7], out float potency);
        // For spirits, we're not passing modifiers as you mentioned
        IngredientData.AddSpirit(values[0], values[1], price, sellPrice, values[4], FindSprite(values[0]),
                                 serveTime, customerDrinkTime, potency);
    }
    private Sprite FindSprite(string id)
    {
        string filePath = spritesFolderPath + "/" + id;
        Sprite sprite;
        sprite = Resources.Load<Sprite>(filePath);
        if (sprite == null)
        {
            sprite = defaultSprite;
        }
        return sprite;
    }
}