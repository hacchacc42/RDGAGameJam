using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerLevel : MonoBehaviour
{
    public int level;
    [SerializeField]
    float experienceRequired;
    [SerializeField]
    float currentExperience;
    [Header("UI")]
    [SerializeField]
    Image experienceUI;
    [SerializeField]
    TMP_Text levelText;

    void Start()
    {
        level = 1;
        experienceRequired = 100;
        currentExperience = 0;
        experienceUI.fillAmount = 0;
    }
    public void AddExperience(int experience)
    {
        currentExperience += experience;
        while (currentExperience >= experienceRequired)
        {
            LevelUp();
            currentExperience -= experienceRequired;
            experienceRequired = Mathf.Floor(100 * Mathf.Pow(1.25f, level));
        }
        experienceUI.fillAmount = currentExperience / experienceRequired;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            AddExperience(20);
        }
    }

    void LevelUp()
    {
        level++;
        levelText.text = level.ToString();
    }
}
