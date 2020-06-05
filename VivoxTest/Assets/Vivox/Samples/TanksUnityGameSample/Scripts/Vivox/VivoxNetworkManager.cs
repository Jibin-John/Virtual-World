﻿using System.Collections;
using UnityEngine;
using Mirror;
using System.Linq;
using VivoxUnity;
using System.Net.Sockets;
using System.Net;

public class VivoxNetworkManager : NetworkManager
{
    private VivoxVoiceManager m_vivoxVoiceManager;
    public string PositionalChannelName { get; private set; }
    public string LobbyChannelName = "lobbyChannel";
    public string clientLocalIpAddress
    {
        get
        {
            string localIpAddress = null;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530); //Google public DNS and port
                IPEndPoint localEndPoint = socket.LocalEndPoint as IPEndPoint;
                localIpAddress = localEndPoint.Address.ToString();
            }
            if (string.IsNullOrWhiteSpace(localIpAddress))
            {
                Debug.LogError("Unable to find clientLocalIpAddress");
            }
            return localIpAddress;
        }
    }
    public string clientPublicIpAddress
    {
        get {
            string publicIpAddress = new WebClient().DownloadString("https://api.ipify.org");
            if (string.IsNullOrWhiteSpace(publicIpAddress))
            {
                Debug.LogError("Unable to find clientPublicIpAddress");
            }
            return publicIpAddress.Trim();
        }
    }

    public string HostingIp
    {
        get
        {
            if (NetworkServer.active || NetworkClient.serverIp == "localhost")
            {
                return clientLocalIpAddress;
            }
            // Your connected to a remote host. Let's get their ip
            return NetworkClient.serverIp ?? clientLocalIpAddress;
        }
    }

    public override void Awake()
    {
        base.Awake();
        m_vivoxVoiceManager = VivoxVoiceManager.Instance;
    }

    public void SendLobbyUpdate(VivoxVoiceManager.MatchStatus status)
    {
        var lobbyChannelId = m_vivoxVoiceManager.ActiveChannels.FirstOrDefault(ac => ac.Channel.Name == LobbyChannelName).Key;
        if (lobbyChannelId != null)
        {
            m_vivoxVoiceManager.SendMatchStatusMessage(status, HostingIp, lobbyChannelId);
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, AddPlayerMessage extraMessage)
    {
        base.OnServerAddPlayer(conn, extraMessage);

        StartCoroutine(AddNewPlayer(conn));
    }


    private IEnumerator AddNewPlayer(NetworkConnection conn)
    {
        var player = conn.playerController.gameObject.GetComponent<TankSetup>();

        TeamManager.Instance.AssignTeam(player);

        // Wait until the player object is ready before adding them to the team roster.
        yield return new WaitUntil(() => player.m_IsReady);
        TeamManager.Instance.Players.Add(player);
    }

    public void LeaveAllChannels(bool includeLobby = true)
    {
        foreach (var channelSession in m_vivoxVoiceManager.ActiveChannels)
        {
            if (channelSession.AudioState == ConnectionState.Connected || channelSession.TextState == ConnectionState.Connected
                && (includeLobby || (includeLobby == false && channelSession.Channel.Name != LobbyChannelName)))
            {
                channelSession.Disconnect();
            }
        }
    }

    private void VivoxVoiceManager_OnParticipantAddedEvent(string username, ChannelId channel, IParticipant participant)
    {
        if (channel.Name == PositionalChannelName && participant.Account.Name == m_vivoxVoiceManager.LoginSession.Key.Name)
        {
            StartCoroutine(AwaitLobbyRejoin());
        }
    }

    private IEnumerator AwaitLobbyRejoin()
    {
        IChannelSession lobbyChannel = m_vivoxVoiceManager.ActiveChannels.FirstOrDefault(ac => ac.Channel.Name == LobbyChannelName);
        // Lets wait until we have left the lobby channel before tyring to join it.
        yield return new WaitUntil(() => lobbyChannel == null
        || (lobbyChannel.AudioState != ConnectionState.Connected && lobbyChannel.TextState != ConnectionState.Connected));

        m_vivoxVoiceManager.JoinChannel(LobbyChannelName, ChannelType.NonPositional, VivoxVoiceManager.ChatCapability.TextOnly, TransmitPolicy.No);
    }

    public override void OnStartClient()
    {
        Debug.Log("Starting client");

        PositionalChannelName = "Positional" + HostingIp;
        m_vivoxVoiceManager.OnParticipantAddedEvent += VivoxVoiceManager_OnParticipantAddedEvent;
        base.OnStartClient();
    }
    public override void OnStopClient()
    {
        Debug.Log("Stopping client");
        m_vivoxVoiceManager.OnParticipantAddedEvent -= VivoxVoiceManager_OnParticipantAddedEvent;
        LeaveAllChannels(false);
        base.OnStopClient();
    }
}
