using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class myIntEvent : UnityEvent<int>
{ }
[System.Serializable]
public class myFloatEvent : UnityEvent<float>
{ }

public class Stats : MonoBehaviour
{
    [SerializeField]
    Health player;
    [SerializeField]
    Inventory inventory;
    [Header("Health")]
    public int maxHealth;
    public int regenHealth;

    public myIntEvent updateMaxHealth;
    public myIntEvent updateRegenHealth;

    public TMP_Text maxHealthText;
    public TMP_Text regenHealthText;

    [Header("Damage")]
    public int attackDamage;
    public float attackSpeed;
    public int critChance;
    public int critDamage;
    public int lifeSteal;

    public UnityEvent updateAttackDamage;
    public UnityEvent updateAttackSpeed;
    public UnityEvent updateCritChance;
    public UnityEvent updateCritDamage;
    public UnityEvent updateLifeSteal;

    public TMP_Text attackDamageText;
    public TMP_Text attackSpeedText;
    public TMP_Text critChanceText;
    public TMP_Text critDamageText;
    public TMP_Text lifeStealText;


    [Header("Utility")]
    public float movementSpeed;
    public int dodgeChance;
    public int luck;

    public myFloatEvent updateMovementSpeed;
    public myIntEvent updateDodgeChance;
    public myIntEvent updateLuck;

    public TMP_Text movementSpeedText;
    public TMP_Text dodgeChanceText;
    public TMP_Text luckText;

    private void Start()
    {
        updateMaxHealth.AddListener(player.UpdateMaxHealth);
        updateRegenHealth.AddListener(player.UpdateHealthRegen);

        updateAttackSpeed.AddListener(inventory.UpdateWeaponValues);

        updateMovementSpeed.AddListener(player.GetComponent<PlayerMovement>().UpdateMovementSpeed);

        Refresh();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            UpdateDamageValues(0, 1, 0, 0, 0);
        }
    }

    public void UpdateHealthValues(int cMaxHealth, int cRegenHealth)
    {
        maxHealth += cMaxHealth;
        regenHealth += cRegenHealth;

        updateMaxHealth.Invoke(maxHealth);
        updateRegenHealth.Invoke(regenHealth);

        maxHealthText.text = maxHealth.ToString();
        regenHealthText.text = regenHealth.ToString();
    }

    public void UpdateUtilityValues(float cMovementSpeed, int cDodgeChance, int cLuck)
    {
        movementSpeed += cMovementSpeed;
        dodgeChance += cDodgeChance;
        luck += cLuck;

        updateMovementSpeed.Invoke(movementSpeed);

        movementSpeedText.text = movementSpeed.ToString();
        dodgeChanceText.text = dodgeChance.ToString();
        luckText.text = luck.ToString();
    }

    public void UpdateDamageValues(int cAttackDamage, float cAttackSpeed, int cCritChance, int cCritDamage, int cLifeSteal)
    {
        attackDamage += cAttackDamage;
        attackSpeed += cAttackSpeed;
        critChance += cCritChance;
        critDamage += cCritDamage;
        lifeSteal += cLifeSteal;

        updateAttackSpeed.Invoke();

        attackDamageText.text = attackDamage.ToString();
        attackSpeedText.text = attackSpeed.ToString();
        critChanceText.text = critChance.ToString();
        critDamageText.text = critDamage.ToString();
        lifeStealText.text = luck.ToString();

    }

    public void Refresh()
    {
        UpdateHealthValues(0, 0);
        UpdateUtilityValues(0, 0, 0);
        UpdateDamageValues(0, 0, 0, 0, 0);
    }
}