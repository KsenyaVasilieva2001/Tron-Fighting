using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBT : Tree
{
    public static float startFightRange = 6f;
    public static float attackRange = 1f;
    protected override Node SetupTree()
    {
        Node root = 
        new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckPlayerInStartFightRange(),
                new CheckPlayerInAttackRange(),
                new CheckPlayerIsAttacking(),
                new ActionBlock()
            }),
            new Sequence(new List<Node>
            {
                new CheckPlayerInStartFightRange(),
                new CheckPlayerInAttackRange(),
                new ConditionPlayerIsOpen(),
                new ActionAttack()
            }),
            new Sequence(new List<Node>
            {
                new CheckPlayerInStartFightRange(),
            }),
            new ActionThrowDisk(),
        });

        return root;
    }
}
