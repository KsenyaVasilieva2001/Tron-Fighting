using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBT : Tree
{
    public static float startFightRange = 2.5f;
    public static float attackRange = 1f;
    public bool isPlayerNear = false;
    public Animator anim;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }
    protected override Node SetupTree()
    {
        Node root = 
        new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckPlayerInStartFightRange(this),
                new CheckPlayerInAttackRange(transform),
                //new CheckPlayerIsAttacking(),
                //new ActionBlock()
            }),
            new Sequence(new List<Node>
            {
                new CheckPlayerInStartFightRange(this),
                new CheckPlayerInAttackRange(transform),
                //new ConditionPlayerIsOpen(),
                //new ActionAttack()
            }),
            new ActionThrowDisk(),
        });
        return root;
    }

    public void OnPlayerEnterFightZone()
    {
        if (!isPlayerNear)
        {
            anim.Play("fight_idle");
            isPlayerNear = true;
            Debug.Log("Игрок вошел в радиус врага");
        }
    }

    public void OnPlayerExitFightZone()
    {
        if (isPlayerNear)
        {
            anim.Play("idle");
            isPlayerNear = false;
            Debug.Log("Игрок покинул радиус врага");
        }
    }
}
