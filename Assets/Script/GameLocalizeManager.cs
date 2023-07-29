using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

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
    }

    public string Unlocalize(string key) => gameStringTable[key].LocalizedValue;

    public string Localize(string key) => systemStringTable[key].LocalizedValue;

    [ContextMenu("Test")]
    public void TestSystemString()
    {
        Debug.Log(systemStringTable["test"]);
    }

}