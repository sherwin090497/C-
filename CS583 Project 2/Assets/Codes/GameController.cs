using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public GameObject[] HighwayStages;
    public GameObject Taxi;
    public GameObject Truck;
    public GameObject FastCar;
    public GameObject miniTruck;
    public GameObject miniVan;
    public GameObject semiTruck;
    public GameObject Police;
    public GameObject PowerUP_Health;
    public GameObject PowerUP_Boost;
    public GameObject Coin;

    private Camera mainCamera;
    private Vector2 screenBounds;
    public float Filler;
    public float PlayerSpeed;
    public float timer = 1f;
    public float healthTimer = 1f;
    public float coinTimer = 1f;
    public float boostTimer = 1f;
    public float levelTimer;
    public Transform Target;
    float recurrence;
    float health_recurrence;
    float coin_recurrence;
    float boost_recurrence;

    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in HighwayStages)
        {
            loadHighway(obj);
        }
        recurrence = timer;
        health_recurrence = healthTimer;
        coin_recurrence = coinTimer;
        boost_recurrence = boostTimer;
    }

    void loadHighway(GameObject obj)
    {
        float HighwayHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y - Filler;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.y * 2 / HighwayHeight);
        GameObject temp = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(temp) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x, HighwayHeight * i, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(temp);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void newHighway(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            GameObject CurrentRoad = children[1].gameObject;
            GameObject PreviousRoad = children[children.Length - 1].gameObject;
            float RoadHeight = PreviousRoad.GetComponent<SpriteRenderer>().bounds.extents.x - Filler;
            if (transform.position.y + screenBounds.y > PreviousRoad.transform.position.y + RoadHeight)
            {
                CurrentRoad.transform.SetAsLastSibling();
                CurrentRoad.transform.position = new Vector3(PreviousRoad.transform.position.x, PreviousRoad.transform.position.y + RoadHeight * 2, PreviousRoad.transform.position.z);
            }
            else if (transform.position.y - screenBounds.y < CurrentRoad.transform.position.y - RoadHeight)
            {
                PreviousRoad.transform.SetAsFirstSibling();
                PreviousRoad.transform.position = new Vector3(CurrentRoad.transform.position.x, CurrentRoad.transform.position.y - RoadHeight * 2, CurrentRoad.transform.position.z);
            }
        }
    }

    void Update()
    {
        
        Vector3 velocity = Vector3.zero;
        Vector3 desiredPosition = transform.position + new Vector3(0, PlayerSpeed, 0);
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0.3f);
        transform.position = smoothPosition;
        recurrence -= Time.deltaTime;
        health_recurrence -= Time.deltaTime;
        coin_recurrence -= Time.deltaTime;
        boost_recurrence -= Time.deltaTime;

        if (recurrence <= 0)
        {
            Vector3 civillianCar = new Vector3(Random.Range(-1.8f, 1.8f), transform.position.y + 6, transform.position.x);
            Vector3 civillianFastCar = new Vector3(Random.Range(-1.8f, 1.8f), transform.position.y + 6, transform.position.x);
            Vector3 civillianTruck = new Vector3(Random.Range(-1.8f, 1.8f), transform.position.y + 7, transform.position.x);
            Vector3 civillianMiniTruck = new Vector3(Random.Range(-1.9f, 1.9f), transform.position.y + 80, transform.position.x);
            Vector3 civillianMiniVan = new Vector3(Random.Range(-1.9f, 1.9f), transform.position.y + 110, transform.position.x);
            Vector3 civillianSemiTruck = new Vector3(Random.Range(-1.9f, 1.9f), transform.position.y + 160, transform.position.x);
            Vector3 policeCar = new Vector3(Random.Range(-1.8f, 1.8f), transform.position.y - 6, transform.position.x);
            Instantiate(Taxi, civillianCar, transform.rotation);
            Instantiate(FastCar, civillianFastCar, transform.rotation);
            Instantiate(Truck, civillianTruck, transform.rotation);
            Instantiate(miniTruck, civillianMiniTruck, transform.rotation);
            Instantiate(miniVan, civillianMiniVan, transform.rotation);
            Instantiate(semiTruck, civillianSemiTruck, transform.rotation);
            Instantiate(Police, policeCar, transform.rotation);
            recurrence = timer;
        }

        if (health_recurrence <= 0)
        {
            Vector3 Car_Heal = new Vector3(Random.Range(-1.8f, 1.8f), transform.position.y + 7, transform.position.x);
            Instantiate(PowerUP_Health, Car_Heal, transform.rotation);
            health_recurrence = healthTimer;
        }

        if (coin_recurrence <= 0)
        {
            float k = Random.Range(-1.8f, 1.8f);
            for (float j = 1f; j <= 3f; j = j + .5f)
            {
                Vector3 coinStrip = new Vector3(k, transform.position.y + 5 + j, transform.position.x);
                Instantiate(Coin, coinStrip, transform.rotation);
            }
            coin_recurrence = coinTimer;
        }

        if (boost_recurrence <= 0)
        {
            Vector3 nitroBoost = new Vector3(Random.Range(-1.9f, 1.9f), transform.position.y + 7, transform.position.x);
            Instantiate(PowerUP_Boost, nitroBoost, transform.rotation);
            boost_recurrence = boostTimer;
        }
        
        levelTimer += Time.deltaTime;
        if (levelTimer >= 120)
        {
            SceneManager.LoadScene(5);
        }
    }

    void LateUpdate()
    {
        foreach (GameObject obj in HighwayStages)
        {
            newHighway(obj);
        }
    }
}
