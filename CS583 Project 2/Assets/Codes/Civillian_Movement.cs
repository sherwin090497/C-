using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civillian_Movement : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
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
