using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleStartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartClick()
    {
        Invoke("StartGame", 1.5f);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
