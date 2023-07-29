using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HostNetworkController : MonoBehaviourPunCallbacks, INetworkController
{

    public void OnItem()
    {
        throw new System.NotImplementedException();
    }

    

    public void SendStemp()
    {
        throw new System.NotImplementedException();
    }

    public void SpawnItem()
    {
        
    }

    [PunRPC]
    public void SpawnPlayer()
    {
        var spwanPointList = PlayManager.Inst.playerSpwanPointList;
        int playerCnt = PhotonNetwork.CurrentRoom.PlayerCount;

        List<int> indexs = new List<int>(spwanPointList.Count);
        for (int i = 0; i < spwanPointList.Count ; i++)
        {
            indexs.Add(i);
        }

        for (int i = 0; i < playerCnt; i++)
        {
            int index = Random.Range(0, indexs.Count);
            var transform = spwanPointList[indexs[i]];
            GameObject player = PhotonNetwork.Instantiate("Player", transform.position, transform.localRotation);
        }
    }
    public void CompleteSpwan()
    {
        int playerCnt = PhotonNetwork.CurrentRoom.PlayerCount;
        var players = PhotonNetwork.PlayerListOthers;
        var pcList = PlayManager.Inst.playerConList;

        for (int i = 1; i < playerCnt; i++)
        {
            pcList[i].PV.TransferOwnership(players[i - 1]);
        }

        PlayManager.Inst.OnCompleteSpawn?.Invoke();
    }
}
