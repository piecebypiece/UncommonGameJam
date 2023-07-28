using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MoveCore
{
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] PhotonView PV;

    protected override void Awake()
    {
        TryGetComponent(out rigid);
    }

    public void Start()
    {
        DontDestroyOnLoad(gameObject);

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}