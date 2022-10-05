using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Taur : BossPhases
{
    NavMeshAgent agent;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public override void Phase2()
    {
        base.Phase2();
        agent.ResetPath();
        agent.speed = 5.5f;
    }

    public override void Phase3()
    {
        base.Phase3();
        agent.ResetPath();
        agent.speed = 6;
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
