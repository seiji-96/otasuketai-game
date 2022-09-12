using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneScript : MonoBehaviour
{
    public List<GameObject> stage;

    GameObject[] step = new GameObject[10];

    public float speed = 10;
    private float disappear = -10;
    private float respawn = 30;


    private GameObject timerObject;
    StartTimer timerScript;

    private float score = 0;
    private int intScore = 0;
    private static int bestScore;

    public Text scoreText;
    public Text bestText;
    
    
    void Start()
    {
        timerObject = GameObject.Find("GameObject");
        timerScript = timerObject.GetComponent<StartTimer>();
        for (int i = 0; i < step.Length; i++)
        {
            step[i] = Instantiate(stage[i], new Vector3(4 * i, 0, 0), Quaternion.identity);
        }
    }
    
    void Update()
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
            score += Time.deltaTime * 10;
            intScore = (int)score;
            scoreText.text = intScore.ToString();
            if(intScore>=bestScore)
            {
                bestScore = intScore;
            }
            bestText.text = $"Best Score : {bestScore}.";
        }
    }
}
