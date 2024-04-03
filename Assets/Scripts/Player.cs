using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int speed = 10, jumpForce = 250, health = 100;
    public bool onGround, die, block, melee;
    public GameObject fireBallSample;
    public PlayerNo playerNo;
    public Image healthBar;

    public enum PlayerNo
    {
        PlayerOne,
        PlayerTwo
    }
    
    public void PlayAnimation(string animationName)
    {
        GetComponent<Animator>().SetBool("Idle", false);
        GetComponent<Animator>().SetBool("Run", false);
        GetComponent<Animator>().SetBool("Block", false);
        GetComponent<Animator>().SetBool(animationName, true);
    }

    public void HealthChange(int healthChange)
    {
        health += healthChange;
        healthBar.fillAmount = (float)health / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!die)
        {
            //if (Input.GetKey(KeyCode.JoystickButton0))//A
            //    transform.Translate(Vector3.left * Time.deltaTime * speed);
            //if (Input.GetKey(KeyCode.JoystickButton1))//X
            //    transform.Translate(Vector3.right * Time.deltaTime * speed);
            if(playerNo == PlayerNo.PlayerOne)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {                    
                    PlayAnimation("Run");
                    if (Input.GetKey(KeyCode.A))
                        transform.Translate(Vector3.left * Time.deltaTime * speed);
                    if (Input.GetKey(KeyCode.D))
                        transform.Translate(Vector3.right * Time.deltaTime * speed);                    
                }
                if (Input.GetKeyDown(KeyCode.W) && onGround == true)
                {
                    GetComponent<Animator>().SetTrigger("Jump");
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Attack();
                }
                if (Input.GetKey(KeyCode.F))
                {
                    Block();
                }
                else
                {
                    block = false;
                }
            }
            else if(playerNo == PlayerNo.PlayerTwo)
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                {    
                    PlayAnimation("Run");
                    if (Input.GetKey(KeyCode.LeftArrow))
                        transform.Translate(Vector3.left * Time.deltaTime * speed);
                    if (Input.GetKey(KeyCode.RightArrow))
                        transform.Translate(Vector3.right * Time.deltaTime * speed);                    
                }
                if (Input.GetKeyDown(KeyCode.UpArrow) && onGround == true)
                {
                    GetComponent<Animator>().SetTrigger("Jump");
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
                }
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    Attack();
                }
                if (Input.GetKey(KeyCode.RightControl))
                {
                    Block();
                }
                else
                {
                    block = false;
                }
            }
            if (Input.anyKey == false)
            {
                PlayAnimation("Idle");
            }
        }        
    }

    void Attack()
    {
        GetComponent<Animator>().SetTrigger("Attack");
        CreateFireBall();
    }

    void Block()
    {
        block = true;
        PlayAnimation("Block");
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
