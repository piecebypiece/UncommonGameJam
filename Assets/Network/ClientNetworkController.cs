using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClientNetworkController : MonoBehaviourPunCallbacks, INetworkController
{
    public void CompleteSpwan()
    {
        PlayManager.Inst.OnCompleteSpawn?.Invoke();
    }

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
