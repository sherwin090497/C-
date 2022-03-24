using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDManager : MonoBehaviour
{

    // Input fields used for output
    protected InputField InputField_Display_Coins;
    protected InputField InputField_Display_Lives;

    // Images to be enabled and disabled
    protected Image Fireball;
    protected Image Immunity;
    protected Image Emerald;
    protected Image Diamond;
    protected Image Ruby;

    // Used for references to player and player manager
    private Player_Manager playerManager;
    private Player_Manager.Player player;

    // Start is called before the first frame update
    void Start()
    {
        InitHUDManager();
    }

    // Update is called once per frame
    void Update()
    {
        // display user coins and lives
        InputField_Display_Coins.text = player.GetCoinCount().ToString();
        InputField_Display_Lives.text = player.GetLives().ToString();
        
    }

    void InitHUDManager()
    {
        // References player and player manager
        playerManager = GameObject.Find("PlayerManager").GetComponent<Player_Manager>();
        player = playerManager.player;

        // References input fields in the HUD
        InputField_Display_Coins = GameObject.Find("InputField_Display_Coins").GetComponent<InputField>();
        InputField_Display_Lives = GameObject.Find("InputField_Display_Lives").GetComponent<InputField>();

        // References Images in the HUD
        Fireball = GameObject.FindGameObjectWithTag("Fireball Image").GetComponent<Image>();
        Immunity = GameObject.FindGameObjectWithTag("Immunity Image").GetComponent<Image>();
        Emerald = GameObject.FindGameObjectWithTag("Emerald Image").GetComponent<Image>();
        Diamond = GameObject.FindGameObjectWithTag("Diamond Image").GetComponent<Image>();
        Ruby = GameObject.FindGameObjectWithTag("Ruby Image").GetComponent<Image>();

        // Disable all gems and power-ups at the beginning
        Fireball.enabled = false;
        Immunity.enabled = false;
        Emerald.enabled = false;
        Diamond.enabled = false;
        Ruby.enabled = false;
    }

    // Enables a gem image if the passed string is of that type
    public void HasGem(string powerUp)
    {
        if (powerUp == "diamond") Diamond.enabled = true;
        else if (powerUp == "ruby") Ruby.enabled = true;
        else if (powerUp == "emerald") Emerald.enabled = true;
    }

    // enables a power up imahe if the passed string is of that type
    public void HasPowerUp(string powerUp)
    {
        if (powerUp == "fireball") Fireball.enabled = true;
        else if (powerUp == "immunity") Immunity.enabled = true;
    }

    // disables all gem images
    public void DisableGemImages()
    {
        Emerald.enabled = false;
        Diamond.enabled = false;
        Ruby.enabled = false;
    }

    // disables the powerup image of the passed type
    public void DisablePowerUpImage(string powerUp)
    {
        if(powerUp == "fireball") Fireball.enabled = false;
        else if (powerUp == "immunity") Immunity.enabled = false;
    }
}
