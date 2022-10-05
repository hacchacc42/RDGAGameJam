using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Experience : MonoBehaviour
{
    [SerializeField]
    int lvl;
    [SerializeField]
    float expereinceRequired;
    [SerializeField]
    float currentExperience;

    int maxLvl = 10;

    void Start()
    {
        lvl = 0;
        expereinceRequired = 100;
        currentExperience = 0;
    }

    public void AddExperience(float experience)
    {
        if (lvl > maxLvl)
        {
            currentExperience = 0;

            return;
        }
        currentExperience += experience;
        while (currentExperience >= expereinceRequired)
        {
            LevelUp();
            currentExperience -= expereinceRequired;
            expereinceRequired = Mathf.Floor(100 * Mathf.Pow(1.25f, lvl));
        }
    }

    void LevelUp()
    {
        lvl++;
    }

}
