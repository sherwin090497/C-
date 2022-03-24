using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;



[SerializeField]
public class Player_Manager : MonoBehaviour
{
    [Serializable]
    public class Player
    {
        private int lives = 4;
        public SortedDictionary<string, int> powerInventory = new SortedDictionary<string, int>();
        private int coinCount = 0;
        public LinkedList<GameObject> gemList = new LinkedList<GameObject>();

        private bool playerDead = false;

        private Vector3 spawnPoint = new Vector3(300, 5, 700);

        bool isImmune = false;
        // unused game over
        public bool GameOver()
        {
            if (playerDead) return true;
            return false;
        }

        private float velocity;
        // unused player velocity
        public void SetVelocity(float swiftness)
        {
            if (swiftness >= 0) velocity = swiftness;
            
        }
        // unused player velocity
        public float GetVelocity()
        {
            return velocity;
        }
        // return number of lives
        public int GetLives()
        {
            return lives;
        }
        // increment lives
        public void IncrementLives()
        {
            lives++;
        }
        // Decrement lives
        public void DecrementLives()
        {
            lives--;
            if (lives <= 0) playerDead = true;
        }
        // Get the coin count
        public int GetCoinCount()
        {
            return coinCount;
        }
        // make player immune
        public void SetIsImmune(bool immunity)
        {
            isImmune = immunity;
        }
        // return if player is immune
        public bool GetIsImmune()
        {
            return isImmune;
        }
        // Increase the number of coins
        public void IncreaseCoinCount(int coinsAdded)
        {
            if(coinsAdded > 0) coinCount += coinsAdded;
        }
        // Decrease the coin coint by the amount passed
        public void DecreaseCoinCount(int coinsRemoved)
        {
            if (coinsRemoved <= 0) return;
            if (coinsRemoved > coinCount) coinCount = 0;
            else coinCount -= coinsRemoved;
        }
        // Get the spawn point
        public Vector3 GetSpawnPoint()
        {
            return spawnPoint;
        }
        // Set spawn point to a new location
        public void SetSpawnPoint(Vector3 location)
        {
            spawnPoint = location;
        }
        // If player has 100 or more coins, reduce by 100 and increment life count
        public void CheckCointCount()
        {
            if (GetCoinCount() >= 100)
            {
                DecreaseCoinCount(100);
                IncrementLives();
            }
        }
        // Check if player has all 3 gems and increment life
        public void CheckGemCount()
        {
            if (gemList.Count == 3)
            {
                IncrementLives();
            }
        }
        // Set lives to an amount
        public void SetLives(int lifeCount)
        {
            lives = lifeCount;
        }
        // Remove all powerups from player
        public void RemoveAllPowerUps()
        {
            powerInventory = new SortedDictionary<string, int>();
        }
        // Set coins to a value
        public void SetCoins(int coins)
        {
            coinCount = coins;
        }
        // Add a gem to the gem list and enable the image in the hud
        public void AddGem(GameObject gem, string gem_type)
        {
            if (gem_type == "diamond")
            {
                Debug.Log("got diamond");
            }

            if (gem_type == "emerald")
            {
                Debug.Log("got emerald");
            }

            if (gem_type == "ruby")
            {
                Debug.Log("got ruby");
            }
            gemList.AddLast(gem);
            GameObject.Find("HUDManager").GetComponent<HUDManager>().HasGem(gem_type);
            Debug.Log("Gem Count: " + gemList.Count);

            CheckGemCount();
        }
        // Set player to be dead
        public void SetPlayerDead(bool isDead)
        {
            playerDead = isDead;
        }
        // Return if player is dead
        public bool GetPlayerDead()
        {
            return playerDead;
        }
        // Add a powerup and the powerup image if it did not exist before
        public void AddPowerUp(string powerUp)
        {
            if (powerInventory.ContainsKey(powerUp))
            {
                int currentCount = powerInventory[powerUp];
                powerInventory[powerUp] = currentCount + 1;
            }
            else
            {
                powerInventory.Add(powerUp, 1);
                GameObject.Find("HUDManager").GetComponent<HUDManager>().HasPowerUp(powerUp);
            }
        }
        // Remove a powerup and either decrement the value if it's >1 or remove the key value pair from list and disable HUD image
        public void RemovePowerUp(string powerUp)
        {
            if (powerInventory.ContainsKey(powerUp))
            {
                int currentCount = powerInventory[powerUp];
                powerInventory[powerUp] = currentCount - 1;
                if (powerInventory[powerUp] <= 0)
                {
                    powerInventory.Remove(powerUp);
                    GameObject.Find("HUDManager").GetComponent<HUDManager>().DisablePowerUpImage(powerUp);
                }
            }
            else
            {
                return;
            }
        }
        // Remove gems from gem list
        public void RemoveAllGems(int gemCount)
        {
            for(int i = 0; i < gemCount; i++)
            {
                gemList.RemoveFirst();
            }
        }
    };

    // Singleton pattern for Player Manager so that player info persists through scenes
    // Based on code shown for the same purpose by Professor Price at line 23 in UIManager-Simple code
    private static Player_Manager Instance;

    public Player player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            player = new Player();
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
