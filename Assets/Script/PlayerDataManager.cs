using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 플레이어 데이터 매니저
public class PlayerDataManager : MonoSingleton<PlayerDataManager>
{
    public Dictionary<string, PlayerGameData> userDataDict;

    public PlayerGameData GetUserData(string nickname)
    {
        if (userDataDict.ContainsKey(nickname))
            return userDataDict[nickname];
        else
            Debug.LogError($"{nickname} not contain dictonary");
        return null;
    }
}