using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Play fireball sound and destroy the game object on collision
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Fireball");
        Destroy(gameObject);
    }
}
