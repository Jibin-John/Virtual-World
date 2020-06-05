using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using VivoxUnity;
using System.Collections.Generic;
using System.Collections;

public class TextChatUI : MonoBehaviour
{
    private VivoxVoiceManager _vivoxVoiceManager;
    private const string LobbyChannelName = "lobbyChannel";
    private ChannelId _lobbyChannelId;
    private List<VivoxMessage> _lobbyMessages = new List<VivoxMessage>();
    private List<GameObject> _messageObjPool = new List<GameObject>();
    private ScrollRect _textChatScrollRect;

    public GameObject ChatContentObj;
    public GameObject MessageObject;
    public Button EnterButton;
    public InputField MessageInputField;

    private void Awake()
    {
        _textChatScrollRect = GetComponent<ScrollRect>();
        _vivoxVoiceManager = VivoxVoiceManager.Instance;
        if (_messageObjPool.Count > 0)
        {
            _lobbyMessages.Clear();
            ClearMessageObjectPool();
        }
        MessageInputField.text = string.Empty;
        MessageInputField.Select();
        MessageInputField.ActivateInputField();

        _vivoxVoiceManager.OnParticipantAddedEvent += OnParticipantAdded;
        _vivoxVoiceManager.OnTextMessageLogReceivedEvent += OnTextMessageLogReceivedEvent;
        _vivoxVoiceManager.OnSessionArchiveMessageReceivedEvent += OnSessionArchiveMessageReceivedEvent;

#if UNITY_PS4 || UNITY_XBOXONE || UNITY_SWITCH
        MessageInputField.gameObject.SetActive(false);
        EnterButton.gameObject.SetActive(false);
#else
        EnterButton.onClick.AddListener(SubmitTextToVivox);
        MessageInputField.onEndEdit.AddListener((string text) => { SubmitTextToVivox(); });
#endif
        if (_vivoxVoiceManager.ActiveChannels.Count > 0)
        {
            _lobbyChannelId = _vivoxVoiceManager.ActiveChannels.FirstOrDefault(ac => ac.Channel.Name == LobbyChannelName).Key;
        }
    }

    private void OnDestroy()
    {
        _vivoxVoiceManager.OnParticipantAddedEvent -= OnParticipantAdded;
        _vivoxVoiceManager.OnTextMessageLogReceivedEvent -= OnTextMessageLogReceivedEvent;
        _vivoxVoiceManager.OnSessionArchiveMessageReceivedEvent -= OnSessionArchiveMessageReceivedEvent;

#if !UNITY_PS4 || UNITY_XBOXONE || UNITY_SWITCH
        EnterButton.onClick.RemoveAllListeners();
        MessageInputField.onEndEdit.RemoveAllListeners();
#endif
    }

    private void ClearMessageObjectPool()
    {
        for (int i = 0; i < _messageObjPool.Count; i++)
        {
            Destroy(_messageObjPool[i]);
        }
        _messageObjPool.Clear();
    }

    private void SubmitTextToVivox()
    {
        if (string.IsNullOrEmpty(MessageInputField.text))
        {
            return;
        }

        _vivoxVoiceManager.SendTextMessage(MessageInputField.text, _lobbyChannelId);
        MessageInputField.text = string.Empty;
        MessageInputField.Select();
        MessageInputField.ActivateInputField();
    }

    private IEnumerator SendScrollRectToBottom()
    {
        yield return new WaitForEndOfFrame();

        // We need to wait for the end of the frame for this to be updated, otherwise it happens too quickly.
        _textChatScrollRect.normalizedPosition = new Vector2(0, 0);

        yield return null;
    }

    #region Vivox Callbacks


    void OnParticipantAdded(string username, ChannelId channel, IParticipant participant)
    {
        if (_vivoxVoiceManager.ActiveChannels.Count > 0)
        {
            _lobbyChannelId = _vivoxVoiceManager.ActiveChannels.FirstOrDefault().Channel;
        }
    }

    private void OnTextMessageLogReceivedEvent(string sender, IChannelTextMessage channelTextMessage)
    {
        VivoxMessage newChannelMessage = new VivoxMessage(channelTextMessage.Sender.DisplayName, channelTextMessage.Message, channelTextMessage.Key);
        _lobbyMessages.Add(newChannelMessage);

        var newMessageObj = Instantiate(MessageObject, ChatContentObj.transform);
        _messageObjPool.Add(newMessageObj);
        Text newMessageText = newMessageObj.GetComponent<Text>();

        // If we find a message with this tag we don't push that to the chat box. Messages with this tag are intended to denote an open or closed multiplayer match.
        if (channelTextMessage.Message.Contains("<Open>"))
        {
            newMessageText.alignment = TextAnchor.MiddleLeft;
            newMessageText.text = string.Format($"<color=blue>{newChannelMessage.SenderId} has begun hosting a match.</color>\n<color=#5A5A5A><size=8>{channelTextMessage.ReceivedTime}</size></color>");
        }
        else if (channelTextMessage.Message.Contains("<Closed>"))
        {
            newMessageText.alignment = TextAnchor.MiddleLeft;
            newMessageText.text = string.Format($"<color=blue>{newChannelMessage.SenderId}'s match has ended.</color>\n<color=#5A5A5A><size=8>{channelTextMessage.ReceivedTime}</size></color>");
        }
        else
        {
            if (channelTextMessage.FromSelf)
            {
                newMessageText.alignment = TextAnchor.MiddleRight;
                newMessageText.text = string.Format($"{newChannelMessage.MessageText} :<color=blue>{newChannelMessage.SenderId} </color>\n<color=#5A5A5A><size=8>{channelTextMessage.ReceivedTime}</size></color>");
                StartCoroutine(SendScrollRectToBottom());
            }
            else
            {
                newMessageText.alignment = TextAnchor.MiddleLeft;
                newMessageText.text = string.Format($"<color=green>{newChannelMessage.SenderId} </color>: {newChannelMessage.MessageText}\n<color=#5A5A5A><size=8>{channelTextMessage.ReceivedTime}</size></color>");
            }
        }
    }

    private void OnSessionArchiveMessageReceivedEvent(string sender, ISessionArchiveMessage archiveMessage)
    {
        Debug.Log("OnSessionArchiveMessageReceivedEvent: " + archiveMessage.Message);
    }

    #endregion
}

[Serializable]
public class VivoxMessage
{
    public string SenderId;
    public string MessageText;
    public string MessageId;

    public VivoxMessage(string senderId, string messageText, string messageId)
    {
        SenderId = senderId;
        MessageText = messageText;
        MessageId = messageId;
    }
}
