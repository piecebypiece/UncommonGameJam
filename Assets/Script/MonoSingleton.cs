using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton : MonoBehaviour
{
    static MonoSingleton sInst;

    private void Awake()
    {
        GameObject.DontDestroyOnLoad(Inst);
    }
    public static MonoSingleton GetInstandce()
    {
        if (sInst == null)
        {
            sInst = GameObject.FindObjectOfType<MonoSingleton>();
            if(sInst == null )
            {
                sInst = new MonoSingleton();
            }
        }

        return sInst;
    }

    public static MonoSingleton Inst => GetInstandce();
}
