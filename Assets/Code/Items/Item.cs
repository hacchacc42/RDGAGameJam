using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    bool playerItem = true;
    public Sprite image;

    public ItemType itemType;
    public Rarity rarity;


    [Header("Health")]
    public int maxHealth;
    [SerializeField]
    int initialMaxHealh;
    public int regenHealth;
    [SerializeField]
    int initialRegenHealth;

    [Header("Damage")]
    public int damage;
    [SerializeField]
    int initialDamage;
    public float projectileSpeed;
    [SerializeField]
    float initialProjectileSpeed;
    public float attackSpeed;
    [SerializeField]
    float initialAttackSpeed;
    public float range;
    [SerializeField]
    float initialRange;
    public int critChance;
    [SerializeField]
    int initialCritChance;
    public int critDamage;
    [SerializeField]
    int initialCritDamage;
    public int lifeSteal;
    [SerializeField]
    int initialLifeSteal;

    [Header("Utility")]
    public float movementSpeed;
    [SerializeField]
    float initialMovementSpeed;
    public int dodgeChance;
    [SerializeField]
    int initialDodgeChance;
    public int luck;
    [SerializeField]
    int initialLuck;

    public void SetBackgroundColor(Image backgorund)
    {
        switch (rarity)
        {
            case Rarity.Common:
                backgorund.color = Color.white;
                break;
            case Rarity.Uncommon:
                backgorund.color = Color.green;
                break;
            case Rarity.Rare:
                backgorund.color = Color.blue;
                break;
            case Rarity.Epic:
                backgorund.color = Color.magenta;
                break;
            case Rarity.Legendary:
                backgorund.color = Color.yellow;
                break;
            default:
                break;
        }
    }

    public void UpdateItemStats()
    {
        if (!playerItem)
            return;
        var player = GameManager.instance.player.GetComponent<Stats>();
        attackSpeed = initialAttackSpeed*((player.attackSpeed*100.0f)/100.0f);
    }
}