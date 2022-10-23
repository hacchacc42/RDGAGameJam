using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem instance;

    public Tooltip tooltip;

    public static Item item;
    private void Awake()
    {
        instance = this;
    }

    public static void Show()
    {
        GetTitleColor();
        instance.tooltip.header.text = item.name;
        instance.tooltip.text.text = GetDescription();
        instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        instance.tooltip.gameObject.SetActive(false);
        item = null;
    }

    static void GetTitleColor()
    {
        switch(item.rarity)
        {
            case (Rarity.Common):
                instance.tooltip.header.color = Color.white;
                break;
            case (Rarity.Uncommon):
                instance.tooltip.header.color = Color.green;
                break;
            case (Rarity.Rare):
                instance.tooltip.header.color = Color.blue;
                break;
            case (Rarity.Epic):
                instance.tooltip.header.color = Color.magenta;
                break;
            case (Rarity.Legendary):
                instance.tooltip.header.color = Color.yellow;
                break;
            default:
                break;

        }
    }

    static string GetDescription()
    {
        string description = "";

        if (item.maxHealth != 0)
            description += $"+ {item.maxHealth} Max Health\n";
        if (item.regenHealth != 0)
            description += $"+ {item.regenHealth} Health Regen\n";
        if (item.damage != 0)
        {
            if (item.itemType == ItemType.Weapon)
            {
                description += $"{item.damage} Damage\n";
            }
            else
            {
                description += $"+ {item.damage} Damage\n";
            }
        }
        if(item.projectileSpeed!=0)
        {
            if (item.itemType == ItemType.Weapon)
            {
                description += $"{item.projectileSpeed} Projectile Speed\n";
            }
            else
            {
                description += $"+ {item.projectileSpeed} Projectile Speed\n";
            }
        }
        if (item.attackSpeed != 0)
        {
            if (item.itemType == ItemType.Weapon)
            {
                description += $"{item.attackSpeed} Attack Speed\n";
            }
            else
            {
                description += $"+ {item.attackSpeed} Attack Speed\n";
            }
        }
        if (item.range != 0)
            description += $"{item.range} Range\n";
        if (item.critChance != 0)
        {
            if (item.itemType == ItemType.Weapon)
            {
                description += $"{item.critChance} Crit Chance\n";
            }
            else
            {
                description += $"+ {item.critChance} Crit Chance\n";
            }
        }
        if (item.critDamage != 0)
        {
            if (item.itemType == ItemType.Weapon)
            {
                description += $"{item.critDamage} Crit Damage\n";
            }
            else
            {
                description += $"+ {item.critDamage} Crit Damage\n";
            }
        }
        if (item.lifeSteal != 0)
        {
            if (item.itemType == ItemType.Weapon)
            {
                description += $"{item.lifeSteal} Lifesteal\n";
            }
            else
            {
                description += $"+ {item.lifeSteal} LifeSteal\n";
            }
        }
        if (item.movementSpeed != 0)
            description += $"+ {item.movementSpeed} Movement Speed\n";
        if (item.dodgeChance != 0)
            description += $"+ {item.dodgeChance} Movement Speed\n";
        if (item.luck != 0)
            description += $"+ {item.luck} Luck\n";
        return description;
    }

}
