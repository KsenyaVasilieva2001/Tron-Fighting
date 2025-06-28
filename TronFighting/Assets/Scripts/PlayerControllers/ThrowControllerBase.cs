using System.Collections;
using UnityEngine;

public abstract class ThrowControllerBase : MonoBehaviour
{
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected int reflections = 3;
    [SerializeField] protected float maxDistance = 20f;
    [SerializeField] protected PathTracker pathTracker;
    [SerializeField] protected Disk diskPrefab;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform handBone;

    protected DiskFactory _diskFactory;
    protected Vector3[] path;

    protected bool isEnable;

    protected virtual void Start()
    {
        _diskFactory = new DiskFactory(diskPrefab);
    }

    protected void ThrowDisk()
    {
        _diskFactory.CreateDisk(path);
        pathTracker.Clear();
    }
    protected void RotateTowards(Vector3 dir)
    {
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f)
            return;
        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 720f * Time.deltaTime);
    }

    public void OnThrowAnimationEnd()
    {
        ThrowDisk();
    }

    protected abstract void ShowTrack();
    protected void Activate()
    {
        isEnable = true;
    }

    public void Deactivate()
    {
        isEnable = false;
    }

    public bool IsActive()
    {
        return isEnable;
    }

    protected void UpdateFirePointTransform()
    {
        //firePoint.position = handBone.position;
        //firePoint.rotation = handBone.rotation;
    }


}
