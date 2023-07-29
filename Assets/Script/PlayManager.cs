using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

// 플레이어 매니저
public class PlayManager : MonoSingleton<PlayManager>
{
    public long startedTimetick;
    public long GameEndTime;
    private long nowTimetick;
    public List<Transform> playerSpwanPointList;
    public List<Transform> itemSpwanPointList;

    public List<PlayerController> playerConList;
    public Dictionary<string, PlayerGameData> userDataDict;
    public CommonRPCProcessor comonRPC;

    public List<StempInfo> stempInfoList = new List<StempInfo>();
    public Action<List<StempInfo>> OnStempInfoUpdated;
    public Action OnCompleteSpawn;

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
        comonRPC = new GameObject("CommonRPC").AddComponent<CommonRPCProcessor>();
        comonRPC.transform.parent = this.transform;
        startedTimetick = DateTime.Now.Ticks;
        netCon = NetworkFactory.CreateNetworkController();

        netCon.SpawnPlayer();
        netCon.SpawnItem();
    }

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        nowTimetick = DateTime.Now.Ticks;
        if (nowTimetick - startedTimetick >= GameEndTime)
        {
            GameOver();
        }
    }

    private void PlayerDistance()
    {
        for(int i = 0; i < playerConList.Count - 1; i++)
        {
            for(int j = 0; j < playerConList.Count - 1; j++)
            {
                float dist = Vector3.Distance(playerConList[i].transform.position, playerConList[j + 1].transform.position);
            }          
        }
    }


    private void GameOver()
    {
        Debug.Log("GameOver");
    }

    private void GameWin()
    {

    }

    public void UpdateStempInfo(StempInfo newInfo)
    {
        stempInfoList.Add(newInfo);

        OnStempInfoUpdated?.Invoke(stempInfoList);
    }

    public PlayerController GetMineController()
    {
        return playerConList.Find(_ => _.PV.IsMine);
    }
}