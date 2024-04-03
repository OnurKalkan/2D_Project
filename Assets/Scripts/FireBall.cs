using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int speed = 20;
    public GameObject myPlayer;
    Vector3 fireBallDirection;

    private void Start()
    {
        if (myPlayer.transform.localScale.x < 0)
            fireBallDirection = Vector3.left;
        else if (myPlayer.transform.localScale.x > 0)
            fireBallDirection = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(fireBallDirection * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            GetComponent<Animator>().SetBool("Exp", true);
            speed = 0;
            Invoke(nameof(DestroyFireBall), 1);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject != myPlayer)
            {
                GetComponent<Animator>().SetBool("Exp", true);
                speed = 0;
                Invoke(nameof(DestroyFireBall), 1);
                if(collision.gameObject.GetComponent<Player>().block == false)
                {
                    collision.gameObject.GetComponent<Player>().HealthChange(-10);
                    if(collision.gameObject.GetComponent<Player>().health <= 0)
                    {
                        collision.gameObject.GetComponent<Player>().PlayAnimation("Death");
                        collision.gameObject.GetComponent<Player>().die = true;
                    }
                    else
                    {
                        //collision.gameObject.GetComponent<Player>().PlayAnimation("Hurt");
                        collision.gameObject.GetComponent<Animator>().SetTrigger("Hurt");
                    }
                }    
                else if (collision.gameObject.GetComponent<Player>().block == true)
                {
                    collision.gameObject.GetComponent<Player>().HealthChange(-2);
                    collision.gameObject.GetComponent<Animator>().SetTrigger("BlockHurt");
                }
            }            
        }
    }

    void DestroyFireBall()
    {
        Destroy(this.gameObject);
    }
}
