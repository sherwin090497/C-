using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    private void OnTriggerEnter(Collider playerCollider)
    {
        if (playerCollider.gameObject.tag == "Player")
        {
            // If the player collides with the star in level A1, level A2 will load
            if (this.tag == "Level A1 Exit")
            {
                // if player has all three gems
                if (GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.gemList.Count == 3)
                {
                    int nextSceneNumber = 7;
                    UI_Manager ui_manager = GameObject.Find("UIManager").GetComponent<UI_Manager>();
                    // Set spawn point for level 2
                    GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.SetSpawnPoint(new Vector3(234, 17, 788));
                    // Remove all gems from player
                    GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.RemoveAllGems(GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.gemList.Count);
                    // load next scene
                    ui_manager.LoadSceneByNumber(nextSceneNumber);
                }

            }
            // If the player collides with the star in level A1 the credits will load

            else if (this.tag == "Level A2 Exit")
            {
                Debug.Log("Gem count: " + GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.gemList.Count);
                // If player has all three gems
                if (GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.gemList.Count == 3)
                {
                    // Remove all gems
                    GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.RemoveAllGems(GameObject.Find("PlayerManager").GetComponent<Player_Manager>().player.gemList.Count);
                    int nextSceneNumber = 5;
                    UI_Manager ui_manager = GameObject.Find("UIManager").GetComponent<UI_Manager>();
                    // load credits
                    ui_manager.LoadSceneByNumber(nextSceneNumber);
                }
            }
        }
    }
}

