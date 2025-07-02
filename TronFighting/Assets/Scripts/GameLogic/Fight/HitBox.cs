using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Damage))]
public class HitBox: MonoBehaviour
{
    public float radius = 1f;
    public Vector3 offset = Vector3.forward;
    public LayerMask targetMask;

    private IDamage _damageDealer;

    private void Awake()
    {
        _damageDealer = GetComponent<IDamage>();
    }

    public void OnHitFrame(AnimationEvent evt)
    {
        float damage = evt.floatParameter;

        Vector3 worldCenter = transform.position + transform.TransformDirection(offset);
        Collider[] hits = Physics.OverlapSphere(worldCenter, radius, targetMask);

        foreach (var col in hits)
        {
            IHealth health = col.GetComponent<IHealth>();
            if (health != null)
            {
                _damageDealer.CauseDamage(health, damage);
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(offset), radius);
    }
}