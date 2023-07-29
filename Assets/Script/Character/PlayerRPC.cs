using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRPC : MonoBehaviourPunCallbacks
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            CreateNewStempInfo();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            CreateNewStempInfo2();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return; // 이 플레이어가 로컬 플레이어가 아니면 아무것도 하지 않습니다.

        Test_Item item = other.GetComponent<Test_Item>();
        if (item != null)
        {
            photonView.RPC("AcquireItem", RpcTarget.All, item.photonView.ViewID);
        }
    }

    [PunRPC]
    public void AcquireItem(int itemId)
    {
        PhotonView itemPhotonView = PhotonView.Find(itemId);
        if (itemPhotonView != null)
        {
            Destroy(itemPhotonView.gameObject);
        }

        // 정보 입력

    }

    public void CreateNewStempInfo()
    {
        StempInfo newInfo = new StempInfo();
        newInfo.kind = StempInfo.Kind.Word;
        newInfo.key = "Hello";
        newInfo.UserID = "Player1";

        PlayManager.Inst.UpdateStempInfo(newInfo);
        //photonView.RPC("CreateNewStempInfoRPC", RpcTarget.All);
    }

    public void CreateNewStempInfo2()
    {
        StempInfo newInfo = new StempInfo();
        newInfo.kind = StempInfo.Kind.Word;
        newInfo.key = "abcd";
        newInfo.UserID = "Player1";

        PlayManager.Inst.UpdateStempInfo(newInfo);
        //photonView.RPC("CreateNewStempInfoRPC", RpcTarget.All);
    }
}
