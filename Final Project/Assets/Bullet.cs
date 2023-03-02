using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    public float speed = 10f;
    public float destroyTime = 2f;
    public bool shootLeft = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealt player = collision.gameObject.GetComponentInParent<PlayerHealt>();
            player.TakeDamage(20);
            GameObject.Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealt player = collision.gameObject.GetComponentInParent<PlayerHealt>();
            player.TakeDamage(20);
            GameObject.Destroy(this.gameObject);
        }
    }
 

    void Update()
    {
        if (!shootLeft)
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        else
            transform.Translate(Vector2.left * Time.deltaTime * speed);
    }

    [PunRPC]
    public void destroy()
    {
        Destroy(this.gameObject);
    }

    [PunRPC]
    public void changeDirection()
    {
        shootLeft = true;
    }
} 
