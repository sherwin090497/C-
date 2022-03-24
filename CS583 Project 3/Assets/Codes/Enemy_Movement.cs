using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public Transform player;
    public Transform groundDetector;
    public LayerMask ground;
    public Animator anim;
    public float groundDistance = .4f;
    float timeCounter = 0f;
    float timeToAttack = 2f;
    bool onGround;
    bool canAttack = true;
    bool enemyDead = false;
    Vector3 velocity;
    Player_Manager.Player playerReference;
    GameObject playerObject;
    void Start()
    {
        anim = GetComponent<Animator>();                                                        // Gets the animation controller for the Enemy 
        player = GameObject.Find("Player").GetComponent<Transform>();                           
        playerReference = GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player;   // Player Reference
        playerObject = GameObject.Find("Player");                                               
    }

    void Update()
    {
        if (playerObject.activeSelf == false) return;
        onGround = Physics.CheckSphere(groundDetector.position, groundDistance, ground);            // ground detector for the enemy 3d object
        if (onGround && velocity.y < 0)                                                             // keeps the enemy face the player when found
        {
            velocity.y = -2f;
        }
        
        Vector3 direction = player.position - this.transform.position;                              // finds the position of the player and enemy rotates towards the player
        if (Vector3.Distance(player.position, this.transform.position) < 2 )                        // distant to detect te player is 2
        {
            direction.y = 0;                                                                        // prevents the enemy from rotating up or down
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);     // direction of the enemy will lock on the player
            anim.SetBool("isIdle", false);                                                          // sets idle = false
            anim.SetBool("isWalking", true);                                                        // calls the walking animation 
            if (direction.magnitude > 1)                                                            // when enemy gets to the distant higher than 1 from the player
            {
                this.transform.Translate(0,0,0.05f);                                                // enemy will keep following the player
                anim.SetBool("isAttacking", false);                                                 // set attacking to false
            }
            else
            {
                anim.SetBool("isWalking", false);                                                   // when the player is in range of the enemy (less than 1)
                if (!playerReference.GetPlayerDead())                                               // checks if player is dead
                {
                    if (canAttack)                                                                  // then enemy will attack the player
                    {
                        anim.SetBool("isAttacking", true);                                          // calls the attacking animation
                        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Hit");   // plays the sound effect for attacking
                        GameObject.Find("Player").GetComponent<PlayerController>().PlayerDeath();   // player will then die
                        canAttack = false;                                                          // once player is dead, enemy will stop attacking
                    }

                    else
                    {
                        if (timeCounter >= timeToAttack)                                            // timer in between attacks
                        {
                            canAttack = true;
                            timeCounter = 0f;
                        }
                        else
                        {
                            timeCounter += Time.deltaTime;                                          // enemy waits 
                        }
                    }
                }
            }
        }
        else
        {
            anim.SetBool("isIdle", true);                           // once player is out of range, enemy will stop attacking and stay on idle
            anim.SetBool("isWalking", false);                          
            anim.SetBool("isAttacking", false);
        }
    }

    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Fireball")               // when enemy is hit by the fireball, the enemy dies
        {
            EnemyDeath();                                   // calls function enemyDeath()
        }
    }

    public void EnemyDeath()
    {
        if (enemyDead) return;                          
        enemyDead = true;                                                                       // sets enemy to die
        anim.SetBool("isDead", true);                                                           // calls dying animation
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Enemy Death");       // plays the dying soundtrack
        this.enabled = false;                                                                   // after enemy dies, it disables this script
        StartCoroutine(WaitForDeath());                                                         // calls waitForDeath() function
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(3.0f);          // enemy waits for 3 secs as a dead body before disappearing
        Destroy(gameObject);
    }
}


