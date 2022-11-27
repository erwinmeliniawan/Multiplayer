using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class Ui : MonoBehaviourPunCallbacks
{
    public InputField createRoom;
    public InputField joinRoom;
    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoom.text, new RoomOptions { MaxPlayers = 4 }, null);
    }
   public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoom.text, null);

    }
    public override void OnJoinedRoom()
    {
        print("Joined Sucess");
        PhotonNetwork.LoadLevel(1);

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Room Failed" + returnCode + "Message " + message);
    }

}
