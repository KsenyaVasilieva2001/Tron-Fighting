using BehaviorTree;
using UnityEngine;

public class ActionAttack : Node
{
    private EnemyBT _enemy;
    private FightController _fightController;
    private float _attackInterval = 2f;
    private float _timer;

    public ActionAttack(EnemyBT enemy)
    {
        _enemy = enemy;
        _fightController = enemy.GetComponent<FightController>();
    }

    public override NodeState Evaluate()
    {
        if (!_enemy.isPlayerNear)
        {
            state = NodeState.FAILURE;
            return state;
        }

        _timer += Time.deltaTime;

        if (_timer >= _attackInterval)
        {
            _timer = 0f;

            Attack randomAttack = GetRandomAttack();
            if (randomAttack != null)
            {
                _fightController.Attack(randomAttack);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

    private Attack GetRandomAttack()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                return _fightController.lightAttack;
            case 1:
                return _fightController.heavyAttack;
            case 2:
                return _fightController.kickAttack;
            default:
                return null;
        }
    }
}
