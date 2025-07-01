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
        PlayerFightController playerFight = player.GetComponent<PlayerFightController>();

        if (playerFight != null && !playerFight.IsBlocking())
        {
            Debug.Log("Player is open");
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}