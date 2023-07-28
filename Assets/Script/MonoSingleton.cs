using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : class, new()
{
    static protected MonoSingleton<T> sInst;
    protected T inst;

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }
    public static T GetInstandce()
    {
        if (sInst == null)
        {
            sInst = GameObject.FindObjectOfType<MonoSingleton<T>>();
            if(sInst == null )
            {
                sInst = new MonoSingleton<T>();
            }
        }

        return sInst.inst;
    }

    public static T Inst => GetInstandce();
}
