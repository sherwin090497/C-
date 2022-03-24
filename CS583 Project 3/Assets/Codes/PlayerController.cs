using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Player_Manager.Player playerReference;
    LinkedList<GameObject> gems;
    SortedDictionary<string, int> powerUps;
    public CharacterController playerController;
    public Transform cam; //Use main camera
    public Transform groundDetector;
    public LayerMask ground;
    public Transform throwPoint;
    public GameObject playerFb;
    public float gravity = -9.81f;
    public float groundDistance = .4f;
    int fireballForce = 30;
    bool onGround;
    bool fireballActive = false;
    bool canThrow = true;
    public Animator animator;
    float fireballTimeCounter = 0;
    float turnTime = .1f;
    float playerSpeed = 12f;
    float turnVelocity;
    float jumpHeight = 12f;
    Animator anim;
    Vector3 velocity;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public LayerMask npcLayers;
    public Transform jumpPoint;
    public float jumpKillRange = 1f;


    float fireballAmmo = 5f;
    private Behaviour halo;
    float immunityTimeCounter = 0f;

   

    // Start is called before the first frame update
    void Start()
    {
        // get a reference to the player instance and get gems and powerups
        playerReference = GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player;
        gems = playerReference.gemList;
        powerUps = playerReference.powerInventory;

        
        gems = new LinkedList<GameObject>();
        // make player not dead at start
        playerReference.SetPlayerDead(false);
        // disable HUD gem images
        GameObject.Find("HUDManager").GetComponent<HUDManager>().DisableGemImages();
        anim = GetComponent<Animator>();

        // used to display immunity
        halo = (Behaviour)gameObject.GetComponent("Halo");
        halo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if player is dead, don't get input
        if (playerReference.GetPlayerDead()) return;
        // activate fireball, set ammo to 5. remove a fireball powerup and make sound
        if(Input.GetButtonDown("Use FB PowerUp") && !fireballActive)
        {
            if (playerReference.powerInventory.ContainsKey("fireball"))
            {
                fireballAmmo = 5f;
                fireballActive = true;
                playerReference.RemovePowerUp("fireball");
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Power Up");
            }
        }
        // if player is not immune and immunity powerup is consumed, set immune to true and remove an immunity powerup. play sound
        if(!playerReference.GetIsImmune() && Input.GetButtonDown("Use Immunity PowerUp"))
        {
            if (playerReference.powerInventory.ContainsKey("immunity"))
            {
                playerReference.SetIsImmune(true);
                playerReference.RemovePowerUp("immunity");
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Power Up");
            }
        }

        // If player is immune. Set time counter for immunity to 10 seconds. 
        if (playerReference.GetIsImmune())
        {
            if (immunityTimeCounter >= 10f)
            {
                playerReference.SetIsImmune(false);
                halo.enabled = false;
                immunityTimeCounter = 0;
            }
            else if (halo.enabled == false)
            {
                halo.enabled = true;
                immunityTimeCounter += Time.deltaTime;
            }
            else
            {
                immunityTimeCounter += Time.deltaTime;
            }
        }
        // If grounded, reset velocity i y direction
        // Movement based on Brackey's first person movement in unity - fps controller video
        onGround = Physics.CheckSphere(groundDetector.position, groundDistance, ground);
        if(onGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //Movement based on Brackey's third person movement in unity video
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        anim.SetInteger("condition", 0);

        if (direction.magnitude >= .1f)
        {
            // Have third person camera follow player and turn player when camera is moved.
            // Movement based on Brackey's third person movement in unity video
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle + 90f, ref turnVelocity, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerController.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
            
            animator.SetFloat("Speed", direction.magnitude);
            anim.SetInteger("condition", 1);
        }

        // If player is grounded and presses jump, play animation and sound and move player vertically
        // Movement based on Brackey's first person movement in unity - fps controller video
        if (Input.GetButtonDown("Jump") && onGround)
        {
            animator.Play("PlayerSkeleton|Jump");
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        // If left mouse button is clicked, punch enemy

        if (Input.GetMouseButton(0))
        {
            animator.Play("PlayerSkeleton|Punch");
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider enemy in hitEnemies)
            {
                GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Hit");
                enemy.GetComponent<Enemy_Movement>().EnemyDeath();
            }
        }
        // unused
        if (Input.GetMouseButton(1))
        {
            animator.Play("PlayerSkeleton|Kick");
        }
        // unused
        if (Input.GetKeyDown("e"))
        {
            animator.Play("PlayerSkeleton|Block");
            anim.SetInteger("condition", 0);
        }
        // unused
        if (Input.GetKeyDown("r"))
        {
            animator.Play("PlayerSkeleton|Grab");
            anim.SetInteger("condition", 0);
        }
        // Throw fireball if not moving and fireball is active
        if (canThrow)
        {
            if (Input.GetKeyDown("f") && direction.magnitude == 0f)
            {
                if (fireballActive)
                {
                    // wait play animation and wait before instantiating fireball
                    anim.Play("PlayerSkeleton|Throwing");
                    StartCoroutine(WaitForFireball());
                    fireballTimeCounter = 0f;
                    canThrow = false;
                    // decrement fireball ammo
                    fireballAmmo--;
                    // if out of ammo, fireball powerup is inactive
                    if (fireballAmmo <= 0) fireballActive = false;
                }
            }
        }
        else
        {
            // can throw every 1.5 seconds
            if(fireballTimeCounter >= 1.5f)
            {
                canThrow = true;
            }

            else
            {
                fireballTimeCounter += Time.deltaTime;
            }
        }
        // Movement based on Brackey's first person movement in unity - fps controller video
        // Slowly have player descend
        velocity.y += 1.5f * gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);

        // Used to kill enemies if player jumps on them
        Collider[] jumpOnEnemies = Physics.OverlapSphere(jumpPoint.position, jumpKillRange, enemyLayers);
        foreach (Collider enemies in jumpOnEnemies)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Hit");
            enemies.GetComponent<Enemy_Movement>().EnemyDeath();
            
        }
        // Used to kill NPCs if player jumps on them
        Collider[] jumpOnNPC = Physics.OverlapSphere(jumpPoint.position, jumpKillRange, npcLayers);
        foreach (Collider npc in jumpOnNPC)
        {
            
            npc.GetComponent<NPC_Movement>().EnemyDeath();
        }
    }

    // Used when jumping on or attacking enemies
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        if (jumpPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(jumpPoint.position, jumpKillRange);
    }

  
    // Wait .5 seconds before creating a fireball and giving it an instant velocity
    IEnumerator WaitForFireball()
    {
        yield return new WaitForSeconds(.5f);
        GameObject fireball = Instantiate(playerFb, throwPoint.position, Quaternion.Euler(0, 0, 0));
        Rigidbody fireballRB = fireball.GetComponent<Rigidbody>();
        // A force is added to the fireball equivalent to the constant fireball force * the appropriate side of the firepoint upon creation
        fireballRB.AddForce(-throwPoint.forward * fireballForce, ForceMode.Impulse);
    }

    // If game over, diable gem images, empty gem list and diable the halo for the player.
    public void SetDeath(bool isDead)
    {
        playerReference = GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player;
        gems = playerReference.gemList;
        powerUps = playerReference.powerInventory;

        
        gems = new LinkedList<GameObject>();

        GameObject.Find("HUDManager").GetComponent<HUDManager>().DisableGemImages();
        GameObject.Find("HUDManager").GetComponent<HUDManager>().DisablePowerUpImage("fireball");
        GameObject.Find("HUDManager").GetComponent<HUDManager>().DisablePowerUpImage("immunity");
        anim = GetComponent<Animator>();

        halo = (Behaviour)gameObject.GetComponent("Halo");
        halo.enabled = false;
    }

    // If player is already dead or is immune, return.
    // Play dying animation, decrement lives, and set player dead to be true. 
    // If player's lives are <= 0, it's game over. Call set death and do wait for death coroutine
    // else start wait for respawn coroutine
    public void PlayerDeath()
    {
        if (playerReference.GetPlayerDead()) return;
        if (playerReference.GetIsImmune()) return;
        anim.Play("PlayerSkeleton|Dying");
        playerReference.DecrementLives();
        playerReference.SetPlayerDead(true);
        
        if (playerReference.GetLives() <= 0)
        {
            SetDeath(true);
            StartCoroutine(WaitForDeath());
        }
        else
        {
            StartCoroutine(WaitForRespawn());
        }

        
    }

    // wait 3 seconds before respawn
    IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(3.0f);
        GameObject.Find("GameManager").GetComponent<GameManager>().Respawn();
    }

    // wait 3 seconds before loading game over scene
    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(3.0f);
        UI_Manager ui_manager = GameObject.Find("UIManager").GetComponent<UI_Manager>();
        int gameOverSceneNumber = 4;
        ui_manager.LoadSceneByNumber(gameOverSceneNumber);
    }
}
