using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour
{
    [SerializeField]
    int damage = 20;
    GameObject target;
    NavMeshAgent agent;
    bool waiting = true;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    float speedInit;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        waiting = true;
        target = GameManager.instance.player;
        speedInit = agent.speed;

        StartCoroutine(InitialDelay());
    }


    private void Update()
    {
        if(waiting)
        {
            return;
        }
        if(target.transform.position.x>transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        if (target.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        agent.SetDestination(target.transform.position);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject?.GetComponent<Health>().ChangeHP(-damage);
            Bounce();
        }
    }

    public void Bounce()
    {
        StartCoroutine(C_Bounce(GameManager.instance.player.transform.position));
    }

    IEnumerator C_Bounce(Vector3 targetPos)
    {
        agent.ResetPath();
        agent.speed = 10;
        waiting = true;
        var opositeVector = targetPos - transform.position;
        agent.SetDestination(transform.position - opositeVector);
        yield return new WaitForSeconds(0.5f);
        agent.speed = speedInit;
        waiting =false;
    }

    IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(0.33f);
        waiting = false;
    }

    private void OnDisable()
    {
        GameManager.instance.SpawnHealthPotion(transform.position);
        GameManager.instance.DefeatedEnemy();
    }
}
