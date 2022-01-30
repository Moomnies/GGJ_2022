using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomInput, nameInput;

    [SerializeField]
    Button create, join;

    public void CreateRoom()
    {
        int playerType = Random.Range(0, 2);
        string roomName = roomInput.text;
        StaticVars.playerName = nameInput.text;
        StaticVars.playerType = playerType;
        StaticVars.roomName = roomName;
        StaticVars.host = true;
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom()
    {
        string roomName = roomInput.text;
        StaticVars.playerName = nameInput.text;
        StaticVars.roomName = roomName;
        StaticVars.host = false;
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("Room");
    }

    private void Update()
    {
        if (nameInput.text == "" || roomInput.text == "")
        {
            create.interactable = false;
            join.interactable = false;
        }
        else { create.interactable = true; join.interactable = true; }
    }
}