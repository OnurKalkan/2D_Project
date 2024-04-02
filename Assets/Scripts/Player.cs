using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed = 10, jumpForce = 250;
    public bool onGround, die;
    public GameObject fireBallSample;
    
    void PlayAnimation(string animationName)
    {
        GetComponent<Animator>().SetBool("Idle", false);
        GetComponent<Animator>().SetBool("Run", false);
        GetComponent<Animator>().SetBool(animationName, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!die)
        {
            if (Input.GetKey(KeyCode.JoystickButton0))//A
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (Input.GetKey(KeyCode.JoystickButton1))//X
                transform.Translate(Vector3.right * Time.deltaTime * speed);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                PlayAnimation("Run");
                if (Input.GetKey(KeyCode.A))
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                if (Input.GetKey(KeyCode.D))
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                    
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton3) && onGround == true)
            {
                GetComponent<Animator>().SetTrigger("Jump");
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
            }
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                GetComponent<Animator>().SetTrigger("Attack");
                CreateFireBall();
            }
            if (Input.anyKey == false)
            {
                PlayAnimation("Idle");
            }
        }        
    }

    void CreateFireBall()
    {
        GameObject newFireBall = Instantiate(fireBallSample, transform.position + new Vector3(0,2,0), Quaternion.identity);
        newFireBall.GetComponent<FireBall>().myPlayer = this.gameObject;
        newFireBall.transform.parent = null;
        newFireBall.SetActive(true);        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
