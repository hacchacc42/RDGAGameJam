using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public myIntEvent Event_UpdateHP;
    public myIntEvent Event_UpdateMaxHP;
    public UnityEvent Event_Rip;
    public int maxHealth = 100;
    [SerializeField]
    int health;
    [SerializeField]
    int healthRegen;
    [SerializeField]
    float healthRegenTime = 2f;
    [Header("Extra Stats")]
    [SerializeField]
    int luck;
    [SerializeField]
    int dodgeChance = 5;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangeHP(-20);
        }
    }

    private void OnEnable()
    {
        if(healthRegen>0)
        {
            StartCoroutine(HealthRegen());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator HealthRegen()
    {
        ChangeHP(healthRegen);
        yield return new WaitForSeconds(healthRegenTime);
        StartCoroutine(HealthRegen());
    }


    public void UpdateMaxHealth(int nMaxHealth)
    {
        int tempHP = nMaxHealth - maxHealth;
        maxHealth = nMaxHealth;
        ChangeHP(tempHP);
        Event_UpdateMaxHP.Invoke(maxHealth);
    }
    public void UpdateHealthRegen(int nHealthRegen)
    {
        healthRegen = nHealthRegen;
    }

    public void ChangeHP(int ammount)
    {
        if (ammount < 0)
        {
            if ((dodgeChance + (luck / 2)) >= Random.Range(0, 100)) // Dodge
                return;
        }
        health += ammount;
        if (health > maxHealth)
        {
            health = maxHealth;
            Event_UpdateHP.Invoke(health);
            return;
        }
        Event_UpdateHP.Invoke(health);
        if (health <= 0)
        {
            Event_Rip.Invoke();
            gameObject.SetActive(false);
        }
    }
}
