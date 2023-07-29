using UnityEngine;
using UnityEngine.Localization.Tables;

// �������� �ʾƾ��ϴ� ���Ӵܾ ���������� ��ȯ���ִ� Ŭ����.
public class UnLocalizeWord : MonoSingleton<UnLocalizeWord>
{
    [SerializeField]
    private StringTable currentStringTable;
    public string Localize(string key) => currentStringTable[key].LocalizedValue;
}