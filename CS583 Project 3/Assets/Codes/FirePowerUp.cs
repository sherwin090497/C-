using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }



    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")                                                                      // when fireball object collides with the player, it disappear
        {
            Debug.Log("collision with player");
            GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.AddPowerUp("fireball");      // fireball gets aded to player's power up inventory
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Power Up");                      // play collecting fireball soundtrack
            Destroy(gameObject);                                                                                // object get destroyed
        }

        if (obj.gameObject.tag == "Fireball")                                                                   // when hit by fireball, object disappears
        {
            Destroy(gameObject);
        }
    }
}
