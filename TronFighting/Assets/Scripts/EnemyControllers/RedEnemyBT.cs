using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemyBT : Tree
{
    public Animator anim;
    public EnemyThrowController throwController;


    protected override void Start()
    {
        anim = GetComponent<Animator>();
        throwController = GetComponent<EnemyThrowController>();

        base.Start();
    }

    protected override Node SetupTree()
    {
        Debug.Log(throwController);
        Node root =
        new Selector(new List<Node>
        {
            new ActionThrowDisk(throwController),
        });
        return root;
    }
}
