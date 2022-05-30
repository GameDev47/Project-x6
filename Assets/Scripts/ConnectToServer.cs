using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public InputField usernameInput;
    public Text buttontext;
    public Text nameRequiredText;

    public void OnClickConnect()
    {
        if(usernameInput.text.Length > 1)
        {
            PhotonNetwork.NickName = usernameInput.text;
            buttontext.text = "connecting ...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }else
        {
            nameRequiredText.text = "Name required";
        }
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
} 


