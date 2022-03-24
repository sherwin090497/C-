using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If play triggers the collider, it will call the player death function
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Entered Killzone");
            GameObject.Find("Player").GetComponent<PlayerController>().PlayerDeath();
        }
    }
}
