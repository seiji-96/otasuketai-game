using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    private Rigidbody rbody;
    public Canvas canvas;
    public Canvas canvas2;
    private Vector3 pos;
    public bool inMove = false;
    public bool canMove = false;
    float FingerPosX0;
    float FingerPosY0;
    float FingerPosNowX;
    float FingerPosNowY;
    float PosDiff=10.0f;

    private GameObject timerObject;
    StartTimer timerScript;

    private float score = 0;
    private int intScore = 0;
    private static int bestScore;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestText;

    public bool isTransparent = false;

    MeshRenderer mesh;
    GameObject shield;

    Coroutine _someCoroutine;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BESTSCORE", 0);
        timerObject = GameObject.Find("GameObject");
        timerScript = timerObject.GetComponent<StartTimer>();
        rbody = this.GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();     
        shield = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        Vector3 tmp = this.transform.position;
        //Move
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(tmp.z+1.167 <= 1.2f && !inMove && tmp.z+1.167 >= 0.8f)
            {
                inMove = true;
                StartCoroutine("MoveRight");         
            }

            if(tmp.z+1.167 <= 0.2f && tmp.z+1.167 >= -0.2f && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveRight");       
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(tmp.z+1.167 >= -1.2f && tmp.z+1.167 <= -0.8f && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveLeft");         
            }

            if(tmp.z+1.167 <= 0.2f && tmp.z+1.167 >= -0.2f && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveLeft");        
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(!inMove)
            {
                inMove = true;
                StartCoroutine("Jump");
            }          
        }

        if (Input.GetMouseButtonDown(0))
        {
            FingerPosX0 = Input.mousePosition.x;
            FingerPosY0 = Input.mousePosition.y;
            canMove = true;
        }
        
        if (Input.GetMouseButton(0))
        {
            FingerPosNowX = Input.mousePosition.x;
            FingerPosNowY = Input.mousePosition.y;
        }

        if (FingerPosNowY - FingerPosY0 > PosDiff && canMove)
        {
            if(!inMove)
            {
                inMove = true;
                StartCoroutine("Jump");
            }      
        }
        
        if (FingerPosNowX　-　FingerPosX0 >= PosDiff && canMove)
        {
            if(tmp.z <= 1.2f && !inMove && tmp.z >= 0.8f)
            {
                inMove = true;
                StartCoroutine("MoveRight");         
            }

            if(tmp.z <= 0.2f && tmp.z >= -0.2f && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveRight");       
            }
        }
        else if (FingerPosNowX　-　FingerPosX0 <= -PosDiff && canMove)
        {
            if(tmp.z >= -1.2f && tmp.z <= -0.8f && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveLeft");         
            }

            if(tmp.z <= 0.2f && tmp.z >= -0.2f && !inMove)
            {
                inMove = true;
                StartCoroutine("MoveLeft");        
            }
        }
    }

    void FixedUpdate()
    {
        if (timerScript.totalTime<=1)
        {
            score += Time.deltaTime * 10;
            intScore = (int)score;
            scoreText.text = intScore.ToString();
            if(intScore>=bestScore)
            {
                bestScore = intScore;
            }
            PlayerPrefs.SetInt("BESTSCORE", bestScore);
            bestText.text = $"Best Score : {bestScore}";
        }
        speed = 1.00001f * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") && !isTransparent)
        {
            canvas.gameObject.SetActive(true);
            canvas2.gameObject.SetActive(true);
            Time.timeScale = 0;
            this.enabled = false;
        }

        if (other.gameObject.CompareTag("enemy") && isTransparent)
        {
            StopCoroutine(_someCoroutine);
            isTransparent = false;
            shield.SetActive(false);
        }

        if (other.gameObject.CompareTag("coin"))
        {
            score += 20;
        }

        if (other.gameObject.CompareTag("invincible"))
        {
            _someCoroutine = StartCoroutine("Transparent");
        }
    }

    IEnumerator MoveLeft()
    {
        for(int i=0; i<5; i++)
        {
            transform.Translate(0.2f, 0, 0);
            yield return new WaitForSeconds(0.01f*speed);
        }
        inMove = false;
        canMove = false;
    }

    IEnumerator MoveRight()
    {
        for(int i=0; i<5; i++)
        {
            transform.Translate(-0.2f, 0, 0);
            yield return new WaitForSeconds(0.01f*speed);
        }
        inMove = false;
        canMove = false;
    }

    IEnumerator Jump()
    {
        for(int i=1; i<50; i++)
        {
            transform.Translate(0, 0.1f-i*0.004f, 0);
            yield return new WaitForSeconds(0.008f*speed);
        }
        inMove = false;
        canMove = false;
    }

    IEnumerator Transparent()
    {
        shield.SetActive(true);
        isTransparent = true;
        yield return new WaitForSeconds(5.0f);
        isTransparent = false;
        shield.SetActive(false);
    }
}
