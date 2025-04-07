using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class DataReader : Manager<DataReader>
{
    [SerializeField] private string spritesFolderPath;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private TextAsset ingredientsFile; // id,displayName,price,description
    [SerializeField] private TextAsset spiritsFile; // id,displayName,price,description,serveTime,potency

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

        IngredientData.AddIngredient(values[0], values[1], price, values[3], FindSprite(values[0]));
    }

    private void LoadSpirit(string[] values)
    {
        float.TryParse(values[2], out float price);
        float.TryParse(values[4], out float serveTime);
        float.TryParse(values[5], out float potency);

        IngredientData.AddSpirit(values[0], values[1], price, values[3], FindSprite(values[0]), serveTime, potency);
    }

    private Sprite FindSprite(string id)
    {
        string filePath = spritesFolderPath + "/" + id;
        Sprite sprite;
        sprite = Resources.Load<Sprite>(filePath);
        if (sprite == null ) {
            sprite = defaultSprite;
        }
        return sprite;
    }
}
