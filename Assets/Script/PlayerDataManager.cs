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

public class PlayerGameData
{
    public int wordAddIndex = 0;
    public List<string> wordKeyList;
    public List<string> localizeWordKeyList;

    public void AddWordList(string key)
    {
        if(wordAddIndex == wordKeyList.Count)
        {
            wordAddIndex = 0;
        }
        wordKeyList[wordAddIndex] = key;
        wordAddIndex++;
    }


    // 플레이어가 word의 키값을 번역할 수 있으면 번역되고 아니면 원문반환
    public string LocalizeGameWordKey(string key)
    {
        if (localizeWordKeyList.Contains(key))
            return GameLocalizeManager.Inst.Localize(key);
        else
            return GameLocalizeManager.Inst.Unlocalize(key);
    }
}