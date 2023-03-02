using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.UI;

public class PlayerCode : MonoBehaviour,IPunObservable
{
    public PhotonView photonview;

    public float moveSpeed = 10;
    public float jumpForce = 800;

    private Vector3 smoothMove;

    private GameObject sceneCamera;
    public GameObject playerCamera;

    public int maxHealth = 100;
    public int cureentHealth;

    public SpriteRenderer sr;
    public Text nameText;
    private Rigidbody2D rb;
    private bool IsGrounded;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform bulletSpawnLeft;

    public float offset = 5f;

    void Start()
    {
        UpdateBulletePos();
        if (photonview.IsMine)
        {
            nameText.text = PhotonNetwork.NickName;
            rb = GetComponent<Rigidbody2D>();
            sceneCamera = GameObject.Find("Main Camera");

            sceneCamera.SetActive(false);
            playerCamera.SetActive(true);
        }
        else
        {
            nameText.text = photonview.Owner.NickName;
        }

        cureentHealth = maxHealth;
    }

   void Update()
    {
        if (photonview.IsMine)
        {
            processInput();
        }
       else
       {
           smoothMovement();
        }

    }

    

    private void processInput()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += move * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            sr.flipX = false;
            photonview.RPC("OnDirectionChange_RIGHT", RpcTarget.Others);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sr.flipX = true;
            photonview.RPC("OnDirectionChange_LEFT", RpcTarget.Others);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump(); 
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Shoot();
        }
    }

    [PunRPC]
    void OnDirectionChange_LEFT()
    {
        sr.flipX = true;
    }

    [PunRPC]
    void OnDirectionChange_RIGHT()
    {
        sr.flipX = false;
    }
    void UpdateBulletePos()
    {
        bulletSpawnLeft.position = new Vector2(bulletSpawnLeft.position.x - offset, bulletSpawnLeft.position.y);
        bulletSpawn.position = new Vector2(bulletSpawn.position.x + offset, bulletSpawn.position.y);
    }

    public void Shoot()
    {
        GameObject bullete;

       if (sr.flipX == true)
        {
           bullete = PhotonNetwork.Instantiate(bulletPrefab.name, bulletSpawnLeft.position, Quaternion.identity);
        } else
        {
           bullete = PhotonNetwork.Instantiate(bulletPrefab.name, bulletSpawn.position, Quaternion.identity);
        }
        
        if (sr.flipX == true)
        {
            bullete.GetComponent<PhotonView>().RPC("changeDirection", RpcTarget.AllBuffered);
        }
    
    }

    

    void OnColisionEnter2D(Collision2D col)
    {
        if (photonview.IsMine)
        {
            if (col.gameObject.tag == "Gro")
            {
                IsGrounded = true;
            }
        }

    }

    void OnColisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Gro")
        {
            IsGrounded = false;
        }

    } 

    void jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
    }
    private void smoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
           smoothMove = (Vector3) stream.ReceiveNext();
        }
    }
}
