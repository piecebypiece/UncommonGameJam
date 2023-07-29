using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

// 플레이어 매니저
public class PlayManager : MonoSingleton<PlayManager>
{
    [Header("*EndCondition")]
    public long GameEndTime;
    public float GameEndDist;

    [Space(10)]
    private long startedTimetick;
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

        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        nowTimetick = DateTime.Now.Ticks;
        var tick = TimeSpan.FromSeconds(GameEndTime).Ticks;
        if (nowTimetick - startedTimetick >= GameEndTime)
        {
            GameOver();
        }
    }

    public void PlayerDistance()
    {
        int MaxCount = 0;
        int count = 0;
        if (playerConList.Count > 2)
        {
            for (int i = 1; i <= playerConList.Count - 1; i++)
            {
                for (int j = i + 1; j <= playerConList.Count - 1; j++)
                {
                    MaxCount++;
                    float dist = Vector3.Distance(playerConList[i].transform.position, playerConList[j].transform.position);
                    if (dist >= GameEndDist)
                    {
                        count++;
                    }
                }
            }
            if(MaxCount == count)
            {
                GameWin();
            }
        }
        else
        {
            GameWin();
        }
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
    }

    private void GameWin()
    {
        Debug.Log("GameWin");
    }

    public void UpdateStempInfo(StempInfo newInfo)
    {
        stempInfoList.Add(newInfo);

        Debug.Log("OnStempInfoUpdated event triggered");
        OnStempInfoUpdated?.Invoke(stempInfoList);
    }

    public PlayerController GetMineController()
    {
        return playerConList.Find(_ => _.PV.IsMine);
    }
}