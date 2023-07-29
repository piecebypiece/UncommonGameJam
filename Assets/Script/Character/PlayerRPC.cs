using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRPC : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text[] buttonTexts;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Debug.Log("AddWordList");
            PlayManager.Inst.GetUserData(PhotonNetwork.LocalPlayer.NickName).AddWordList("straight");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RequestCreateNewStempInfo("go", "right", PhotonNetwork.LocalPlayer.NickName);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            RequestCreateNewStempInfo("go", "left", PhotonNetwork.LocalPlayer.NickName);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return; // 이 플레이어가 로컬 플레이어가 아니면 아무것도 하지 않습니다.

        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            photonView.RPC("AcquireItem", RpcTarget.All, item);
        }
    }

    [PunRPC]
    public void AcquireItem(Item item)
    {
        if (item.photonView != null)
        {
            Destroy(item.photonView.gameObject);
        }
        // item.key
        // 정보 
    }

    [PunRPC]
    public void CreateNewStempInfo(string key1, string key2, string userID)
    {
        StempInfo newInfo = new StempInfo();
        newInfo.kind = StempInfo.Kind.Word;
        newInfo.key = key1 + key2;
        newInfo.UserID = userID;

        PlayManager.Inst.UpdateStempInfo(newInfo);
    }

    public void RequestCreateNewStempInfo(string key1, string key2, string userID)
    {
        photonView.RPC("CreateNewStempInfo", RpcTarget.All, key1, key2, userID);
    }

    //public void CreateNewStempInfo2()
    //{
    //    StempInfo newInfo = new StempInfo();
    //    newInfo.kind = StempInfo.Kind.Word;
    //    newInfo.key = "abcd";
    //    newInfo.UserID = "Player1";

    //    PlayManager.Inst.UpdateStempInfo(newInfo);
    //}
}
