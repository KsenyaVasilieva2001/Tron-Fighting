using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public abstract class Tree : MonoBehaviour
{
    private Node _root = null;

    protected virtual void Start()
    {
        _root = SetupTree();
    }
    protected virtual void Update()
    {
        if (_root != null)
            _root.Evaluate();
    }
    protected abstract Node SetupTree();
}
