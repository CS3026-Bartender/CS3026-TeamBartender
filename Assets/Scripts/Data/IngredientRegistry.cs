using UnityEngine;

public class IngredientRegistry : MonoBehaviour
{
    [Header("Ingredient Sprites")]
    public Sprite iceSprite;
    public Sprite limeSprite;
    public Sprite mintSprite;

    [Header("Spirit Sprites")]
    public Sprite vodkaSprite;
    public Sprite rumSprite;

    void Awake()
    {
        RegisterIngredients();
        RegisterSpiritsWithTiers();
    }

    void RegisterIngredients()
    {
        IngredientData.AddIngredient(
            id: "ice",
            displayName: "Ice",
            price: 1f,
            sellPrice: 2f,
            desc: "Cold and crisp. Chills the drink.",
            sprite: iceSprite,
            serveTimeMod: 0.2f,
            customerDrinkTimeMod: 0f,
            potencyMod: 0f
        );

        IngredientData.AddIngredient(
            id: "lime",
            displayName: "Lime Juice",
            price: 2f,
            sellPrice: 3f,
            desc: "Tangy and zesty. Sharpens the drink.",
            sprite: limeSprite,
            serveTimeMod: 0.1f,
            customerDrinkTimeMod: 0.1f,
            potencyMod: 0f
        );

        IngredientData.AddIngredient(
            id: "mint",
            displayName: "Mint Leaves",
            price: 1.5f,
            sellPrice: 2.5f,
            desc: "Fresh and fragrant. Refreshes the senses.",
            sprite: mintSprite,
            serveTimeMod: 0.05f,
            customerDrinkTimeMod: 0.2f,
            potencyMod: 0f
        );
    }

    void RegisterSpiritsWithTiers()
    {
        IngredientData.AddSpiritWithTiers(
            id: "sp_vodka",
            displayName: "Vodka",
            price: 5f,
            sellPrice: 7f,
            desc: "A clean spirit with a strong kick.",
            sprite: vodkaSprite,
            serveTime: 1f,
            customerDrinkTime: 2f,
            potency: 3f,
            serveTimeMod: 0.1f,
            customerDrinkTimeMod: 0.2f,
            potencyMod: 0.5f
        );

        IngredientData.AddSpiritWithTiers(
            id: "sp_rum",
            displayName: "Rum",
            price: 4.5f,
            sellPrice: 6.5f,
            desc: "Sweet and dark, rich in flavor.",
            sprite: rumSprite,
            serveTime: 1.2f,
            customerDrinkTime: 2.2f,
            potency: 2.8f,
            serveTimeMod: 0.15f,
            customerDrinkTimeMod: 0.1f,
            potencyMod: 0.3f
        );
    }
}
