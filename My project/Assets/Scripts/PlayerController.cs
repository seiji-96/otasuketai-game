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
    }

    IEnumerator MoveRight()
    {
        for(int i=0; i<5; i++)
        {
            transform.Translate(0, 0, -0.2f);
            yield return new WaitForSeconds(0.01f);
        }
        inMove = false;
    }

}
