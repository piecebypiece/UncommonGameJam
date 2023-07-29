using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonListView : MonoBehaviour
{
    [SerializeField] List<Text> buttonList;
    private void Start()
    {
        PlayManager.Inst.GetUserData(PhotonNetwork.LocalPlayer.NickName).OnButtonInfoUpdated += SetButtonList;
    }

    public void SetButtonList(List<string> list)
    {
        for(int i=0; i < list.Count; i++)
        {
            buttonList[i].text = PlayManager.Inst.GetUserData(PhotonNetwork.LocalPlayer.NickName).LocalizeGameWordKey(list[i]);
        }
    }
}
