using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        health = maxHealth;
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
