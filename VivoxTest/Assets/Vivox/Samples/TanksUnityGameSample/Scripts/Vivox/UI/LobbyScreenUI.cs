using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VivoxUnity;

public class LobbyScreenUI : MonoBehaviour
{

    private VivoxVoiceManager _vivoxVoiceManager;
    private VivoxNetworkManager _vivoxNetworkManager;


    private EventSystem _evtSystem;

    public Button LogoutButton;
    public GameObject LobbyScreen;

    #region Unity Callbacks

    private void Awake()
    {
        _evtSystem = FindObjectOfType<EventSystem>();
        _vivoxVoiceManager = VivoxVoiceManager.Instance;
        _vivoxNetworkManager = FindObjectOfType<VivoxNetworkManager>();
        _vivoxVoiceManager.OnUserLoggedInEvent += OnUserLoggedIn;
        _vivoxVoiceManager.OnUserLoggedOutEvent += OnUserLoggedOut;
        LogoutButton.onClick.AddListener(() => { LogoutOfVivoxService(); });

        if (_vivoxVoiceManager.LoginState == LoginState.LoggedIn)
        {
            OnUserLoggedIn();
        }
        else
        {
            OnUserLoggedOut();
        }
    }

    private void OnDestroy()
    {
        _vivoxVoiceManager.OnUserLoggedInEvent -= OnUserLoggedIn;
        _vivoxVoiceManager.OnUserLoggedOutEvent -= OnUserLoggedOut;

        LogoutButton.onClick.RemoveAllListeners();
    }

    #endregion

    private void JoinLobbyChannel()
    {
        _vivoxVoiceManager.JoinChannel(_vivoxNetworkManager.LobbyChannelName, ChannelType.NonPositional, VivoxVoiceManager.ChatCapability.TextAndAudio);
    }

    private void LogoutOfVivoxService()
    {
        LogoutButton.interactable = false;

        _vivoxVoiceManager.DisconnectAllChannels();

        _vivoxVoiceManager.Logout();
    }

    #region Vivox Callbacks

    private void OnUserLoggedIn()
    {
        LobbyScreen.SetActive(true);
        LogoutButton.interactable = true;
        _evtSystem.SetSelectedGameObject(LogoutButton.gameObject, null);

        var lobbychannel = _vivoxVoiceManager.ActiveChannels.FirstOrDefault(ac => ac.Channel.Name == _vivoxNetworkManager.LobbyChannelName);
        if ((_vivoxVoiceManager && _vivoxVoiceManager.ActiveChannels.Count == 0) 
            || lobbychannel == null)
        {
            JoinLobbyChannel();
        }
        else
        {
            lobbychannel.BeginSetAudioConnected(true, TransmitPolicy.Yes, ar =>
            {
                Debug.Log("Now transmitting into lobby channel");
            });

        }
    }

    private void OnUserLoggedOut()
    {
        _vivoxVoiceManager.DisconnectAllChannels();

        LobbyScreen.SetActive(false);
    }

    #endregion
}
