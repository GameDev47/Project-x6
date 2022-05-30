using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class PlayerItem : MonoBehaviourPunCallbacks
{
    public Text playerName;

    [SerializeField] GameObject capsul;
    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;


    Image backgroundImage;
    public Color highlightColor;
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;
    Player player;
    

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public int playerAvatar;
    //public int [] avatars;
    

    private void Awake()
    {
       
        
        backgroundImage = GetComponent<Image>();
        playerProperties["playerAvatar"] = 0;
        OnAvatarChange();
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
    private void Start()
    {
        //int[] avatars = new int[3];
    }

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        player = _player;
        UpdatePlayerItem(player);
    }

    public void ApplyLocalChanges()
    {
        backgroundImage.color = highlightColor;
        leftArrowButton.SetActive(true);
        rightArrowButton.SetActive(true);
    }

   public void OnClickLeftArrow()
    {
        if ((int)playerProperties["playerAvatar"] < 1)
        {
            playerProperties["playerAvatar"] = 2;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
        OnAvatarChange();
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightArrow()
    {
        if ((int)playerProperties["playerAvatar"] > 1)
        {
            playerProperties["playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }
        OnAvatarChange();
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnAvatarChange()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
        {
            capsul.SetActive(true);
            cube.SetActive(false);
            sphere.SetActive(false);
        }
        if ((int)playerProperties["playerAvatar"] == 1)
        {
            capsul.SetActive(false);
            cube.SetActive(true);
            sphere.SetActive(false);
        }
        if ((int)playerProperties["playerAvatar"] == 2)
        {
            capsul.SetActive(false);
            cube.SetActive(false);
            sphere.SetActive(true);
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
       if(player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }
    }

    void UpdatePlayerItem(Player player)
    {
        /* if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.int = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
        }
        else
        {
            playerProperties["playerAvatar"] = 0;
        }*/
    }
   
}
