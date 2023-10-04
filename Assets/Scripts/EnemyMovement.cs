using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    public Vector3 startPosition;

    private EdgeCollider2D enemyHead;

    public Animator Goomba;
    public AudioSource stompSound;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemyHead = GetComponent<EdgeCollider2D>();
        // get the starting position
        originalX = transform.position.x;
        startPosition = transform.localPosition;
        ComputeVelocity();
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move goomba
            Movegoomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }

    public void GameRestart()
    {
        Goomba.SetTrigger("Restart");
        gameObject.SetActive(true);
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
    }

    public void KillGoomba()
    {
        Goomba.SetTrigger("Kill");
    }

    void PlayStompSound()
    {
        // play jump sound
        stompSound.PlayOneShot(stompSound.clip);
    }

    public void RemoveGoomba()
    {
        gameObject.SetActive(false);
    }


}