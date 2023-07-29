using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;


public class Item : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    public string key;
    public TextMeshPro text;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        key = (string)instantiationData[0];

        GameLocalizeManager.Inst.Unlocalize(key);
    }
}
