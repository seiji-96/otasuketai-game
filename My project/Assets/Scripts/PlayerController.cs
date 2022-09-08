using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rbody;
    public Canvas canvas;

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
            if(tmp.z != -1.0){
                this.transform.position += new Vector3(0, 0, -1.0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(tmp.z != 1.0){
                this.transform.position += new Vector3(0, 0, 1.0f);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
