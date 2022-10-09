using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class myIntEvent : UnityEvent<int>
{ }

public class Stats : MonoBehaviour
{

    [Header("Health")]
    [SerializeField]
    Health player;
    public int maxHealth;
    public int regenHealth;

    public myIntEvent updateMaxHealth;
    public myIntEvent updateRegenHealth;


    [Header("Damage")]
    public int attackDamage;
    public int attackSpeed;
    public int critChance;
    public int critDamage;
    public int lifeSteal;

    public myIntEvent updateAttackDamage;
    public myIntEvent updateAttackSpeed;
    public myIntEvent updateCritChance;
    public myIntEvent updateCritDamage;
    public myIntEvent updateLifeSteal;


    [Header("Utility")]
    public int movementSpeed;
    public int dodgeChance;
    public int luck;

    public myIntEvent updateMovementSpeed;
    public myIntEvent updateDodgeChance;
    public myIntEvent updateLuck;

    private void Start()
    {
        updateMaxHealth.AddListener(player.UpdateMaxHealth);
        updateRegenHealth.AddListener(player.UpdateHealthRegen);


    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            UpdateHealthValues(0, 10);
        }
    }

    public void UpdateHealthValues(int cMaxHealth, int cRegenHealth)
    {
        maxHealth += cMaxHealth;
        regenHealth += cRegenHealth;

        updateMaxHealth.Invoke(maxHealth);
        updateRegenHealth.Invoke(regenHealth);
    }

    public void UpdateUtilityValues(int cMovementSpeed, int cDodgeChance, int cLuck)
    {
        movementSpeed += cMovementSpeed;
        dodgeChance += cDodgeChance;
        luck += cLuck;

    }

    public void UpdateDamageValues(int cAttackDamage, int cAttackSpeed, int cCritChance, int cCritDamage, int cLifeSteal)
    {
        attackDamage += cAttackDamage;
        attackSpeed += cAttackSpeed;
        critChance += cCritChance;
        critDamage += cCritDamage;
        lifeSteal += cLifeSteal;
    }

    public void Refresh()
    {
        UpdateHealthValues(0, 0);
        UpdateUtilityValues(0, 0, 0);
        UpdateDamageValues(0, 0, 0, 0, 0);
    }
}