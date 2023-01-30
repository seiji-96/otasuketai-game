using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public List<GameObject> enemy;

    GameObject[] step = new GameObject[4];

    public float speed = 10;
    private float disappear = -10;
    private float respawn = 30;


    private GameObject timerObject;
    StartTimer timerScript;
    
 
    // Start is called before the first frame update
    void Start()
    {
        timerObject = GameObject.Find("GameObject");
        timerScript = timerObject.GetComponent<StartTimer>();
        for (int i = 0; i < 4; i++)
        {
            step[i] = Instantiate(enemy[Random.Range(0,7)], new Vector3(20 * i, -6, 4.7f), Quaternion.Euler(0, 90, 0));
        }
    }

    void FixedUpdate()
    {
        if (timerScript.totalTime<=1)
        {
            for (int i = 0; i < 4; i++)
            {
                step[i].gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                if (step[i].gameObject.transform.position.x < disappear)
                {
                    Destroy(step[i]);
                    step[i] = Instantiate(enemy[Random.Range(0,7)], new Vector3(40 , -6, 4.7f), Quaternion.Euler(0, 90, 0));
                }
            }
            speed = 1.000001f * speed;
        }
    }

}
