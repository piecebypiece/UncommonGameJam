using UnityEngine;
using UnityEngine.Localization.Tables;

// �������� �ʾƾ��ϴ� ���Ӵܾ �������ִ� �Ŵ��� Ŭ�����Դϴ�.
// Locale key �� Game �Դϴ�.
public class UnLocaleWordLocale : MonoSingleton<UnLocaleWordLocale>
{
    [SerializeField]
    private StringTable _currentStringTable;
    public string Localize(string key) => _currentStringTable[key].LocalizedValue;
}