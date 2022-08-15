using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] Transform PlayerListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] GameObject StartGameButton; 
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
      PhotonNetwork.JoinLobby();
      PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void Startgame()
    {
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnJoinedLobby()
    {
        MenuManger.Instance.OpenMenu("title");
        Debug.Log("joined lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0,1000).ToString("0000");
    }
    public void CreateRoom()
    {
       if(string.IsNullOrEmpty(roomNameInputField.text))
       {
          return;
       }
       PhotonNetwork.CreateRoom(roomNameInputField.text);
       MenuManger.Instance.OpenMenu("loading");
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManger.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
       MenuManger.Instance.OpenMenu("room"); 
       roomNameText.text = PhotonNetwork.CurrentRoom.Name;
       
       Player[] players = PhotonNetwork.PlayerList;
       foreach(Transform child in PlayerListContent)
       {
        Destroy(child.gameObject);
       }
       for(int i = 0;i < players.Count();i++)
       {
         Instantiate(PlayerListItemPrefab,PlayerListContent).GetComponent<PlayerListItem>().Setup(players[i]);
       }
       StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        StartGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
      errorText.text = "Room Creation Failed: "+ message;
      MenuManger.Instance.OpenMenu("error");
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManger.Instance.OpenMenu("loading");
    }
    
    public override void OnLeftRoom()
    {
        MenuManger.Instance.OpenMenu("title");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
       foreach(Transform trans in roomListContent)
       {
          Destroy(trans.gameObject);
       }
       for(int i = 0; i<roomList.Count;i++)
       {
          if(roomList[i].RemovedFromList)
           {continue;}
          Instantiate(roomListItemPrefab,roomListContent).GetComponent<RoomListitem>().Setup(roomList[i]);
       }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab,PlayerListContent).GetComponent<PlayerListItem>().Setup(newPlayer);
    }
}
