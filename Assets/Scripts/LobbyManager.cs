using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField]GameObject findmatchBtn;
    [SerializeField]GameObject searchingPanel;
    [SerializeField]GameObject createRoomBtn;
    [SerializeField]GameObject joinRoomBtn;
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    void Start()
    {
        findmatchBtn.SetActive(false);
        searchingPanel.SetActive(false);
        createRoomBtn.SetActive(false);
        joinRoomBtn.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("we are conntected "+ PhotonNetwork.CloudRegion);
        PhotonNetwork.AutomaticallySyncScene = true;
        findmatchBtn.SetActive(true);
        createRoomBtn.SetActive(true);
        joinRoomBtn.SetActive(true);
        
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
       Debug.Log("could find room - creating room");
       MakeRoom();
    }
    void MakeRoom()
    {
        int randomRoomName = Random.Range(0,5000);
        RoomOptions roomOptions = 
        new RoomOptions()
        {
            IsVisible = true, 
            IsOpen = true, 
            MaxPlayers = 2
        };
        PhotonNetwork.CreateRoom("Room_name_ "+ randomRoomName,roomOptions);
        Debug.Log("room created waiting for another player");

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
      if(PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
      {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "/2 starting Game");
        PhotonNetwork.LoadLevel("Game");
      }
    }

    
    public void FindMatch()
    {
      searchingPanel.SetActive(true);
      findmatchBtn.SetActive(false);
      createRoomBtn.SetActive(false);
      joinRoomBtn.SetActive(false);
      PhotonNetwork.JoinRandomRoom();
      Debug.Log("searching for a game");
    }
    public void StopSearch()
    {
      searchingPanel.SetActive(false);
      findmatchBtn.SetActive(true);
      PhotonNetwork.LeaveRoom();
      Debug.Log("stopped , back to menu");
    }
    public void CreateLocalRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }
    public void JoinLocalRoom()
    {
       PhotonNetwork.JoinRoom(joinInput.text);  
    }
     public override void OnJoinedRoom()
    {
      PhotonNetwork.LoadLevel("Game");
    }
}
