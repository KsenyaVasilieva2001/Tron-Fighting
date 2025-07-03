using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public EnemyBT currentEnemy;
    public Health health;
    public bool IsInFightZone;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        health.OnDeath += HandleDeath;
        GlobalEvents.OnPlayerEnterFightZone += HandleEnterFightZone;
        GlobalEvents.OnPlayerExitFightZone += HandleExitFightZone;
        GlobalEvents.OnPlayerWin += HandleWin;
    }

    private void Update()
    {
        if (IsInFightZone) RotateTowards(currentEnemy);
    }

    public void HandleEnterFightZone(EnemyBT enemy)
    {
        animator.Play("fight_idle");
        IsInFightZone = true;
        currentEnemy = enemy;
    }

    public void HandleExitFightZone(EnemyBT enemy)
    {
        animator.Play("idle");
        IsInFightZone = false;
    }

    public void HandleWin(EnemyBT enemy)
    {
        animator.Play("victory");
    }

    public void HandleDeath()
    {
        animator.Play("die");
        Vector3 pos = transform.position;
        pos.y -= 100f;
        transform.position = pos;
    }

    protected void RotateTowards(EnemyBT enemy)
    {
        var direction = (enemy.transform.position - transform.position).normalized;
        direction.y = 0f;
        if (direction.sqrMagnitude < 0.0001f)
            return;
        Quaternion targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 720f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.TryGetComponent(out Enemy enemy))
        {
            currentEnemy = enemy;
            uiController.SetEnemyUI(enemy.data);
        }
        */
    }
}
