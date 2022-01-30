using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TMP_Text roomName;

    private void Start()
    {
        roomName.text = StaticVars.roomName;
    }

    public void setPlayerColor(int hostType)
    {
        StaticVars.playerType = hostType == 0 ? 1 : 0;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //base.OnPlayerEnteredRoom(newPlayer);
        photonView.RPC("RPC_playerJoined", RpcTarget.Others, StaticVars.playerType);
    }

    [PunRPC]
    void RPC_playerJoined(int hostType)
    {
        setPlayerColor(hostType);
        roomName.text = StaticVars.roomName + StaticVars.playerType;
    }
}
