using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaneScript : MonoBehaviour
{
    public List<GameObject> stage;

    GameObject[] step = new GameObject[10];

    public float speed = 10;
    private float disappear = -10;
    private float respawn = 30;


    private GameObject timerObject;
    StartTimer timerScript;
    
    
    void Start()
    {
        timerObject = GameObject.Find("GameObject");
        timerScript = timerObject.GetComponent<StartTimer>();
        for (int i = 0; i < step.Length; i++)
        {
            step[i] = Instantiate(stage[i], new Vector3(4 * i, 0, 0), Quaternion.identity);
        }
    }
    
    void FixedUpdate()
    {
        if (timerScript.totalTime<=0)
        {
            for (int i = 0; i < step.Length; i++)
            {
                step[i].gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                if (step[i].gameObject.transform.position.x < disappear)
                {
                    step[i].gameObject.transform.position = new Vector3(respawn, 0, 0);
                }
            }
            speed = 1.000001f * speed;
        }
    }
}
