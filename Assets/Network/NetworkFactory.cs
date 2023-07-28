using Photon.Pun;
using System.Collections;
using UnityEngine;

namespace Network
{
    // 현재 네트워크 상태에 맞는 네트워크 컨트롤러를 생성해 반환해준다.
    public static class NetworkFactory
    {
        public static INetworkController CreateNetworkController()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                return new HostNetworkController();
            }
            else
            {
                return new ClientNetworkController();
            }
        }

        
    }
}