using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MoveCore
{
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] public PhotonView PV;
    [SerializeField] GameObject inputManager;

    protected override void Awake()
    {
        TryGetComponent(out rigid);
    }

    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(gameObject);

        if(PV.IsMine)
        {

        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Turn()
    {
        base.Turn();
        if(isTurn.Value)
        {
            isTurn.Value = false;
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTurn.Value = true;
            Turn();
        }
    }
}