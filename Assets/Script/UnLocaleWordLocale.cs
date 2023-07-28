using UnityEngine;
using UnityEngine.Localization.Tables;

// 번역되지 않아야하는 게임단어를 번역해주는 매니저 클래스입니다.
// Locale key 는 Game 입니다.
public class UnLocaleWordLocale : MonoSingleton<UnLocaleWordLocale>
{
    [SerializeField]
    private StringTable _currentStringTable;
    public string Localize(string key) => _currentStringTable[key].LocalizedValue;
}