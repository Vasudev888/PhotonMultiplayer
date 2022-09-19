using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Photon.MonoBehaviour
{
    public bool moveDirection = false;
    public float moveSpeed;
    public float destroyTime;

    private void Awake()
    {
        StartCoroutine(DestroyByTime());
    }

    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(destroyTime);
        this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    public void Change_Dir_Left()
    {
        moveDirection = true;
    }

    [PunRPC]
    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (!moveDirection)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.isMine)
            return;

        PhotonView target = collision.gameObject.GetComponentInChildren<PhotonView>();

        if(target != null && (!target.isMine || target.isSceneView))
        {
            this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
        }
    }
}
