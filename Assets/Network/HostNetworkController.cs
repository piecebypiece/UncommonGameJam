using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HostNetworkController : MonoBehaviourPunCallbacks, INetworkController
{

    public void OnItem()
    {
        throw new System.NotImplementedException();
    }

    public void SpawnItem()
    {
        var pm = PlayManager.Inst;
        var itemSpwanList = pm.itemSpwanPointList;
        var center = pm.villageCenter.position;
        var disLis = pm.centerDisGradeList;

        List<int> indexs = new List<int>(itemSpwanList.Count);
        for (int i = 0; i < itemSpwanList.Count; i++)
        {
            indexs.Add(i);
        }

        List<string> keys = GameLocalizeManager.Inst.GetAllKeyList();
        for (int i = 0; i < keys.Count; i++)
        {
            int index = Random.Range(0, indexs.Count);
            var transform = itemSpwanList[indexs[index]];
            int gradeIndex = FindGradeIndex(transform.position, center, disLis);
            float per = 0.5f;
            if (gradeIndex != -1)
            {
                per = pm.centerWeightProperNonuPer[gradeIndex]; 
            }

            int start = 0;
            int end = keys.Count;
            // 여기서 일반 명사
            if(Random.value > per)
            {
                start = UCDefine.EndOfProperNonuPer;
                end = keys.Count;
            }
            else
            {
                // 여긴 고유명사   
                start = 0;
                end = UCDefine.EndOfProperNonuPer - 1;
            }
            string key = keys[Random.Range(start, end)];
            _ = PhotonNetwork.Instantiate("Item", transform.position, transform.localRotation, data: new object[] {key});
        }
    }

    public int FindGradeIndex(Vector3 target, Vector3 center, List<float> distance)
    {
        var sqrdis = (target - center).sqrMagnitude;
        for (int i = 0; i < distance.Count; i++)
        {
            var disSqr = distance[i] * distance[i];
            if(sqrdis <= disSqr)
            {
                return i;
            }
        }
        return -1;
    }

    public void SpawnPlayer()
    {
        var spwanPointList = PlayManager.Inst.playerSpwanPointList;
        int playerCnt = PhotonNetwork.CurrentRoom.PlayerCount;

        List<int> indexs = new List<int>(spwanPointList.Count);
        for (int i = 0; i < spwanPointList.Count ; i++)
        {
            indexs.Add(i);
        }

        for (int i = 0; i < playerCnt; i++)
        {
            int index = Random.Range(0, indexs.Count);
            var transform = spwanPointList[indexs[index]];
            _ = PhotonNetwork.Instantiate("Player", transform.position, transform.localRotation);

            indexs.RemoveAt(index);
        }
    }
    public void CompleteSpwan()
    {
        PlayManager.Inst.OnCompleteSpawn?.Invoke();
    }

}
