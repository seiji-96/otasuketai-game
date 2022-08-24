using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movePawor = 5f;

    private Rigidbody m_Rigidbody;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Move
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_Rigidbody.AddForce(Vector3.right * movePawor, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_Rigidbody.AddForce(Vector3.right * (-1) *movePawor, ForceMode.Impulse);
        }
    }
}
