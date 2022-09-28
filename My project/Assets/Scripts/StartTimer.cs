using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
	public float totalTime;
	int seconds;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        totalTime -= Time.deltaTime;
		seconds = (int)totalTime;
		timerText.text= seconds.ToString();
        if (totalTime<=0 && totalTime>=-0.5)
        {
            timerText.text = "Start";
        }
        if (totalTime<-0.5)
        {
            timerText.text = "";
        }
    }
}
