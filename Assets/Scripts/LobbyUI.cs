using System;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

#if UNITY_WEBGL && !UNITY_EDITOR
    using System.Runtime.InteropServices;
#endif

public class LobbyUI : MonoBehaviour
{
    [Header("Host")]
    [SerializeField] Button createLobbyButton;
    [SerializeField] Button copyButton;
    [SerializeField] TMP_InputField hostCodeInput;

    [Header("Join")]
    [SerializeField] TMP_InputField joinCodeInput;
    [SerializeField] Button joinButton;

    [Header("Scene")]
    [SerializeField] string gameSceneName = "OnlineGame";

    const string ConnectionType = "wss"; // (WebSocket Secure ==> wss)
    // itch.io , https (secure) ------------> WebSocket (Secure) wss (Not) -> ws 

    NetworkManager network;
    UnityTransport transport;

    #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        static extern void WebGLClipboard_Arm(string text);
    #endif

    async void Start()
    {
        network = NetworkManager.Singleton;
        if (network == null || !network.TryGetComponent(out transport))
        {
            Debug.LogError("LobbyUI: NetworkManager or UnityTransport is missing from the scene.");
            return;
        }

        hostCodeInput.text = "";
        hostCodeInput.readOnly = true;

        createLobbyButton.onClick.AddListener(CreateLobby);
        copyButton.onClick.AddListener(CopyCode);
        joinButton.onClick.AddListener(JoinLobby);

        SetupWebGL_Copy();

        SetButtonsInteractable(false);
        await InitServices();
    }

    void OnDestroy()
    {
        if (network != null)
        {
            network.OnClientConnectedCallback -= OnClientConnected;
            network.OnTransportFailure -= OnTransportFailure;
        }
    }


    async Task InitServices()
    {
        try
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
                await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

            SetButtonsInteractable(true);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error: {e.Message}");
        }
    }


    void ConfigureTransport(Allocation Host)
    {
        transport.UseWebSockets = true;
        transport.SetRelayServerData(new RelayServerData(Host, ConnectionType));
    }

    void ConfigureTransport(JoinAllocation Client)
    {
        transport.UseWebSockets = true;
        transport.SetRelayServerData(new RelayServerData(Client, ConnectionType));
    }


    async void CreateLobby()
    {
        SetButtonsInteractable(false);
        try
        {
            var HostAllocation = await RelayService.Instance.CreateAllocationAsync(1);
            string code = await RelayService.Instance.GetJoinCodeAsync(HostAllocation.AllocationId);
            hostCodeInput.text = code;

            ConfigureTransport(HostAllocation);

            network.OnClientConnectedCallback += OnClientConnected;
            network.OnTransportFailure += OnTransportFailure;

            bool started = network.StartHost();
            if (!started)
                SetButtonsInteractable(true);
        }
        catch (Exception e)
        {
            Debug.LogError($"Create lobby failed: {e.Message}");
            SetButtonsInteractable(true);
        }
    }

    async void JoinLobby()
    {
        string code = joinCodeInput.text.Trim();

        SetButtonsInteractable(false);
        try
        {
            var Clientallocation = await RelayService.Instance.JoinAllocationAsync(code);
            ConfigureTransport(Clientallocation);

            network.OnTransportFailure += OnTransportFailure;

            bool started = network.StartClient();
            if (!started)
                SetButtonsInteractable(true);
        }
        catch (Exception e)
        {
            Debug.LogError($"Join failed: {e.Message}");
            SetButtonsInteractable(true);
        }
    }

    void OnClientConnected(ulong clientId)
    {
        if (clientId == NetworkManager.ServerClientId)
            return;

        network.OnClientConnectedCallback -= OnClientConnected;
        var status = network.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);

        Debug.Log($"[NET] LoadScene status: {status}");
        if (status != SceneEventProgressStatus.Started)
            Debug.LogError($"[NET] Failed to load scene '{gameSceneName}': {status}");
    }

    void OnTransportFailure()
    {
        Debug.LogError("Transport failure");
        SetButtonsInteractable(true);
    }


    void SetupWebGL_Copy()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            var trigger = copyButton.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = copyButton.gameObject.AddComponent<EventTrigger>();

            var entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
            entry.callback.AddListener(_ =>
            {
                if (!string.IsNullOrEmpty(hostCodeInput.text))
                    WebGLClipboard_Arm(hostCodeInput.text);
            });
            trigger.triggers.Add(entry);
        #endif
    }

    void CopyCode()
    {
        if (string.IsNullOrEmpty(hostCodeInput.text))
            return;
        #if !(UNITY_WEBGL && !UNITY_EDITOR)
                GUIUtility.systemCopyBuffer = hostCodeInput.text;
        #endif
    }


    void SetButtonsInteractable(bool value)
    {
        createLobbyButton.interactable = value;
        joinButton.interactable = value;
    }
}