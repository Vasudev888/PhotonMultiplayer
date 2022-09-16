using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChatManager : MonoBehaviour
{
    //public Player plMove;
    public PhotonView photonView;
    public GameObject bubbleSpeechObject;
    public Text updatedText;


    private InputField chatinputField;
    private bool disableSend;

    private void Awake()
    {
        chatinputField = GameObject.Find("ChatInputField").GetComponent<InputField>();
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            if(!disableSend && chatinputField.isFocused)
            {
                if(chatinputField.text != "" && chatinputField.text.Length > 0 && Input.GetKeyDown(KeyCode.M))
                {
                    photonView.RPC("SendMessage", PhotonTargets.AllBuffered, chatinputField.text);
                    bubbleSpeechObject.SetActive(true);

                    chatinputField.text = "";
                    disableSend = true;
                }
            }
        }
    }

    [PunRPC]
    private void SendMessage(string message)
    {
        updatedText.text = message;
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(4f);
        bubbleSpeechObject.SetActive(false);
        disableSend = false;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(bubbleSpeechObject.active);
        }
        else if (stream.isReading)
        {
            bubbleSpeechObject.SetActive((bool)stream.ReceiveNext());
        }
    }
}
