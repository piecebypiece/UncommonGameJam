using UnityEngine;
using UnityEngine.Localization.Tables;

// 번역되지 않아야하는 게임단어를 고정적으로 반환해주는 클래스.
public class UnLocalizeWord : MonoSingleton<UnLocalizeWord>
{
    [SerializeField]
    private StringTable _currentStringTable;
    public string Localize(string key) => _currentStringTable[key].LocalizedValue;
}