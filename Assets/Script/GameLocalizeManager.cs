using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Components;

// 번역되지 않아야하는 게임단어를 고정적으로 반환해주는 클래스.
public class GameLocalizeManager : MonoSingleton<GameLocalizeManager>
{
    [SerializeField]
    private StringTable gameStringTable;
    [SerializeField]
    private StringTable systemStringTable;


    private void Start()
    {
        systemStringTable = LocalizationSettings.Instance.GetStringDatabase().GetTable("GameWord");

        // LocalizationSettings.StringDatabase는 모든 StringTableCollection을 가지고 있습니다.
        

    }

    public string Unlocalize(string key) => systemStringTable[key].LocalizedValue;

    public string Localize(string key) => systemStringTable[key].LocalizedValue;

    [ContextMenu("Test")]
    public void TestSystemString()
    {
        //Debug.Log(systemStringTable["test"]);
        var table = LocalizationSettings.StringDatabase.GetTable("GameWord") as StringTable;
        Debug.Log(table + "");

        if (table != null)
        {
            foreach (var entry in table.SharedData.Entries)
            {
                // entry.Key.Id는 키를 나타냅니다.
                string key = entry.Key;

                Debug.Log($"Key: {key}");

                string result = Localize(key);
                Debug.Log(result);
            }
        }
    }

    [ContextMenu("Get Item")]
    public void GetItem()
    {
        string itemKey = "test2";
        var table = LocalizationSettings.StringDatabase.GetTable("GameWord") as StringTable;

        foreach (var entry in table.SharedData.Entries)
        {
            string key = entry.Key;

            if(entry.Key == itemKey)
                Debug.Log(Localize(itemKey));
        }
    }

    [ContextMenu("Localize")]
    public void LocalizeTextString()
    {
        string tableName = "GameWord";
        string itemId = "testb";
        Debug.Log(GetComponent<LocalizeStringEvent>().StringReference.GetLocalizedString(tableName, itemId));

        //.SetReference(tableName, itemValue.itemName);
    }
}