using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using Photon.Pun;
using DG.Tweening;


// 플레이어 매니저
public class PlayManager : MonoSingleton<PlayManager>
{
    [Header("*EndCondition")]
    public long GameEndTime;
    public float GameEndDist;
    //BoolReactiveProperty RaderDist = new BoolReactiveProperty();
    //public GameObject dangerImage;

    [Space(10)]
    private long startedTimetick;
    private long nowTimetick;
    
    public List<Transform> playerSpwanPointList;
    public List<Transform> itemSpwanPointList;
    public Transform villageCenter;                 // 마을의 중앙값 문자 아이템 생성시 사용한다.
    public List<float> centerDisGradeList;
    public List<float> centerWeightProperNonuPer;   // 마을 단계별
    public int initSpawnItemCount;

    public SoundManager soundManager;

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
        //RaderDist
        //        .DistinctUntilChanged()
        //        .Where(x => x == true)
        //        .Subscribe(x =>
        //        {
        //            dangerImage.SetActive(true);
        //            dangerImage.transform.DOScale(new Vector2(2, 2), 1).SetLoops(-1, LoopType.Yoyo);
        //        });

        comonRPC = new GameObject("CommonRPC").AddComponent<CommonRPCProcessor>();
        comonRPC.transform.parent = this.transform;
        startedTimetick = DateTime.Now.Ticks;

        var userList = PhotonNetwork.PlayerList;
        userDataDict = new(userList.Length);
        for (int i = 0; i < userList.Length; i++)
        {
            var u = userList[i];
            userDataDict.Add(u.NickName, new PlayerGameData());
        }

        netCon = NetworkFactory.CreateNetworkController();

        netCon.SpawnPlayer();
        netCon.SpawnItem();

        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void Update()
    {
        Timer();
        AllPlayerDistance();
    }

    private void Timer()
    {
        nowTimetick = DateTime.Now.Ticks;
        var tick = TimeSpan.FromSeconds(GameEndTime).Ticks;
        var oneMin = TimeSpan.FromMinutes(1).Ticks;
        if (nowTimetick - startedTimetick >= tick - oneMin)
        {
            soundManager.ChangeSound();
        }
        else if (nowTimetick - startedTimetick >= tick)
        {
            GameOver();
        }
    }

    public int CountingAroundPlayer(PlayerController playerController, float dis)
    {
        int cnt = 0;
        for (int i = 0; i < playerConList.Count; i++)
        {
            if (playerConList[i] == playerController) continue;
            if ((playerConList[i].transform.position - playerController.transform.position)
                .sqrMagnitude <= dis * dis)
            {
                ++cnt;
            }
        }
        return cnt;
    }

    public void AllPlayerDistance()
    {
        int MaxCount = 0;
        int count = 0;
        if (playerConList.Count == 1) return;
        if (playerConList.Count > 2)
        {
            for (int i = 1; i <= playerConList.Count - 1; i++)
            {
                for (int j = i + 1; j <= playerConList.Count - 1; j++)
                {
                    MaxCount++;
                    float dist = (playerConList[i].transform.position - playerConList[j].transform.position).sqrMagnitude;
                    if (dist <= GameEndDist * GameEndDist)
                    {
                        count++;
                    }
                    else
                    {
                        break;
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
            float dist = (playerConList[0].transform.position - playerConList[1].transform.position).sqrMagnitude;
            if (dist <= GameEndDist * GameEndDist)
            {
                GameWin();
            }
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