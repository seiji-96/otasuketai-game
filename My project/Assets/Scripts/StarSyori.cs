using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSyori : MonoBehaviour
{
    public List<GameObject> stage;

    GameObject[] step = new GameObject[10];

    public float speed = 3;
    private float disappear = -10;
    private float respawn = 30;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < step.Length; i++)
        {
            step[i] = Instantiate(stage[i], new Vector3(4 * i, 10 * (-1)^i, 3), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        for (int i = 0; i < step.Length; i++)
        {
            speed = 1.000001f * speed;
            step[i].gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (step[i].gameObject.transform.position.x < disappear)
            {
                step[i].gameObject.transform.position = new Vector3(respawn, 0, 0);
            }
        }
    }
        
}
