using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory
{
    private Disk _diskPrefab;
    public DiskFactory(Disk prefab)
    {
        _diskPrefab = prefab;
    }

    public Disk CreateDisk(Vector3[] trackPoints)
    {
        Disk disk = GameObject.Instantiate(_diskPrefab);
        disk.Init(trackPoints);
        return disk;
    }
}
