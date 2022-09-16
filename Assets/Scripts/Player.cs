using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : Photon.MonoBehaviour
{
    #region PublicVariables

    public Rigidbody2D rb;
    public Animator anim;
    public GameObject playerCamera;
    public PhotonView photonView;
    public SpriteRenderer sr;
    public Text playerNameText;

    public bool isGrounded = false;
    public float moveSpeed;
    //public float speed;
    public float jumpForce;

    #endregion

    #region PrivateVariables
    Vector3 scale;
    float horizontalMovement;
    float verticalMovement;

    private bool jump, crouch =  false;

    #endregion



    #region PublicMethods()

    #endregion


    #region PrivateMethods()

    private void Awake()
    {
        if (photonView.isMine)
        {
            playerCamera.SetActive(true);
            playerNameText.text = PhotonNetwork.playerName;
        }
        else
        {
            playerNameText.text = photonView.owner.name;
            playerNameText.color = Color.cyan;
        }
    }

    private void Update()
    {
        verticalMovement = Input.GetAxis("Jump");
        horizontalMovement = Input.GetAxis("Horizontal");


      

        if (photonView.isMine)
        {
            MoveCharacter(horizontalMovement, verticalMovement);
            PlayerMovementAnimation(horizontalMovement);
            CheckInput();
            Jump();
            Crouch();
        }
    }

    private void PlayerMovementAnimation(float horizontalMovement)
    {
      
        //var move = new Vector3(verticalMovement, 0);
        //transform.position += move * moveSpeed * Time.deltaTime;
        anim.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        scale = transform.localScale;
        if (horizontalMovement < 0)
        {

            /*scale.x = -1f * Mathf.Abs(scale.x);*/
            sr.flipX = true;
        }
        else if (horizontalMovement > 0)
        {
            /*scale.x = Mathf.Abs(scale.x);*/
            sr.flipX = false;
        }
        transform.localScale = scale;
    }


    private void MoveCharacter(float horizontalMovement, float verticalMovement)
    {
        //Move Character Horizontally
        Vector3 position = transform.position;
        position.x += horizontalMovement * moveSpeed * Time.deltaTime;
        transform.position = position;

        //Move Character vertically


    }

    private void Jump()
    {
       
        float vertical = Input.GetAxis("Jump");
        if (vertical > 0)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetBool("Crouch", true);
        }
        else 
        {
            anim.SetBool("Crouch", false);
        }
    }

    private void CheckInput()
    {

        if (horizontalMovement < 0)
        {
            photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);
        }


        else if (horizontalMovement > 0)
        {
            photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);

        }
    }

    [PunRPC]
    private void FlipTrue()
    {
        sr.flipX = true;
        /*scale.x = -1f * Mathf.Abs(scale.x);*/
    }

    [PunRPC]
    private void FlipFalse()
    {
        sr.flipX = false;
        /*scale.x = Mathf.Abs(scale.x);*/

    }


    #endregion
}
