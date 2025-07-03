using BehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public enum EnemyState
{
    THROW,
    WAIT_FOR_ATTACK,
    ATTACK
}
*/

public class EnemyBT : Tree
{
    public static float startFightRange = 2f;
    public static float attackRange = 1f;
    public bool isPlayerNear = false;
    public Animator anim;
    public EnemyThrowController throwController;
    public Health health;


    protected override void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
        throwController = GetComponent<EnemyThrowController>();

        health.OnDeath += HandleDeath;

        base.Start();
    }

    protected override Node SetupTree()
    {
        Debug.Log(throwController);
        Node root = 
        new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckPlayerInStartFightRange(this),
                new ActionRotateTowards(this),
                new CheckPlayerIsAttacking(),
                new ActionBlock()
            }),
            new Sequence(new List<Node>
            {
                new CheckPlayerInStartFightRange(this),
                new ActionRotateTowards(this),
                new CheckPlayerIsOpen(),
                new ActionAttack(this)
            }),
            new ActionThrowDisk(throwController),
        });
        return root;
    }

    public void OnPlayerEnterFightZone()
    {
        if (!isPlayerNear)
        {
            anim.Play("fight_idle");
            isPlayerNear = true;
            GlobalEvents.OnPlayerEnterFightZone?.Invoke(this);
        }
    }

    public void OnPlayerExitFightZone()
    {
        if (isPlayerNear)
        {
            anim.Play("idle");
            isPlayerNear = false;
            GlobalEvents.OnPlayerExitFightZone?.Invoke(this);
        }
    }

    private void HandleDeath()
    {
        anim.Play("die");
        Vector3 pos = transform.position;
        pos.y -= 100f;
        transform.position = pos;
        GlobalEvents.OnPlayerWin?.Invoke(this);
        Destroy(gameObject);
    }
}
