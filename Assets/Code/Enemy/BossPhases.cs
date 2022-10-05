using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhases : MonoBehaviour
{
    [SerializeField]
    float maxHp = 100;
    [SerializeField]
    Health healthComp;
    [SerializeField]
    GameObject phase1;
    [SerializeField]
    GameObject phase2;
    [SerializeField]
    GameObject phase3;
    [SerializeField]
    bool inPhase2 = false;
    [SerializeField]
    bool inPhase3 = false;
    [SerializeField]
    LevelTrigger levelTrigger;

    private void Start()
    {
        GetComponent<AudioSource>().Play();
        healthComp.Event_UpdateHP.AddListener(TrackHp);
        maxHp = healthComp.maxHealth;
        inPhase2 = false;
        inPhase3 = false;
        GameManager.instance.numberOfEnemies++;
    }

    public void TrackHp(int hp)
    {
        if(hp<=0)
        {
            levelTrigger.OpenGate();
            GameManager.instance.OpenBook(GameManager.instance.level / 2 - 1);
        }
        float percentage = hp / maxHp;
        if(percentage <.33f)
        {
            if (!inPhase3)
            {
                Phase3();
                inPhase3 = true;
            }
        }
        else if(percentage<.66f)
        {
            if(!inPhase2)
            {
                Phase2();
                inPhase2 = true;
            }
        }
    }

    public virtual void Phase2()
    {
        if (phase1!=null)
        {
            phase1.SetActive(false);
        }
        phase2.SetActive(true);
    }

    public virtual void Phase3()
    {
        phase2.SetActive(false);
        phase3.SetActive(true);
    }
}
