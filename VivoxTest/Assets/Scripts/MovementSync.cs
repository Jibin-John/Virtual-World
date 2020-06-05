using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using DitzeGames.MobileJoystick;

public class MovementSync : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject FpsCam;
    public TouchField touch;
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            transform.GetComponent<JoystickPlayerExample>().enabled = true;
            FpsCam.GetComponent<Camera>().enabled = true;
            
        }
        else
        {
            transform.GetComponent<JoystickPlayerExample>().enabled = false;
            FpsCam.GetComponent<Camera>().enabled = false;
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
