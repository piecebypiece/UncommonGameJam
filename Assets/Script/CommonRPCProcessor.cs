
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 매니저
public class CommonRPCProcessor : MonoBehaviourPunCallbacks
{
    [PunRPC]
    public void AddPlayers(List<PlayerController> players)
    {
       PlayManager.Inst.playerConList = players;
    }
}