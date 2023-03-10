using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;
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
    public StartTimer timerScript;

    private float score = 0;
    public int intScore = 0;
    private static int bestScore;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestText;

    public bool isTransparent = false;

    MeshRenderer mesh;
    GameObject shield;
    public GameObject slider;

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
    public int diffic;
    private Slider power;
    private int sushi;

    public float currentScale;
    private float nextTime;
	public float interval = 1.0f;
    private bool muteki;

    public GameObject black;
    public GameObject pause;

    private void Start()
    {
        timerScript = timerObject.GetComponent<StartTimer>();
        audioSource2 = timerObject.GetComponent<AudioSource>();
        rbody = this.GetComponent<Rigidbody>();
        mesh = GetComponent<MeshRenderer>();     
        shield = transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();
        power = slider.GetComponent<Slider>();
        nextTime = Time.time;
        Time.timeScale = 0;
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
            audioSource2.Play();
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

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(!inMove && !inJump && sushi>=3)
                {
                    inMove = true;
                    StartCoroutine("Rocket");
                    audioSource.PlayOneShot(jump);
                }          
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                audioSource2.Stop(); 
                currentScale = Time.timeScale;
                Time.timeScale = 0;
                black.SetActive(true);
                pause.SetActive(true);
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
            if(diffic==0)
            {
                Time.timeScale = 1f;
                score += Time.deltaTime * speed * 15 / 3 ;
                speed += 0.00005f;
                Time.timeScale += 0.00005f;
            }
            if(diffic==1)
            {
                Time.timeScale = 2f;
                score += Time.deltaTime * speed * 15 / 2;
                speed += 0.0001f;
                Time.timeScale += 0.0001f;
            }
            if(diffic==2)
            {
                Time.timeScale = 3f;
                score += Time.deltaTime * speed * 15;
                speed += 0.0002f;
                Time.timeScale += 0.0005f;
            }
            if(diffic==3)
            {
                Time.timeScale = 4f;
                score += Time.deltaTime * speed * 15 * 2 ;
                speed += 0.0003f;
                Time.timeScale += 0.001f;
            }
                
            intScore = (int)score;
            scoreText.text = intScore.ToString();
            bestText.text = $"Current Score : {intScore}";
        }
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") && !isTransparent && !muteki)
        {
            audioSource2.Stop(); 
            audioSource.PlayOneShot(over, 0.5f);
            canvas.gameObject.SetActive(true);
            canvas2.gameObject.SetActive(true);
            Time.timeScale = 0;
            this.enabled = false;
        }

        if (other.gameObject.CompareTag("enemy") && isTransparent && !muteki)
        {
            StopCoroutine("Transparent");
            isTransparent = false;
            shield.SetActive(false);
        }

        if (other.gameObject.CompareTag("coin"))
        {
            audioSource.PlayOneShot(getCoin, 0.3f);
            score += 5*(1 + speed);
            power.value++;
            sushi++;
        }

        if (other.gameObject.CompareTag("invincible"))
        {
            audioSource.PlayOneShot(getShield);
            StopCoroutine("Transparent");
            StartCoroutine("Transparent");
        }
    }

    IEnumerator MoveLeft()
    {
        audioSource.PlayOneShot(move, 1.5f);
        for(int i=0; i<5; i++)
        {
            transform.Translate(0.2f, 0, 0);
            yield return new WaitForSeconds(0.01f);
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
            yield return new WaitForSeconds(0.01f);
        }
        inMove = false;
        canMove = false;
    }

    IEnumerator Rocket()
    {
        power.value = 0;
        sushi = 0;
        currentScale = Time.timeScale;
        Time.timeScale *= 5;
        muteki = true;
        for(int i=0; i<20; i++)
        {
            transform.Translate(0, 0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(10);
        Time.timeScale = currentScale;

        for(int i=0; i<20; i++)
        {
            transform.Translate(0, -0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }

        inMove = false;
        canMove = false;

        shield.SetActive(true);

        yield return new WaitForSeconds(7);

        shield.SetActive(false);

        muteki = false;
    }

    IEnumerator Jump()
    {
            for(int i=1; i<50; i++)
            {
                transform.Translate(0, 0.1f-i*0.004f, 0);
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
        yield return new WaitForSeconds(15.0f);
        isTransparent = false;
        shield.SetActive(false);        
    }
}
