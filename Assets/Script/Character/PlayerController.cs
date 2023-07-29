using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MoveCore, IPunInstantiateMagicCallback
{
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] public PhotonView PV;
    [SerializeField] GameObject inputManager;
    private bool isChack = false;

    protected override void Awake()
    {
        TryGetComponent(out rigid);
    }

    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(gameObject);

        /*
        if(PV.IsMine)
        {

        }
        */
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTurn.Value = true;
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject _playManager = GameObject.Find("PlayManager");
            _playManager.TryGetComponent(out PlayManager playManager);
            playManager.PlayerDistance();
        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        var pm = PlayManager.Inst;
        pm.playerConList.Add(this);

        if (pm.playerConList.Count == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            pm.NetController.CompleteSpwan();
        }
    }
}