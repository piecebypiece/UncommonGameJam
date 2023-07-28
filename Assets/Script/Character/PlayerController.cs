using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MoveCore
{
    [SerializeField] protected Rigidbody rigid;

    protected override void Awake()
    {
        TryGetComponent(out rigid);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}