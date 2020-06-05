using System;
using System.Collections;
using System.Collections.Generic;
using DitzeGames.MobileJoystick;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInstance :MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject[] playerPrefab;
    [SerializeField]
    UnityEngine.UI.Button[] mybutton;
    static int buttonid;
    private void Start()
    {

        if(PhotonNetwork.IsConnected)
        {
            if(playerPrefab!=null)
            {
                Debug.Log(buttonid);
                int RandomPos = UnityEngine.Random.Range(55, 62);
                PhotonNetwork.Instantiate(playerPrefab[buttonid].name, new Vector3(RandomPos, 18.32f, RandomPos), Quaternion.identity);
            }
            
        }
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log(PhotonNetwork.NickName + " Joined  " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log(newPlayer.NickName + " Joined okay " + PhotonNetwork.CurrentRoom.PlayerCount); ;
    }

    public void ButtonClicked1()
    {
        buttonid = 0;
        Debug.Log(buttonid);
    }
    public void ButtonClicked2()
    {
        buttonid = 1;
        Debug.Log(buttonid);
    }
    public void ButtonClicked3()
    {
        buttonid = 2;
        Debug.Log(buttonid);
    }
    public void ButtonClicked4()
    {
        buttonid = 3;
        Debug.Log(buttonid);
    }
    
}
