using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider obj)
    {
    	if (obj.gameObject.tag == "Player")                                                                      // when powerup object collides with the player, it disappear
        {
            Debug.Log("collision with player");
    		GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.AddPowerUp("immunity");      // immunity gets aded to player's power up inventory
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Power Up");                      // play collecting immunity power up soundtrack
            Destroy(gameObject);                                                                                // object get destroyed
        }

    	if (obj.gameObject.tag == "Fireball")                                                                   // when hit by fireball, object disappears
        {
    		Destroy(gameObject);
    	}
    }
}
