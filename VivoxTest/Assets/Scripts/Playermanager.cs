using System.Collections;
using System.Collections.Generic;
using DitzeGames.MobileJoystick;
using Photon.Pun;
using UnityEngine;


public class Playermanager : MonoBehaviour
{
    
    public int buttonid;
    public void playerName(string playername)
    {
        if (string.IsNullOrEmpty(playername))
        {
            Debug.Log("Empty name");
            return;
        }
        
        PhotonNetwork.NickName = playername;
    }

    public void Buttonclick1()
    {
        buttonid = 0;
        Debug.Log(buttonid);
    }
    public void Buttonclick2()
    {
        buttonid = 1;
        Debug.Log(buttonid);
    }
    public void Buttonclick3()
    {
        buttonid = 2;
        Debug.Log(buttonid);
    }
}
