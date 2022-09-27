using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rbody;
    public Canvas canvas;
    private Vector3 pos;
    public bool inMove = false;
    public bool canMove = false;
    float FingerPosX0;
    float FingerPosY0;
    float FingerPosNowX;
    float FingerPosNowY;
    float PosDiff=10.0f;

    private void Start()
    {
        rbody = this.GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        Vector3 tmp = this.transform.position;
        //Move
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(tmp.z <= 1.2f && !inMove && tmp.z >= 0.8f)
            {
                inMove = true;
                StartCoroutine("MoveRight");         
            }

            if(tmp.z <= 0.2f && tmp.z >= -0.2f && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveRight");       
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(tmp.z >= -1.2f && tmp.z <= -0.8f && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveLeft");         
            }

            if(tmp.z <= 0.2f && tmp.z >= -0.2f && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveLeft");        
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(!inMove)
            {
                inMove = true;
                StartCoroutine("Jump");
            }          
        }

        if (Input.GetMouseButtonDown(0))
        {
            FingerPosX0 = Input.mousePosition.x;
            FingerPosY0 = Input.mousePosition.y;
            canMove = true;
        }
        
        if (Input.GetMouseButton(0))
        {
            FingerPosNowX = Input.mousePosition.x;
            FingerPosNowY = Input.mousePosition.y;
        }

        if (FingerPosNowY - FingerPosY0 > PosDiff && canMove)
        {
            if(!inMove)
            {
                inMove = true;
                StartCoroutine("Jump");
            }      
        }
        
        if (FingerPosNowX　-　FingerPosX0 >= PosDiff && canMove)
        {
            if(tmp.z <= 1.2f && !inMove && tmp.z >= 0.8f)
            {
                inMove = true;
                StartCoroutine("MoveRight");         
            }

            if(tmp.z <= 0.2f && tmp.z >= -0.2f && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveRight");       
            }
        }
        else if (FingerPosNowX　-　FingerPosX0 <= -PosDiff && canMove)
        {
            if(tmp.z >= -1.2f && tmp.z <= -0.8f && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveLeft");         
            }

            if(tmp.z <= 0.2f && tmp.z >= -0.2f && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveLeft");        
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            this.enabled = false;
        }
    }

    IEnumerator MoveLeft()
    {
        for(int i=0; i<5; i++)
        {
            transform.Translate(0, 0, 0.2f);
            yield return new WaitForSeconds(0.01f);
        }
        inMove = false;
        canMove = false;
    }

    IEnumerator MoveRight()
    {
        for(int i=0; i<5; i++)
        {
            transform.Translate(0, 0, -0.2f);
            yield return new WaitForSeconds(0.01f);
        }
        inMove = false;
        canMove = false;
    }

    IEnumerator Jump()
    {
        for(int i=0; i<15; i++)
        {
            transform.Translate(0, 0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        for(int i=0; i<15; i++)
        {
            transform.Translate(0, -0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        inMove = false;
        canMove = false;
    }
}
