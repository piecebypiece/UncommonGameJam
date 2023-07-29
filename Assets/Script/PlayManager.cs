
using System;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 매니저
public class PlayManager : MonoSingleton<PlayManager>
{
    public long startedTimetick;
    public List<Transform> playerSpwanPointList;
    public List<Transform> itemSpwanPointList;

    public List<PlayerController> playerConList;
    public Dictionary<string, PlayerGameData> userDataDict;
    public CommonRPCProcessor comonRPC;

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

    private void Update()
    {
        
    }
}