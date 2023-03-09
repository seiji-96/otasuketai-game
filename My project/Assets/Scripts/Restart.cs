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
    private GameObject parent ;
    public GameObject black;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject ;
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

    public void Easy()
    {
        playerScript.diffic = 0;
        Time.timeScale = 1;
        parent.SetActive(false);
        black.SetActive(false);
    }

    public void Normal()
    {
        playerScript.diffic = 1;
        Time.timeScale = 1;
        parent.SetActive(false);
        black.SetActive(false);
    }

    public void Hard()
    {
        playerScript.diffic = 2;
        Time.timeScale = 1;
        parent.SetActive(false);
        black.SetActive(false);
    }

    public void Jigoku()
    {
        playerScript.diffic = 3;
        Time.timeScale = 1;
        parent.SetActive(false);
        black.SetActive(false);
    }
}
