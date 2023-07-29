using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 간단한 게임오브젝트 풀링
// 풀에서 꺼내올때는 오브젝트가 비활성화 되어있습니다.
public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    List<GameObject> list;
    List<GameObject> activeList;

    private void Start()
    {
        list = new List<GameObject>(20);
        activeList = new List<GameObject>(20);
        Transform tf = transform;
        int childCnt = transform.childCount;
        for (int i = 0; i < childCnt; i++)
        {
            var obj = tf.GetChild(i).gameObject;
            obj.SetActive(false);
            list.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        GameObject obj;
        if(list.Count > 0)
        {
            obj = list[^1];
            list.RemoveAt(list.Count - 1);
        }
        else
        {
            obj = GameObject.Instantiate(prefab);
        }
        activeList.Add(obj);
        return obj;
    }

    public T GetObject<T>() where T : Component
    {
        var obj = GetObject();
        var component = obj.GetComponent<T>();

        if(component != null)
        {
            return component;
        }
        else
        {
            ReturnObject(obj);
            return null;
        }
    }

    public bool ReturnObject(GameObject obj)
    {
        if(activeList.Remove(obj))
        {
            obj.SetActive(false);
            list.Add(obj);
            return true;
        }
        return false;
    }
}
