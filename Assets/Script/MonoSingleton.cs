using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton : MonoBehaviour
{
    static MonoSingleton Inst;
    // Start is called before the first frame update
    public static MonoSingleton GetInstandce()
    {

        return Inst;
    }
}
