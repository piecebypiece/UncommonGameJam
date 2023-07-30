using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MoveCore, IPunInstantiateMagicCallback
{
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] public PhotonView PV;
    [SerializeField] GameObject inputManager;
    [SerializeField] float slowVal = 1f;

    public List<Material> Materials;
    Camera cam;
    public List<Material> LightMaterials;
    public List<Sprite> HatMaterials;

    [Space(10)]
    public SpriteRenderer HatRenderer;
    public GameObject LightPillar; // 크기 조절용
    public MeshRenderer LightRenderer;

    protected override void Awake()
    {
        TryGetComponent(out rigid);
    }

    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(gameObject);
        
        // playManager.AllPlayerDistance();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(isTurn.Value)
        {
            speed -= slowVal * Time.fixedDeltaTime;
            if(speed < slowVal)
            {
                speed = normalSpeed;
                isTurn.Value = false;
                Vector3 camera = new(cam.transform.position.x, 0f, cam.transform.position.z);
                Vector3 pc = new(transform.position.x, 0f, transform.position.z);
                direction = (pc - camera).normalized;
            }
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.CompareTag("Wall"))
        { 
            Vector3 incomingVec = collision.impulse - direction;

            Vector3 normalVec = collision.contacts[0].normal;       // 법선벡터

            Vector3 reflectVec = Vector3.Reflect(incomingVec, normalVec);
            direction = reflectVec;

            if (isTurn.Value)
                Dash(speed * 2f);
            else
                Dash(speed * 0.9f);
            isTurn.Value = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        var pm = PlayManager.Inst;
        pm.playerConList.Add(this);
        int materIndex = pm.playerConList.Count - 1;
        this.GetComponent<MeshRenderer>().material = Materials[materIndex];
        this.HatRenderer.sprite = HatMaterials[materIndex];
        this.LightRenderer.material = LightMaterials[materIndex];
        this.LightPillar.SetActive(false);

        if (pm.playerConList.Count == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            pm.NetController.CompleteSpwan();
        }
    }
}