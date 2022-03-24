using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public GameManager instance;
    private Vector3 respawnPosition;
    private GameObject player;
    public string levelToLoad;

    public bool playerIsRespawning; 

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        // Sets respawn point at start and gets player game object reference at start
        respawnPosition = GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.GetSpawnPoint(); //location of player to respawn
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoRoutine());
    }

    public IEnumerator RespawnCoRoutine()
    {
        // Sets player as inactive
        // disables the cinemachine controller
        // waits 2 seconds
        // sets new player position
        // enables camera
        // sets player active
        // sets player dead to be false
        Debug.Log("Player respawning");
        player.SetActive(false);
        GameObject.Find("Main Camera").GetComponent<CameraController>().cineMachineController.enabled = false;
        yield return new WaitForSeconds(2f);
        player.transform.position = respawnPosition;
        GameObject.Find("Main Camera").GetComponent<CameraController>().cineMachineController.enabled = true;
        player.SetActive(true);
        GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.SetPlayerDead(false);
    }

    // Set new spawn point position
    public void SetSpawnPoint( Vector3 newSpawnPoint ) 
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("New spawnpoint set"); 
    }

    // Wait a few seconds before setting the player responding to true and loading the level.
    public IEnumerator LevelExitCo() 
    {
        
        yield return new WaitForSeconds(2f);
        Debug.Log("Level Ended");

        playerIsRespawning = true; 

        SceneManager.LoadScene(levelToLoad); 

    }

}
