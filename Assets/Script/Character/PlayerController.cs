using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MoveCore
{
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] public PhotonView PV;
    [SerializeField] GameObject inputManager;
    [SerializeField] PlayerInputController PlayerInputController;

    protected override void Awake()
    {
        TryGetComponent(out rigid);
    }

    public void Start()
    {
        DontDestroyOnLoad(gameObject);

        if(PV.IsMine)
        {

        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}