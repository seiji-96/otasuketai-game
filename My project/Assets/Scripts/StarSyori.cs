using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSyori : MonoBehaviour
{
    public List<GameObject> stage;
    private GameObject timerObject;
    StartTimer timerScript;

    GameObject[] step = new GameObject[10];

    public float speed;
    private float disappear = -10;
    private float respawn = 30;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        timerObject = GameObject.Find("GameObject");
        timerScript = timerObject.GetComponent<StartTimer>();
        for (int i = 0; i < step.Length; i++)
        {
            step[i] = Instantiate(stage[i], new Vector3(100 * (i+1) , (-1) * Random.Range(1, 200) , Random.Range(80, 100) * Mathf.Pow(-1, i)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (timerScript.totalTime<=0)
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
        
}
