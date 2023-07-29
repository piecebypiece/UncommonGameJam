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
        throw new System.NotImplementedException();
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

        List<PlayerController> pcList = new(playerCnt);
        for (int i = 0; i < playerCnt; i++)
        {
            int index = Random.Range(0, indexs.Count);
            var transform = spwanPointList[indexs[i]];
            GameObject player = PhotonNetwork.Instantiate("Player", transform.position, transform.localRotation);
            player.name = "player" + i + 1;
            pcList.Add(player.GetComponent<PlayerController>());
        }

        var players = PhotonNetwork.PlayerListOthers;

        for (int i = 1; i < playerCnt; i++)
        {
            pcList[i].PV.TransferOwnership(players[i]);
        }

        PlayManager.Inst.comonRPC.AddPlayers(pcList);
    }
}
