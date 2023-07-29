using Photon.Pun;
using System.Collections;
using UnityEngine;


public class Item : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    public string key;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        key = (string)instantiationData[0];
    }
}
