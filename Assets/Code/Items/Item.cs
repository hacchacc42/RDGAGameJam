using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Sprite image;

    public string itenName;
    [TextArea]
    public string bonus;

    public ItemType itemType;
    public Rarity rarity;


    [Header("Health")]
    public int maxHealth;
    public int regenHealth;

    [Header("Damage")]
    public int attackDamage;
    public int damage;
    public float projectileSpeed;
    public float attackSpeed;
    public float range;
    public int critChance;
    public int critDamage;
    public int lifeSteal;

    [Header("Utility")]
    public int movementSpeed;
    public int dodgeChance;
    public int luck;
}