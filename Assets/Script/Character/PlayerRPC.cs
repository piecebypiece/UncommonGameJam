using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerRPC : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text[] buttonTexts;

    private PlayerGameData cachedPlayerData;
    private Dictionary<KeyCode, System.Action> keyMap;
    private string key1, key2;

    public AudioSource audioSource;
    public AudioClip acquireSound;

    private void Start()
    {
       // cachedPlayerData = PlayManager.Inst.GetUserData(PhotonNetwork.LocalPlayer.NickName);
        keyMap = new Dictionary<KeyCode, System.Action>
        {
            { KeyCode.A, () => SendText(0) },
            { KeyCode.S, () => SendText(1) },
            { KeyCode.D, () => SendText(2) },
            { KeyCode.F, () => SendText(3) },
            { KeyCode.Z, () => SendText(4) },
            { KeyCode.X, () => SendText(5) },
            { KeyCode.C, () => SendText(6) },
            { KeyCode.V, () => SendText(7) },
        };
    }

    public void SendText(int num)
    {
        if (PlayManager.Inst.GetUserData(PhotonNetwork.LocalPlayer.NickName).wordKeyList.Count <= num)
            return;

        string key = PlayManager.Inst.GetUserData(PhotonNetwork.LocalPlayer.NickName).wordKeyList[num];

        if (key1 == null)
        {
            key1 = key;
            return;
        }
        key2 = key;
        RequestCreateNewStempInfo(key1, key2, PhotonNetwork.LocalPlayer.NickName);
        key1 = null;
        key2 = null;
    }

    private void Update()
    {
        foreach (var keyActionPair in keyMap)
        {
            if (Input.GetKeyDown(keyActionPair.Key))
            {
                keyActionPair.Value.Invoke();
            }
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            Debug.Log("AddWordList");
            PlayManager.Inst.GetUserData(PhotonNetwork.LocalPlayer.NickName).AddWordList("short");
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            Debug.Log("AddWordList");
            PlayManager.Inst.GetUserData(PhotonNetwork.LocalPlayer.NickName).AddWordList("lemon");
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
            audioSource.PlayOneShot(acquireSound);
            photonView.RPC("AcquireItem", RpcTarget.All, item.photonView.ViewID);
            PlayManager.Inst.GetUserData(PhotonNetwork.LocalPlayer.NickName).AddWordList(item.key);
        }
    }

    [PunRPC]
    public void AcquireItem(int viewID)
    {
        PhotonView itemPhotonView = PhotonView.Find(viewID);
        if (itemPhotonView != null)
        {
            Destroy(itemPhotonView.gameObject);
        }
    }

    [PunRPC]
    public void CreateNewStempInfo(string key1, string key2, string userID)
    {
        StempInfo newInfo = new StempInfo();
        newInfo.kind = StempInfo.Kind.Word;
        newInfo.key = GameLocalizeManager.Inst.Localize(key1) + " " + GameLocalizeManager.Inst.Localize(key2);
        newInfo.UserID = userID;

        PlayManager.Inst.UpdateStempInfo(newInfo);
    }

    public void RequestCreateNewStempInfo(string key1, string key2, string userID)
    {
        photonView.RPC("CreateNewStempInfo", RpcTarget.All, key1, key2, userID);
    }
}
