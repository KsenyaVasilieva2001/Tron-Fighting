using BehaviorTree;
using UnityEngine;

public class ActionRotateTowards : Node
{
    private EnemyBT enemyBT;

    public ActionRotateTowards(EnemyBT enemyBT)
    {
        this.enemyBT = enemyBT;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t is Transform player)
        {
            Vector3 direction = (player.position - enemyBT.transform.position).normalized;
            RotateTowards(direction);
        }
        state = NodeState.SUCCESS;
        return state;
    }
    protected void RotateTowards(Vector3 dir)
    {
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f)
            return;
        Quaternion targetRot = Quaternion.LookRotation(dir);
        enemyBT.transform.rotation = Quaternion.RotateTowards(enemyBT.transform.rotation, targetRot, 720f * Time.deltaTime);
    }
}