using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private float disappear = -3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //speed = 1.000001f * speed;
        //this.transform.position = new Vector3(this.transform.position.x-speed*Time.deltaTime, this.transform.position.y, this.transform.position.z);
    }

}
