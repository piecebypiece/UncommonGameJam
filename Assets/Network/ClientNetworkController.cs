using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClientNetworkController : MonoBehaviourPunCallbacks, INetworkController
{
    [PunRPC]
    public void OnItem()
    {
       
    }

    public void SendStemp()
    {
        throw new System.NotImplementedException();
    }

    public void SpawnItem()
    {
        
    }

    public void SpawnPlayer()
    {

    }
}
