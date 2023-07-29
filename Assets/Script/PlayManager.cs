using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 플레이어 매니저
public class PlayManager : MonoSingleton<PlayManager>
{
    [SerializeField] Transform playerSpwanPointList;
    [SerializeField] Transform itemSpwanPointList;

    public Dictionary<string, PlayerGameData> userDataDict;

    public INetworkController NetController => netCon;

    public PlayerGameData GetUserData(string nickname)
    {
        if (userDataDict.ContainsKey(nickname))
            return userDataDict[nickname];
        else
            Debug.LogError($"{nickname} not contain dictonary");
        return null;
    }
    INetworkController netCon;
    private void Start()
    {
        netCon = NetworkFactory.CreateNetworkController();

        netCon.SpawnPlayer();
        netCon.SpawnItem();
    }
}