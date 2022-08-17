using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManger : MonoBehaviour
{
    PhotonView PV;
    [SerializeField] GameObject[] SpawnPoints;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

   void CreateController()
   {
    int player = 0;
        if(!PhotonNetwork.IsMasterClient)
        {
            player = 1;
        }
        if(player == 0)
        {PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Player1"),SpawnPoints[player].transform.position,Quaternion.identity);}
        else
        {PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Player2"),SpawnPoints[player].transform.position,Quaternion.identity);} 
   }
}
