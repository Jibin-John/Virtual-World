using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Connection : MonoBehaviourPunCallbacks
{
    public GameObject inputField;
    public GameObject loadingPanel;
    public GameObject lobyPanel;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void OnConnectedToServer()
    {
        inputField.SetActive(false);
        loadingPanel.SetActive(true);
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }
    public override void OnConnectedToMaster()
    {
        //base.OnConnectedToMaster();
        loadingPanel.SetActive(false);
        lobyPanel.SetActive(true);
        Debug.Log(PhotonNetwork.NickName+"Connection with photon is established");
    }
    public override void OnConnected()
    {
        //base.OnConnected();
        Debug.Log("Connected to internet");
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoin();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log(PhotonNetwork.NickName + "Joined to" + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("AvaRoom");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName + "Joined"+PhotonNetwork.CurrentRoom.PlayerCount);
    }


    void CreateAndJoin()
    {
        string randomRoomName = "Room" + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 10;
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

        
    }
}
