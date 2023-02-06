using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public List<GameObject> enemy;

    GameObject[] step = new GameObject[5];

    public float speed = 10;
    private float disappear = -2;
    private float respawn = 30;
    private int count = 0;


    private GameObject timerObject;
    StartTimer timerScript;
    
 
    // Start is called before the first frame update
    void Start()
    {
        timerObject = GameObject.Find("GameObject");
        timerScript = timerObject.GetComponent<StartTimer>();
        for (int i = 0; i < 5; i++)
        {
            step[i] = Instantiate(enemy[Random.Range(0,24)], new Vector3(8.2f * (i+2), -6, 4.7f), Quaternion.Euler(0, 90, 0));
        }
    }

    void FixedUpdate()
    {
        if (timerScript.totalTime<=0.5)
        {
            for (int i = 0; i < 5; i++)
            {
                step[i].gameObject.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
                if (step[i].gameObject.transform.position.x < disappear && count<=20)
                {
                    Destroy(step[i]);
                    step[i] = Instantiate(enemy[Random.Range(0,24)], new Vector3(41 , -6, 4.7f), Quaternion.Euler(0, 90, 0));
                    count += 1;
                }
                if (step[i].gameObject.transform.position.x < disappear && count==21)
                {
                    Destroy(step[i]);
                    step[i] = Instantiate(enemy[9], new Vector3(41 , -6, 4.7f), Quaternion.Euler(0, 90, 0));
                    count += 1;
                }
                if (step[i].gameObject.transform.position.x < disappear && count==22)
                {
                    Destroy(step[i]);
                    step[i] = Instantiate(enemy[9], new Vector3(41 , -6, 4.7f), Quaternion.Euler(0, 90, 0));
                    count += 1;
                }
                if (step[i].gameObject.transform.position.x < disappear && count==23)
                {
                    Destroy(step[i]);
                    step[i] = Instantiate(enemy[9], new Vector3(41 , -6, 4.7f), Quaternion.Euler(0, 90, 0));
                    count = 0;
                }
                
                
            }
            speed = 1.00008f * speed;
        }
    }

}
