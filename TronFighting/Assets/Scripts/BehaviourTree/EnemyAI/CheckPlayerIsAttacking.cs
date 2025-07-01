using BehaviorTree;
using UnityEngine;

public class CheckPlayerIsAttacking : Node
{

    public CheckPlayerIsAttacking()
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
        PlayerFightController playerFight = player.GetComponent<PlayerFightController>();

        if (playerFight != null && playerFight.IsAttacking())
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}