using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rbody;

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
                this.transform.position += new Vector3(0, 0, -0.5f);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(tmp.z != 1.0){
                this.transform.position += new Vector3(0, 0, 0.5f);
            }
        }
    }
}
