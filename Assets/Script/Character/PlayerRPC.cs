using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRPC : MonoBehaviourPunCallbacks
{
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
}
