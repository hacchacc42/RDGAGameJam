using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField]
    bool canAdvance = false;

    private void OnEnable()
    {
        GameManager.instance.clearedEnemiesEvent.AddListener(OpenGate);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if(canAdvance)
            {
                canAdvance = false;
                GetComponent<Animator>().SetBool("CanEnter", false);
                GameManager.instance.EnterNewLevel();
            }
        }
    }
    public void OpenGate()
    {
        canAdvance = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Animator>().SetBool("CanEnter", true);
    }
}
