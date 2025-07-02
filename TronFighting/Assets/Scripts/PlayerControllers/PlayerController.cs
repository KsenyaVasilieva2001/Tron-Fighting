using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public UIController uiController;
    public Enemy currentEnemy;

    void Start()
    {
        animator = GetComponent<Animator>();
        GlobalEvents.OnPlayerEnterFightZone += PlayFightAnim;
        GlobalEvents.OnPlayerExitFightZone += PlayIdleAnim;
    }

    public void PlayFightAnim(EnemyBT enemy)
    {
        animator.Play("fight_idle");
    }

    public void PlayIdleAnim(EnemyBT enemy)
    {
        animator.Play("idle");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            currentEnemy = enemy;
            uiController.SetEnemyUI(enemy.data);
        }
    }
}
