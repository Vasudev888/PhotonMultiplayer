using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    #region PrivateVariables

    [SerializeField] private string versionName = "0.1";
    [SerializeField] private GameObject userNameMenu;
    [SerializeField] private GameObject connectPanel;
    [SerializeField] private TMP_InputField userNameInput;
    [SerializeField] private TMP_InputField createGameInput;
    [SerializeField] private TMP_InputField joinGameInput;
    [SerializeField] private GameObject startButton;

    #endregion

    #region PublicVariables
    #endregion

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(versionName);
    }


    private void Start()
    {
        userNameMenu.SetActive(true);
    }
    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void ChangeUserNameInput()
    {
        if(userNameInput.text.Length >= 4)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    public void SetUserName()
    {
        userNameMenu.SetActive(false);
        PhotonNetwork.playerName = userNameInput.text;
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(createGameInput.text, new RoomOptions() { maxPlayers = 5 }, null);
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 5;
        PhotonNetwork.JoinOrCreateRoom(joinGameInput.text, roomOptions, TypedLobby.Default);
    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
