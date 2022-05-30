using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomItem : MonoBehaviourPunCallbacks
{
    public Text roomName;
    LobbyManager manager;
    private void Start()
    {
        manager = FindObjectOfType<LobbyManager>(); 
    }
    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void onClickItem()
    {
        manager.JoinRoom(roomName.text);
    }

    
}
