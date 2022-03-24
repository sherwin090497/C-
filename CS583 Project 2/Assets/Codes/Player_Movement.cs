using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float X, Y;
    public float Speed;
    public float BoostSpeed;
    public float defaultSpeed;
    public float boostTimer;
    public int maxHealth = 100;
    public int currentHealth;  
    public int coinScore = 1;
    public static int points = 0;
    public HealthBar healthBar;
    public bool boost;

    Animator Player_Animation;
    const string Boost_Anim = "Boost_Activate";
    const string Heal_Anim = "Heal_Activate";

    private void Start()
    {
        Player_Animation = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        defaultSpeed = Speed;
        boostTimer = 0;
        boost = false;

    }

    private void Update()
    {
        X = Input.GetAxis("Horizontal") * Speed;
        Y = Input.GetAxis("Vertical") * Speed;

        if (boost == true)
        {
            Player_Animation.SetTrigger(Boost_Anim);
            Speed = BoostSpeed;
            boostTimer += Time.deltaTime;
            if(boostTimer >= 2)
            {
                Speed = defaultSpeed;
                boostTimer = 0;
                boost = false;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(X, Y);
    }

    private void OnCollisionEnter2D(Collision2D car)
    {
        if (car.gameObject.tag.Equals("Police"))
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(3);
            Sound_Manager.PlaySound("Scraping-Sound");
            points = 0;
        }
        
        if (car.gameObject.tag.Equals("Small_Vehicle"))
        {
            TakeDamage(3);
            Sound_Manager.PlaySound("Scraping-Sound");
            if (currentHealth <= 0)
            {
                SceneManager.LoadScene(4);
                points = 0;
            }
        }

        if (car.gameObject.tag.Equals("Large_Vehicle"))
        {
            TakeDamage(5);
            Sound_Manager.PlaySound("Scraping-Sound");
            if (currentHealth <= 0)
            {
                SceneManager.LoadScene(4);
                points = 0;
            }
        }

        if (car.gameObject.tag.Equals("Highway_Rails"))
        {
            TakeDamage(1);
            Sound_Manager.PlaySound("Scraping-Sound");
            if (currentHealth <= 0)
            {
                SceneManager.LoadScene(4);
                points = 0;
            }
        }

        if (car.gameObject.tag.Equals("Health"))
        {
            Player_Animation.SetTrigger(Heal_Anim);
            Sound_Manager.PlaySound("Heal-Sound");
            Heal(30);
            if (currentHealth > 100)
            {
                currentHealth = 100;
            }
 
        }

        if (car.gameObject.tag.Equals("Coin"))
        {
            Score_Player.instance.ChangeScore(coinScore);
            Sound_Manager.PlaySound("Coin-Sound");
            points += coinScore;
        }

        if (car.gameObject.tag.Equals("Boost"))
        {
            boost = true;
            Sound_Manager.PlaySound("Boost-Sound");
        }
    }

    void Heal(int heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
