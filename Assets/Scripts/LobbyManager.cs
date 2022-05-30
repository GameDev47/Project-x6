using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;



public class LobbyManager : MonoBehaviourPunCallbacks
{

    public InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public Text roomName;
    [SerializeField] Camera SecondCam;

    public Text notEnoughPlayerText;
    public Text roomNameRequiredText;
    public Text waitingForHostTostart;

    public RoomItem roomItemPrefab;
    List<RoomItem> roomItemList = new List<RoomItem>();
    public Transform contenObject;

    public float timeBetweenUpdates = 1.5f;
    public float nextUpdateTime;

    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playButton;
    private void Start()
    {
        PhotonNetwork.JoinLobby();
        SecondCam.GetComponent<AudioListener>().enabled = false;
    }

    public void OnClickCreate()
    {
        if (roomInputField.text.Length > 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers = 3, BroadcastPropsChangeToAll = true });
        }
        else
        {
            roomNameRequiredText.text = "Room name required";
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
       
    }

    private void UpdateRoomList(List<RoomInfo> lists)
    {
        foreach (RoomItem item in roomItemList)
        {
            Destroy(item.gameObject);
        }

        roomItemList.Clear();
        foreach (RoomInfo room in lists)
        {
           RoomItem newRoom =  Instantiate(roomItemPrefab, contenObject);
            newRoom.SetRoomName(room.Name);
            roomItemList.Add(newRoom);
        }
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnclickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        notEnoughPlayerText.text = string.Empty;
    }
        
    public override void OnLeftRoom()
    {
        if(roomPanel.activeSelf == true)
        {
            roomPanel.SetActive(false);
        }
       
        lobbyPanel.SetActive(true);
         
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void UpdatePlayerList()
    {

        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();
        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
           PlayerItem  newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }
            playerItemsList.Add(newPlayerItem);
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playButton.SetActive(true);
            waitingForHostTostart.text = string.Empty;
        }
        else
        {
            playButton.SetActive(false);
            waitingForHostTostart.text = "Waiting forhost to start the game";
        }
    }

    public void OnClickPlay()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2 )
        {
            PhotonNetwork.LoadLevel("Game");
        }else

        {
            notEnoughPlayerText.text = "Need atleast two players to start game";
        }
        
    }
}
 