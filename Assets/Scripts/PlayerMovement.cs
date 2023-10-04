using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    // global variables
    // public GameOverScreen GameOverScreen;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public float speed = 10;
    public float maxSpeed = 20;
    public float upSpeed = 10;
    private bool onGroundState = true;
    private Rigidbody2D marioBody;
    // public TextMeshProUGUI scoreText;
    public GameObject enemies;
    // public JumpOverGoomba jumpOverGoomba;
    public GameManager gameManager;
    // for audio
    public float deathImpulse = 15;
    public Transform gameCamera;
    public Animator marioAnimator;
    public AudioSource marioAudio;
    public AudioSource marioDeathAudio;
    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);
    private bool moving = false;
    private bool jumpedState = false;

    // state
    [System.NonSerialized]
    public bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
        marioSprite = GetComponent<SpriteRenderer>();
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        // reset everything
        ResetGame();
        // resume time
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // if (col.gameObject.CompareTag("Ground")) onGroundState = true;
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }

        if (col.gameObject.tag == "Enemy" && alive)
        {
            gameManager.KillGoomba(col.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }
        // if (alive)
        // {
        //     float moveHorizontal = Input.GetAxisRaw("Horizontal");
        //     if (Mathf.Abs(moveHorizontal) > 0)
        //     {
        //         Vector2 movement = new Vector2(moveHorizontal, 0);
        //         // check if it doesn't go beyond maxSpeed
        //         if (marioBody.velocity.magnitude < maxSpeed)
        //             marioBody.AddForce(movement * speed);
        //     }

        //     // stop
        //     if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        //     {
        //         // stop
        //         marioBody.velocity.Set(0, marioBody.velocity.y);
        //     }
        //     // jumping
        //     if (Input.GetKeyDown("space") && onGroundState)
        //     {
        //         marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
        //         onGroundState = false;
        //         // update animator state
        //         marioAnimator.SetBool("onGround", onGroundState);
        //     }
        // }
    }

    void Move(int value)
    {

        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.velocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }
    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }

    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy") && alive)
        {
            Debug.Log("Collided with goomba!");

            // play death animation
            marioAnimator.Play("mario-die");
            marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
            alive = false;
        }
    }

    // void GameOverScene()
    // {
    //     // stop time
    //     Time.timeScale = 0.0f;
    //     // set gameover scene
    //     GameOverScreen.Setup(jumpOverGoomba.score); // replace this with whichever way you triggered the game over screen for Checkoff 1
    // }

    private void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(-11.565f, -5.255f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;
        // // reset score
        // scoreText.text = "Score: 0";
        // // reset Goomba
        // foreach (Transform eachChild in enemies.transform)
        // {
        //     eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        // }
        // // reset score
        // jumpOverGoomba.score = 0;

        // GameOverScreen.Deactivate();

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(-4, 0, -10);
    }

    public void GameRestart()
    {
        // reset position
        marioBody.transform.position = new Vector3(-11.565f, -5.255f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(-4, 0, -10);
    }

    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void CallGameOver()
    {
        gameManager.GameOver();
    }


}
