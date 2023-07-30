using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGameData
{
    private int maxKeyCount = 8;
    public int wordAddIndex = 0;
    public List<string> wordKeyList = new List<string>();
    public List<string> unLocalizeWordKeyList = new List<string>();
    public PlayerController playerCon;

    public List<StempInfo> stempInfoList = new List<StempInfo>();

    public Action<List<StempInfo>> OnStempInfoUpdated;
    public Action<List<string>> OnButtonInfoUpdated;

    public void AddWordList(string key)
    {
        if (wordAddIndex == maxKeyCount)
        {
            wordAddIndex = 0;
        }

        if (wordKeyList.Count < maxKeyCount)
        {
            wordKeyList.Add(key);
        }
        else
        {
            wordKeyList[wordAddIndex] = key;
        }

        UpdateButtonInfo();
        wordAddIndex++;
    }

    public string ButtonText(int index)
    {
        if(index < wordKeyList.Count)
        {
            return wordKeyList[index];
        }
        return null;
    }

    public void UpdateStempInfo(StempInfo newInfo)
    {
        stempInfoList.Add(newInfo);

        Debug.Log("OnStempInfoUpdated event triggered");
        OnStempInfoUpdated?.Invoke(stempInfoList);
    }

    public void UpdateButtonInfo()
    {
        OnButtonInfoUpdated?.Invoke(wordKeyList);
    }

    //[ContextMenu("Add Key")]
    //public void AddKey()
    //{
    //    string key = "test2";
    //    AddWordList(key);
    //}


    // 플레이어가 word의 키값을 번역할 수 있으면 번역되고 아니면 원문반환
    public string LocalizeGameWordKey(string key)
    {
        if (unLocalizeWordKeyList.Contains(key))
            return GameLocalizeManager.Inst.Unlocalize(key); 
        else
            return GameLocalizeManager.Inst.Localize(key);
    }
}
