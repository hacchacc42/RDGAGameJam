using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGiver : MonoBehaviour
{
    [SerializeField]
    int experienceGiving;
    private void Start()
    {
        GetComponent<Health>().Event_Rip.AddListener(GiveExperience);
    }
    
    void GiveExperience()
    {
        GameManager.instance.GetComponent<PlayerLevel>().AddExperience(experienceGiving);
    }
}
