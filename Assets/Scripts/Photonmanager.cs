using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Photonmanager : MonoBehaviour
{
    [SerializeField] GameObject[] SpawnPoints;
    void Start()
    {
        if(PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        int player = 0;
        if(!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("p1");
            player = 1;
        }
        // Debug.Log("0");
        GameObject Player = PhotonNetwork.Instantiate("Player1",SpawnPoints[player].transform.position,Quaternion.identity);
    }
}
