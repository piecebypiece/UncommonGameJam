using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MoveCore
{
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] PhotonView PV;
    [SerializeField] GameObject inputController;
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
            inputController = GameObject.Find("InputManager");
            if(inputController != null)
            {
                Debug.Log(GameObject.Find("InputManager").name);
                inputController.TryGetComponent(out PlayerInputController playerInputController);
                playerInputController.player = this.gameObject;
                playerInputController.PlayerSetting();
            }

        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}