using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStempListView : MonoBehaviour
{
    [SerializeField] GameObjectPool pool;
    [SerializeField] List<UIStemp> stempList;

    // 리스트를 통해 정보 정보 업데이트
    // 스템프 정보 리스트 최근것이 가장 뒤에
    public void SetList(List<StempInfo> list)
    {
        int viewStempCount = stempList.Count;

        int i = 0;
        for (i = 0; i < viewStempCount || 0 > list.Count - i; i++)
        {
            stempList[i].SetInfo(list[^(i + 1)]);
        }

        for (int j = i; j < viewStempCount; j++)
        {
            stempList[i].gameObject.SetActive(false);
        }
    }
}
