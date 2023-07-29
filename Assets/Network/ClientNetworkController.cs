using Photon.Pun;
using System.Collections;
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
        throw new System.NotImplementedException();
    }

    public void SpawnPlayer()
    {
        throw new System.NotImplementedException();
    }
}
