using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    private Animator playerAnim;
    public ParticleSystem dirtParticle;
    public ParticleSystem explosionParticle;
    public AudioClip crashSound;
    public AudioClip jumpSound;
    private AudioSource playerAudio;
    public bool canDoubleJump = false;
    public bool jumpOnce = false;
    public bool jumpTwice = false;
    public float speed = 1.0f;
    public int score = 0;
    public bool dashModeActivated = false;
    [SerializeField] TextMeshProUGUI m_Object;


    public bool gameOver;




   


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();


    }



    // Update is called once per frame
    void Update()
    {
        //single jump if
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.speed = 1f;



            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            canDoubleJump = true;
        }
        //double jump if
        if (Input.GetKeyDown(KeyCode.Space) && playerRb.velocity.y > 0f && canDoubleJump && !gameOver)
        {
            canDoubleJump = false;

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1);




        }

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            dirtParticle.Stop();
            Debug.Log("Game Over!");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }

        
        else if (Input.GetKeyDown(KeyCode.LeftShift) && isOnGround && !gameOver)
        {
            speed = 2.0f;
            playerAnim.SetFloat("Speed_f", speed);
            dashModeActivated = true;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift) && isOnGround && !gameOver)
        {
            speed = 1.0f;
            playerAnim.SetFloat("Speed_f", speed);
            dashModeActivated = false;

        }
        if (!gameOver)
        {
            score += (int)speed;
            if (dashModeActivated)
            {
                score += 100;
            }
            m_Object.text = "Score:" + score;

        }

        }
    }













