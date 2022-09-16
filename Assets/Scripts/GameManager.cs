using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject gameCanvas;
    public GameObject sceneCamera;
    public Text pingText;
    public GameObject disconnectUI;
    
    public GameObject playerFeed;
    public GameObject feedGrid;


    private bool disconnectOff = false;


    private void Awake()
    {
        gameCanvas.SetActive(true);
    }

    private void Update()
    {
        CheckInput();
        pingText.text = "Ping : " + PhotonNetwork.GetPing();
    }

    private void CheckInput()
    {
        if(disconnectOff && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(false);
            disconnectOff = false;
        }
        else if(!disconnectOff && Input.GetKeyDown(KeyCode.Escape)) 
        {
            disconnectUI.SetActive(true);
            disconnectOff = true;
        }
    }

    public void SpawnPlayer()
    {
        float randomValue = Random.Range(1, -1f);
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(this.transform.position.x * randomValue, this.transform.position.y), Quaternion.identity, 0);
        gameCanvas.SetActive(false);
        sceneCamera.SetActive(false);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

    private void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        GameObject obj = Instantiate(playerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(feedGrid.transform, false);
        obj.GetComponent<Text>().text = player.name + " Joined the game";
        obj.GetComponent<Text>().color = Color.green;
    }

    private void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        GameObject obj = Instantiate(playerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(feedGrid.transform, false);
        obj.GetComponent<Text>().text = player.name + " Left the game";
        obj.GetComponent<Text>().color = Color.red;
    }
}
