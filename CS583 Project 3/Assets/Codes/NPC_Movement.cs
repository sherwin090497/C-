using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float speed = 100;
    public float groundDistance = .4f;
    
    private bool wandering = false;
    private bool left = false;
    private bool right = false;
    private bool walk = false;
    public Animator anim;

    public Transform groundDetector;
    public LayerMask ground;
    bool onGround;
    bool npcDead = false;
    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.CheckSphere(groundDetector.position, groundDistance, ground);              // NPC detects the ground
        if (onGround && velocity.y < 0)                                                              
        {
            velocity.y = -2f;
        }

        if (wandering == false)                                                                         // if NPC is not wandering, then NPC will wander
        {
            StartCoroutine(playerWander());
        }
        if (right == true)
        {
            this.transform.Rotate(0.0f, (speed * Time.deltaTime), 0.0f);            // NPC rotates right
        }
        if (left == true)
        {
            this.transform.Rotate(0.0f, -(speed * Time.deltaTime), 0.0f);           // NPC rotates left
        }
        if (walk == true)   
        {
            this.transform.position += -transform.right * moveSpeed * Time.deltaTime;       // NPC walks forward
        }
    }


    IEnumerator playerWander()
    {
        int rotation = Random.Range(1, 3);                  // Range in secs
        int rotationWait = Random.Range(4, 5);             
        int rotate = Random.Range(1, 3);
        int pause = Random.Range(1, 4);
        int walking = Random.Range(1, 5);

        wandering = true;                                   
        yield return new WaitForSeconds(pause);             // If NPC is wandering, it wait for a random range between 1 - 4
        walk = true;                                        // NPC will walk
        anim.SetBool("Walking", true);                      // if walk is true, it calls walking animation
        yield return new WaitForSeconds(walking);           // NPC keeps walking until the range of 1 - 5 secs
        anim.SetBool("Walking", false);                     // then NPC will stop walking
        walk = false;
        yield return new WaitForSeconds(rotationWait);      // NPC will wait before it rotates
        if (rotate == 1)                                    // 1 means rotate to the right
        {
            right = true;
            yield return new WaitForSeconds(rotation);      // starts rotating
            right = false;
        }
        if (rotate == 2)                                    // 2 means rotate to the left
        {
            left = true;
            yield return new WaitForSeconds(rotation);      // starts rotating
            left = false;
        }
        wandering = false;      // npc will stop to wander
    }

    public void EnemyDeath()  // NPC death
    {
        if (npcDead) return;
        npcDead = true;
        anim.SetBool("isDead", true);           // calls death animation
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("NPC-Death");   // plays dying sound
        this.enabled = false;               // disable the script after dying 
        StartCoroutine(WaitForDeath());         
    }

    IEnumerator WaitForDeath()          // NPC will wait for 3 secs as a dead object before disappearing
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
