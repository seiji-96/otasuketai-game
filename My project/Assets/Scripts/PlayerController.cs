using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.01f;
    private Rigidbody rbody;
    public Canvas canvas;
    public Canvas canvas2;
    private Vector3 pos;
    public bool inMove = false;
    public bool inJump = false;
    public bool canMove = false;
    float FingerPosX0;
    float FingerPosY0;
    float FingerPosNowX;
    float FingerPosNowY;
    float PosDiff=50.0f;

    public GameObject timerObject;
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

    public AudioClip jump;
    public AudioClip move;
    public AudioClip getCoin;
    public AudioClip getShield;
    public AudioClip over;
    public AudioClip count;
    AudioSource audioSource;
    AudioSource audioSource2;

    private bool countS;

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BESTSCORE", 0);
        timerScript = timerObject.GetComponent<StartTimer>();
        audioSource2 = timerObject.GetComponent<AudioSource>();
        rbody = this.GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();     
        shield = transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (timerScript.seconds == 3 && !countS)
        {
            audioSource.PlayOneShot(count);
            countS = true;
        }
        if (timerScript.totalTime<=0.5f)
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
                if(!inMove && !inJump)
                {
                    inJump = true;
                    StartCoroutine("Jump");
                    audioSource.PlayOneShot(jump);
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
                if(!inMove && !inJump)
                {
                    inJump = true;
                    StartCoroutine("Jump");
                }      
            }
            
            if (FingerPosNowX　-　FingerPosX0 >= PosDiff && canMove)
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
            else if (FingerPosNowX　-　FingerPosX0 <= -PosDiff && canMove)
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
        }
    }

    void FixedUpdate()
    {
        if (timerScript.totalTime<=0.5f)
        {
            score += Time.deltaTime * 3 / speed / 220;
            intScore = (int)score;
            scoreText.text = intScore.ToString();
            if(intScore>=bestScore)
            {
                bestScore = intScore;
            }
            PlayerPrefs.SetInt("BESTSCORE", bestScore);
            bestText.text = $"Best Score : {bestScore}";
        }
        //if(speed >= 0.00005)
        //{
            //speed -= 0.000001f;
        //}
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") && !isTransparent)
        {
            audioSource2.Stop(); 
            audioSource.PlayOneShot(over, 0.5f);
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
            audioSource.PlayOneShot(getCoin, 0.3f);
            score += 5;
        }

        if (other.gameObject.CompareTag("invincible"))
        {
            audioSource.PlayOneShot(getShield);
            _someCoroutine = StartCoroutine("Transparent");
        }
    }

    IEnumerator MoveLeft()
    {
        audioSource.PlayOneShot(move, 1.5f);
        for(int i=0; i<5; i++)
        {
            transform.Translate(0.2f, 0, 0);
            yield return new WaitForSeconds(speed);
        }
        inMove = false;
        canMove = false;
    }

    IEnumerator MoveRight()
    {
        audioSource.PlayOneShot(move, 1.5f);
        for(int i=0; i<5; i++)
        {
            transform.Translate(-0.2f, 0, 0);
            yield return new WaitForSeconds(speed);
        }
        inMove = false;
        canMove = false;
    }

    IEnumerator Jump()
    {
        for(int i=1; i<40; i++)
        {
            transform.Translate(0, 0.1f-i*0.005f, 0);
            //if (!(i==48 || i==49))
            //{
                //yield return new WaitForSeconds(speed);
            //}    
            if (!(i==39))
            {
                yield return new WaitForFixedUpdate();
            }   
        }
        inJump = false;
        canMove = false;
    }

    IEnumerator Transparent()
    {
        shield.SetActive(true);
        isTransparent = true;
        yield return new WaitForSeconds(10.0f);
        isTransparent = false;
        shield.SetActive(false);
    }
}
