using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Test_ItemManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public Vector3 minSpawnPosition;
    public Vector3 maxSpawnPosition;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for(int i=0; i<3; i++)
            {
                Vector3 spawnPosition = new Vector3(
                Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                Random.Range(minSpawnPosition.y, maxSpawnPosition.y),
                Random.Range(minSpawnPosition.z, maxSpawnPosition.z)
            );

                GameObject tmpItem = PhotonNetwork.Instantiate(itemPrefab.name, spawnPosition, Quaternion.identity);
                Debug.Log(tmpItem.name + " : " + tmpItem.transform);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
