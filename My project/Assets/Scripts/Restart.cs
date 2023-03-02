using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public GameObject player;
    PlayerController playerScript;
    private string strScore;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void Ranking()
    {
        strScore = playerScript.intScore.ToString();
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(playerScript.intScore);
    }
}
