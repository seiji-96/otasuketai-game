using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
	public float totalTime;
	public int seconds;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        totalTime -= Time.deltaTime;
        if (totalTime <= 3.9)
        {
            seconds = (int)totalTime;
            timerText.text= seconds.ToString();
            if (totalTime<=1 && totalTime>=0.5)
            {
                timerText.text = "";
            }
            if (totalTime<0.99 && totalTime>=0)
            {
                timerText.text = "START!!";
            }
            if (totalTime<0)
            {
                timerText.text = "";
            }
        }
            
    }
}
