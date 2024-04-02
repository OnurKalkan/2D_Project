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
        if (myPlayer.transform.position.x < 0)
            fireBallDirection = Vector3.right;
        else if (myPlayer.transform.position.x > 0)
            fireBallDirection = Vector3.left;
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
                collision.gameObject.GetComponent<Animator>().SetBool("Death", true);
                collision.gameObject.GetComponent<Player>().die = true;
            }            
        }
    }

    void DestroyFireBall()
    {
        Destroy(this.gameObject);
    }
}
