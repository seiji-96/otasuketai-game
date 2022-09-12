using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rbody;
    public Canvas canvas;
    private Vector3 pos;
    private bool inMove = false;

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
            if(tmp.z != -1.0 && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveRight");          
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(tmp.z != 1.0 && !inMove)
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
        for(int i=0; i<10; i++)
        {
            transform.Translate(0, 0, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }

        inMove = false;
    }

    IEnumerator MoveRight()
    {
        for(int i=0; i<10; i++)
        {
            transform.Translate(0, 0, -0.1f);
            yield return new WaitForSeconds(0.01f);
        }

        inMove = false;
    }

}
