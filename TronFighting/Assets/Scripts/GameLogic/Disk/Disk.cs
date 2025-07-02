using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damageAmount = 10;
    private Queue<Vector3> _trackPoints;
    private bool isDestroyed;

    public void Init(Vector3[] trackPoints)
    {
        _trackPoints = new Queue<Vector3>(trackPoints);
        transform.position = _trackPoints.Dequeue();
    }

    private void Update()
    {
        MoveToPoint();
        DestroyDisk();
    }

    private void MoveToPoint()
    {
        Vector3 target = _trackPoints.Peek();
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            _trackPoints.Dequeue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IHealth>();
        if (damageable != null && !other.GetComponent<EnemyBT>())
        {
            damageable.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }

    private void DestroyDisk()
    {
        if (_trackPoints.Count == 0 && !isDestroyed)
        {
            Destroy(gameObject);
            isDestroyed = !isDestroyed;
        }
    }
}
