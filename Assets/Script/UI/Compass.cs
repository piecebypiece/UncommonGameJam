using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Compass : MonoBehaviour
{
    [SerializeField] public GameObject followCamera;
    Vector3 dir;

    private void Start()
    {
        followCamera = Camera.main.gameObject;
    }

    private void Update()
    {
        dir.z = followCamera.transform.eulerAngles.y;
        transform.localEulerAngles = dir;
    }
}
