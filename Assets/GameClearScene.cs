using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearScene : MonoBehaviour
{
    public static GameClearScene inst;

    void Awake() => inst = this;

    public void OnPanel()
    {
        gameObject.SetActive(true);
    }
}
