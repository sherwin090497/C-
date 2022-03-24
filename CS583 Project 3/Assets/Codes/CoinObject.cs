using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : MonoBehaviour
{
    void Start()
    {

    }

    /*private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            AudioManager.PlaySound("Coin-Sound");
            GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.IncreaseCoinCount(1);
            Destroy(gameObject);

        }
         if (obj.gameObject.tag == "Fireball")
        {
            AudioManager.PlaySound("Coin-Sound");
            Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")             // when player collides, coins disappear
        {
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Coin");          // plays coin soundtrack
            GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.IncreaseCoinCount(1);    // increase the coin count
            Destroy(gameObject);                    // destroys the coin
        }

        if (obj.gameObject.tag == "Fireball")       // get destroyed when hit by fireball
        {
            Destroy(gameObject);
        }
    }
}
