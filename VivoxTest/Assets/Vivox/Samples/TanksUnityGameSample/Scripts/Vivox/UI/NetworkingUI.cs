using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;
using VivoxUnity;
using System.Net;
using System.Linq;

public class NetworkingUI : MonoBehaviour
{
    private VivoxNetworkManager _vivoxNetworkManager;
    private VivoxVoiceManager _vivoxVoiceManager;
    private Dictionary<string, Button> _lobbyPlayers = new Dictionary<string, Button>();

    public GameObject LobbyPlayerPrefab;
    public GameObject NetworkingContentObj;
    public Button HostButton;

    #region Unity Callbacks

    private void Awake()
    {
        _vivoxNetworkManager = FindObjectOfType<VivoxNetworkManager>();
        if (!_vivoxNetworkManager)
        {
            Debug.LogError("Unable to find VivoxNetworkManager object.");
        }
        _vivoxVoiceManager = VivoxVoiceManager.Instance;
        _vivoxVoiceManager.OnParticipantAddedEvent += OnParticipantAdded;
        _vivoxVoiceManager.OnTextMessageLogReceivedEvent += OnTextMessageLogReceivedEvent;
        _vivoxVoiceManager.OnSessionArchiveMessageReceivedEvent += OnSessionArchiveMessageReceivedEvent;
        HostButton.onClick.AddListener(() => { HostMatch(); });
    }

    private void OnDestroy()
    {
        _vivoxVoiceManager.OnParticipantAddedEvent -= OnParticipantAdded;
        _vivoxVoiceManager.OnTextMessageLogReceivedEvent -= OnTextMessageLogReceivedEvent;
        _vivoxVoiceManager.OnSessionArchiveMessageReceivedEvent -= OnSessionArchiveMessageReceivedEvent;

        HostButton.onClick.RemoveAllListeners();
    }

    #endregion

    private void QueryForOpenMatches()
    {
        // TODO Needs implementation.
        //_vivoxVoiceManager.RunArchiveQueryInChannel(_lobbyChannelId, null, null);
    }

    private void AddJoinButton(string hostName, string hostIp)
    {
        GameObject lobbyPlayer = Instantiate(LobbyPlayerPrefab, NetworkingContentObj.transform);
        Button playerButton = lobbyPlayer.GetComponent<Button>();
        playerButton.onClick.AddListener(() => JoinMatch(hostIp));
        lobbyPlayer.GetComponentInChildren<Text>().text = hostName+"'s Game";
        _lobbyPlayers.Add(hostIp, playerButton);

    }

    private void RemoveJoinButton(string username, string hostIp)
    {
        var elementToRemove = _lobbyPlayers.FirstOrDefault(player => player.Key == hostIp);
        if (elementToRemove.Value != null && elementToRemove.Key != null)
        {
            Button buttonToDestroy = elementToRemove.Value;
            _lobbyPlayers.Remove(elementToRemove.Key);
            buttonToDestroy.onClick.RemoveAllListeners();
            Destroy(buttonToDestroy.gameObject);
        }
    }

    private void RemoveAllJoinButtons()
    {
        foreach (var player in _lobbyPlayers)
        {
            Button buttonToDestroy = player.Value;
            _lobbyPlayers.Remove(player.Key);
            buttonToDestroy.onClick.RemoveAllListeners();
            GameObject.Destroy(buttonToDestroy);
        }
    }

    private void HostMatch()
    {
        // StartHost must fire before SendLobbyUpdate
        _vivoxNetworkManager.StartHost();
        _vivoxNetworkManager.SendLobbyUpdate(VivoxVoiceManager.MatchStatus.Open);
        _vivoxNetworkManager.LeaveAllChannels();
    }

    private void JoinMatch(string playerIp)
    {
        _vivoxNetworkManager.networkAddress = playerIp;
        _vivoxNetworkManager.StartClient();
        _vivoxNetworkManager.LeaveAllChannels();
    }

    private string ParseIpFromQuery(string query)
    {
        return string.IsNullOrEmpty(query) ? string.Empty : query.Split(':')[1];
    }

    #region Vivox Callbacks

    private void OnParticipantAdded(string username, ChannelId channel, IParticipant participant)
    {
    }

    private void OnTextMessageLogReceivedEvent(string sender, IChannelTextMessage channelTextMessage)
    {
        // If we find a message with this tag we don't push that to the chat box. Messages with this tag are intended to denote an open or closed multiplayer match.
        if (channelTextMessage.Message.Contains("<Open>"))
        {
            AddJoinButton(channelTextMessage.Sender.DisplayName, ParseIpFromQuery(channelTextMessage.Message));
        }
        else if (channelTextMessage.Message.Contains("<Closed>"))
        {
            RemoveJoinButton(channelTextMessage.Sender.DisplayName, ParseIpFromQuery(channelTextMessage.Message));
        }
    }

    private void OnSessionArchiveMessageReceivedEvent(string sender, ISessionArchiveMessage channelTextMessage)
    {
        // TODO Implement this. Relatees to QueryForOpenMatches().
    }

    #endregion
}