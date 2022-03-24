using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyObject : MonoBehaviour
{
    void Start()
    {
        
    }

 

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")             // when gem collides with the player, it disappear
        {
            Debug.Log("collision with player");
            GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.AddGem(gameObject, "ruby");      // gem gets added to player inventory
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Gems");                              // plays gem sountrack
            Destroy(gameObject);                                                                                    // object gets destroyed
        }

        if (obj.gameObject.tag == "Fireball")           // when hit by fireball, object disappears
        {
            Destroy(gameObject);
        }
    }
}

