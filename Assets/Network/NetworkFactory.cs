using Photon.Pun;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


// 현재 네트워크 상태에 맞는 네트워크 컨트롤러를 생성해 반환해준다.
public static class NetworkFactory
{
    public static INetworkController CreateNetworkController()
    {
        GameObject go = new GameObject();
        go.name = nameof(INetworkController);
        if (PhotonNetwork.IsMasterClient)
        {
            return go.AddComponent<HostNetworkController>();
        }
        else
        {
            return go.AddComponent<ClientNetworkController>();
        }
    }
}
