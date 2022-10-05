using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 100;
    [SerializeField]
    float speedChange=5f;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Image hpBar;
    float currenthealth;
    float newHealth;
    float timer = 0;
    private void Start()
    {
        player.GetComponent<Health>().Event_UpdateHP.AddListener(UpdateBar);
        currenthealth = 1;
        newHealth = maxHealth;
        currenthealth = newHealth;
        hpBar.fillAmount = 1;
    }

    void UpdateBar(int hp)
    {
        timer = 0;
        currenthealth = hpBar.fillAmount;
        newHealth=hp;

    }

    private void Update()
    {
        timer += Time.deltaTime * speedChange;
        hpBar.fillAmount = Mathf.Lerp(currenthealth, (newHealth/maxHealth), timer);
    }
}
