using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField nameInput;

    public void CreateRoom()
    {
        int playerType = Random.Range(0, 2);
        StaticVars.playerType = playerType;
        string roomName = nameInput.text;
        StaticVars.roomName = roomName + playerType;
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom()
    {
        string roomName = nameInput.text;
        StaticVars.roomName = roomName;
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("Multiplayer");
    }
}