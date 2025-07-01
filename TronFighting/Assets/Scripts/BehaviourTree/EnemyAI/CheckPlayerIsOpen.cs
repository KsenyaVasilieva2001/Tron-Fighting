using BehaviorTree;
using UnityEngine;

public class CheckPlayerIsOpen : Node
{

    public CheckPlayerIsOpen()
    {
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");
        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform player = (Transform)target;
        FightController playerFight = player.GetComponent<FightController>();

        if (playerFight != null && !playerFight.IsBlocking())
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}