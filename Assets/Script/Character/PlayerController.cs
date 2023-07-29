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
    private PlayManager PlayManager;

    public List<Material> Materials;

    protected override void Awake()
    {
        TryGetComponent(out rigid);
    }

    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(gameObject);

        GameObject _playManager = GameObject.Find("PlayManager");
        _playManager.TryGetComponent(out PlayManager);
        
        // playManager.AllPlayerDistance();
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

        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        var pm = PlayManager.Inst;
        pm.playerConList.Add(this);
        int materIndex = pm.playerConList.Count - 1;
        this.GetComponent<MeshRenderer>().material = Materials[materIndex];

        if (pm.playerConList.Count == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            pm.NetController.CompleteSpwan();
        }
    }
}