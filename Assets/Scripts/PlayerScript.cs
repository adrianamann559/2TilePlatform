using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text liveText;

    public Text winText;

    public Text loseText;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    Animator anim;

    private int scoreValue = 0;

    private int liveValue = 3;

    private bool facingRight = true;

    private bool isOnGround;

    public Transform groundcheck;

    public float checkRadius;

    public LayerMask allGround; 



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        loseText.text = "";
        liveText.text = liveValue.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>();
    
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }

        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        
        if (liveValue <= 0)
        {
           loseText.text = "You Lose! Try Again!";
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        
    }

     void Update()
        {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
            //Set the integer named "States" in your Animator to 1. If the Animator is set up properly, this should trigger an animation.
        anim.SetInteger("State", 1);
        else if (Input.GetKey(KeyCode.W))
            anim.SetInteger("State", 2);
        else
            //If all the other keys are let go, set the "States" integer to 0.
            anim.SetInteger("State", 0);
        }
        
        void SetScoreValue ()
        {
            score.text = scoreValue.ToString();
            if (scoreValue == 4)
            {
                transform.position = new Vector3 (0f , 20f , 0f);
                liveValue = 3; 
                liveText.text = liveValue.ToString();
            }
            if (scoreValue >= 8)
        {
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
            winText.text = "You Win! Game by Adriana M ";
            

        }
        }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue = scoreValue + 1;
            SetScoreValue();
            Destroy(collision.collider.gameObject);
            
            
        }

         if (collision.collider.tag == "Enemy")
        {
            liveValue -= 1;
            liveText.text = liveValue.ToString();
            Destroy(collision.collider.gameObject);
        }
       

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
        if (Input.GetKey(KeyCode.W))
        {
        rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
        }
        }
        }

    }

   

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    

     
}