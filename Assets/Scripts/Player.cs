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
    public TextMesh playerNameText;

    public bool isGrounded = false;
    public float moveSpeed;
    //public float speed;
    public float jumpForce;

    #endregion

    #region PrivateVariables
    //private float horizontalMovement;
    //private float verticalMovement;

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
        }
    }

    private void Update()
    {
        float verticalMovement = Input.GetAxis("Vertical");
        float horizontalMovement = Input.GetAxis("Horizontal");

        MoveCharacter(horizontalMovement);

        PlayerMovementFunction(horizontalMovement);

        

        CheckInput();


        if (photonView.isMine)
        {

        }
    }

    private void PlayerMovementFunction(float horizontalMovement)
    {
      
        //var move = new Vector3(verticalMovement, 0);
        //transform.position += move * moveSpeed * Time.deltaTime;
        anim.SetFloat("Speed", Mathf.Abs(horizontalMovement));
        Vector3 scale = transform.localScale;
        if (horizontalMovement < 0)
        {

            scale.x = -1f * Mathf.Abs(scale.x);
            //sr.flipX = true;
        }
        else if (horizontalMovement > 0)
        {
            scale.x = Mathf.Abs(scale.x);
            //sr.flipX = false
        }
        transform.localScale = scale;
    }


    private void MoveCharacter(float horizontalMovement)
    {
        Vector3 position = transform.position;
        position.x += horizontalMovement * moveSpeed * Time.deltaTime;
        transform.position = position;
    }

    private void Crouch()
    {
        
    }

    private void CheckInput()
    {
      
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    sr.flipX = true;
        //}


        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    sr.flipX = false;
        //}
    }

    #endregion
}
