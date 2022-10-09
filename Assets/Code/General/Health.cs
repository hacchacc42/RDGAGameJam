using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [System.Serializable]
    public class myIntEvent : UnityEvent<int>
    { }
    public myIntEvent Event_UpdateHP;
    public int maxHealth = 100;
    [SerializeField]
    int health;
    [SerializeField]
    int healthRegen;
    [SerializeField]
    float healthRegenTime = 2f;

    private void Start()
    {
        health = maxHealth;
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


    public void ChangeHP(int ammount)
    {
        health += ammount;
        Event_UpdateHP.Invoke(health);
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
