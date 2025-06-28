using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyThrowController : ThrowControllerBase
{
    [SerializeField] private Transform target;
    [SerializeField] private float throwInterval = 3f;
    [SerializeField] private EnemyController enemyController;
    private float timer;

    protected override void Start()
    {
        base.Start();
        enemyController.OnActivateThrow += Activate;
        enemyController.OnDeactivateThrow += Deactivate;
    }

    private void Update()
    {
        if (IsActive())
        {
            timer += Time.deltaTime;
            ShowTrack();
            if (timer >= throwInterval)
            {
                timer = 0f;
                StartCoroutine(ThrowRoutine());
            }
        }
    }
    private IEnumerator ThrowRoutine()
    {
        anim.SetBool("IsAimToThrow", true);
        ShowTrack();
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Throw");
        anim.SetBool("IsAimToThrow", false);
        pathTracker.Clear();
    }
    protected override void ShowTrack()
    {
        Vector3 direction = (target.position - firePoint.position).normalized;
        RotateTowards(direction);
        path = pathTracker.CalculatePath(firePoint.position, direction, reflections, maxDistance);
    }

    void OnDestroy()
    {
        enemyController.OnActivateThrow -= Activate;
        enemyController.OnDeactivateThrow -= Deactivate;
    }
}