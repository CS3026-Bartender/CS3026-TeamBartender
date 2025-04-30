using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
public class DataReader : Manager<DataReader>
{
    [SerializeField] private string spritesFolderPath;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private TextAsset ingredientsFile; // id,displayName,price,sellPrice,description,statID,statMod,isMult
    [SerializeField] private TextAsset spiritsFile; // id,displayName,price,sellPrice,description,serveTime,customerDrinkTime,potency,drinkTime
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
            if (lines[i] == "") return;
            string[] values = lines[i].Split(',');
            action.Invoke(values);
        }
    }
    private void LoadIngredient(string[] values)
    {
        string id = values[0];
        Sprite sprite = FindSprite(id);
        string name = values[1];
        float.TryParse(values[2], out float price);
        float.TryParse(values[3], out float sellPrice);
        string desc = values[4];
        string statID = values[5];
        float.TryParse(values[6], out float statMod);
        bool.TryParse(values[7], out bool isMult);
        
        IngredientData.AddIngredient(id, name, price, sellPrice, values[4], sprite,
                                     statID, statMod, isMult);
    }
    private void LoadSpirit(string[] values)
    {
        float.TryParse(values[2], out float price);
        float.TryParse(values[3], out float sellPrice);
        float.TryParse(values[5], out float serveTime);
        float.TryParse(values[6], out float customerDrinkTime);
        float.TryParse(values[7], out float potency);
        float.TryParse(values[8], out float drinkPrice);
        // For spirits, we're not passing modifiers as you mentioned
        IngredientData.AddSpirit(values[0], values[1], price, sellPrice, values[4], FindSprite(values[0]),
                                 serveTime, customerDrinkTime, potency, drinkPrice);
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