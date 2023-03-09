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
    StartTimer timerScript;

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

    private float currentScale;
    private float nextTime;
	public float interval = 1.0f;

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
                if(!inMove && !inJump && sushi>=100)
                {
                    inMove = true;
                    StartCoroutine("Rocket");
                    sushi = 0;
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
            if(diffic==0)
            {
                Time.timeScale = 1f;
                score += Time.deltaTime * speed * 10 / 2 ;
                speed += 0.00005f;
                Time.timeScale += 0.00005f;
            }
            if(diffic==1)
            {
                Time.timeScale = 1.5f;
                score += Time.deltaTime * speed * 10 ;
                speed += 0.0001f;
                Time.timeScale += 0.0001f;
            }
            if(diffic==2)
            {
                Time.timeScale = 3f;
                score += Time.deltaTime * speed * 10 * 2.5f ;
                speed += 0.0003f;
                Time.timeScale += 0.0005f;
            }
            if(diffic==3)
            {
                Time.timeScale = 3.5f;
                score += Time.deltaTime * speed * 10 * 5 ;
                speed += 0.0005f;
                Time.timeScale += 0.001f;
            }
                
            intScore = (int)score;
            scoreText.text = intScore.ToString();
            bestText.text = $"Current Score : {intScore}";
        }
            
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
            power.value++;
            sushi++;
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
        currentScale = speed;
        speed *= 2;
        isTransparent = true;
        for(int i=0; i<20; i++)
        {
            transform.Translate(0, 0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(10);
        speed = currentScale;

        for(int i=0; i<20; i++)
        {
            transform.Translate(0, -0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }

        inMove = false;
        canMove = false;

        shield.SetActive(true);

        yield return new WaitForSeconds(3);

        shield.SetActive(false);

        isTransparent = false;
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
        /*else if(speed >= 0.00007)
        {
            for(int i=1; i<40; i++)
            {
                transform.Translate(0, 0.2f-i*0.01f, 0);
                //if (!(i==48 || i==49))
                //{
                    //yield return new WaitForSeconds(speed);
                //}    
                if (!(i==39))
                {
                    yield return new WaitForSeconds(speed);
                }   
            }        
        }
        else
        {
            for(int i=1; i<40; i++)
            {
                transform.Translate(0, 0.4f-i*0.02f, 0);
                //if (!(i==48 || i==49))
                //{
                    //yield return new WaitForSeconds(speed);
                //}    
                if (!(i==39))
                {
                    yield return new WaitForSeconds(speed);
                }   
            }        
        }*/
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
