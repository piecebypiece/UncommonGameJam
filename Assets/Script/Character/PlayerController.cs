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

        if(collision.gameObject.CompareTag("Player"))
        {
            AllPlayerCollideChack();
        }
    }

    private void AllPlayerCollideChack()
    {
        GameObject _playManager = GameObject.Find("PlayManager");
        _playManager.TryGetComponent(out PlayManager playManager);

        for (int i = 0; i < playManager.playerConList.Count - 1; i++)
        {
            float dist = Vector3.Distance(playManager.playerConList[i].transform.position, playManager.playerConList[i + 1].transform.position);
            if (dist > playManager.GameEndDist)
            {
                isChack = true;
            }
        }
    }

    private void EndChack()
    {
        
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