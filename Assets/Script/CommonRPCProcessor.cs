
using Photon.Pun;

// 플레이어 매니저
public class CommonRPCProcessor : MonoBehaviourPunCallbacks
{
    [PunRPC]
    public void CreateNewStempInfo(string key1, string key2, string userID)
    {
        StempInfo newInfo = new StempInfo();
        newInfo.kind = StempInfo.Kind.Word;
        newInfo.key = GameLocalizeManager.Inst.Localize(key1) + " " + GameLocalizeManager.Inst.Localize(key2);
        newInfo.UserID = userID;

        PlayManager.Inst.GetUserData(PhotonNetwork.LocalPlayer.NickName).UpdateStempInfo(newInfo);
    }
}