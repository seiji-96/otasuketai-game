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
            rbody.velocity = new Vector3(0, 0, -3.0f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rbody.velocity = new Vector3(0, 0, 3.0f);
        }
    }
}
