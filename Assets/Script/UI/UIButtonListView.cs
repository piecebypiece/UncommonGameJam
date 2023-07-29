using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonListView : MonoBehaviour
{
    [SerializeField] List<Text> buttonList;
    private void Start() // userdata 캐싱 
    {
        PlayManager.Inst.GetUserData(PhotonNetwork.LocalPlayer.NickName).OnButtonInfoUpdated += SetButtonList;
    }

    public void SetButtonList(List<string> list)
    {
        Debug.Log("SetButtonList");
        int viewButtonCount = buttonList.Count;

        int i = 0;
        for (i = 0; i < viewButtonCount && i < list.Count; i++)
        {
            buttonList[i].text = PlayManager.Inst.GetUserData(PhotonNetwork.LocalPlayer.NickName).LocalizeGameWordKey(list[i]);
            buttonList[i].gameObject.SetActive(true);
        }

        for (int j = i; j < viewButtonCount; j++)
        {
            buttonList[j].gameObject.SetActive(false);
        }
    }
}
