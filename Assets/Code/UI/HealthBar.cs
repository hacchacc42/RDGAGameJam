using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField]
    TMP_Text currentHPText;
    [SerializeField]
    TMP_Text maxHPText;

    float currenthealth;
    float newHealth;
    float timer = 0;
    private void Start()
    {
        player.GetComponent<Health>().Event_UpdateHP.AddListener(UpdateBar);
        player.GetComponent<Health>().Event_UpdateMaxHP.AddListener(UpdateMaxHp);
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
        if(currentHPText!=null)
            currentHPText.text = hp.ToString();
    }

    void UpdateMaxHp(int maxHP)
    {
        maxHealth = maxHP;
        maxHPText.text = maxHP.ToString();
    }

    private void Update()
    {
        timer += Time.deltaTime * speedChange;
        hpBar.fillAmount = Mathf.Lerp(currenthealth, (newHealth/maxHealth), timer);
    }
}
