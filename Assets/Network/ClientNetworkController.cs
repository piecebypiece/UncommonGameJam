using Photon.Pun;
using Photon.Realtime;
using System;


public class ClientNetworkController : MonoBehaviourPunCallbacks, INetworkController, IPunOwnershipCallbacks
{
    public void CompleteSpwan()
    {
        int playerCnt = PhotonNetwork.CurrentRoom.PlayerCount;
        var players = PhotonNetwork.PlayerList;
        int actorNum = PhotonNetwork.LocalPlayer.ActorNumber;
        int myIndex = Array.FindIndex(players, _ => _.ActorNumber == actorNum);

        PhotonNetwork.AddCallbackTarget(this);
        PlayManager.Inst.playerConList[myIndex].PV.RequestOwnership();
    }

    [PunRPC]
    public void OnItem()
    {
       
    }

    public void SpawnItem()
    {
        
    }

    public void SpawnPlayer()
    {

    }


    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {

    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        if (targetView.ControllerActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            PhotonNetwork.RemoveCallbackTarget(this);
            PlayManager.Inst.OnCompleteSpawn?.Invoke();
        }
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {

    }
}
