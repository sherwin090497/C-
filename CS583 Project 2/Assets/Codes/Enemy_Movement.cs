using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float carAcceleration;
    public float maxSpeed;
    public float curSpeed;
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, curSpeed * Time.deltaTime);
        curSpeed += carAcceleration;
        if (curSpeed > maxSpeed)
            curSpeed = maxSpeed;
        
    }

    private void OnCollisionEnter2D(Collision2D car)
    {
        if (car.gameObject.tag == "Health")
        {
            Physics2D.IgnoreCollision(car.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (car.gameObject.tag == "Coin")
        {
            Physics2D.IgnoreCollision(car.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (car.gameObject.tag == "Boost")
        {
            Physics2D.IgnoreCollision(car.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
