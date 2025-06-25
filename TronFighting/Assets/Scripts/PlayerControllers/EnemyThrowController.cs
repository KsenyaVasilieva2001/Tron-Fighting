using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowController : ThrowControllerBase
{
    [SerializeField] private Transform target;
    [SerializeField] private float throwInterval = 3f;
    private float timer;
    private Vector3 direction;

    private void Update()
    {

        timer += Time.deltaTime;
        ShowTrack();
        if (timer >= throwInterval)
        {
            timer = 0f;
            StartCoroutine(ThrowRoutine());
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
}