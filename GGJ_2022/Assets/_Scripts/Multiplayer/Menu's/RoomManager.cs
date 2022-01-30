using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TMP_Text roomName, playerName1, playerName2, waiting;

    [SerializeField]
    Button startButton;

    [SerializeField]

    private void Start()
    {
        roomName.text = StaticVars.roomName;
        playerName1.text = StaticVars.playerName;
        playerName2.enabled = false;
        startButton.gameObject.SetActive(false);

        if (PhotonNetwork.IsMasterClient)
        {
            waiting.text = "Waiting for other player";
        }
    }

    public void setPlayerType(int hostType)
    {
        StaticVars.playerType = hostType == 0 ? 1 : 0;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //base.OnPlayerEnteredRoom(newPlayer);
        waiting.enabled = false;
        photonView.RPC("RPC_playerJoined", RpcTarget.Others, StaticVars.playerType, StaticVars.playerName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        startButton.gameObject.SetActive(true);
        startButton.interactable = false;
        waiting.enabled = false;
    }

    public void SetOtherPlayerName(string playerName)
    {
        playerName2.enabled = true;
        playerName2.text = playerName;
    }

    public void EnableStart()
    {
        waiting.enabled = false;
        startButton.gameObject.SetActive(true);
        startButton.interactable = true;
    }

    [PunRPC]
    void RPC_playerJoined(int hostType, string hostName)
    {
        setPlayerType(hostType);
        SetOtherPlayerName(hostName);
        photonView.RPC("RPC_ReturnName", RpcTarget.Others, StaticVars.playerName);
    }

    [PunRPC]
    void RPC_ReturnName(string playerName)
    {
        SetOtherPlayerName(playerName);
        EnableStart();
    }
}
