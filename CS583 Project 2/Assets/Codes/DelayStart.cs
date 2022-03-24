using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayStart : MonoBehaviour
{
    public GameObject countdown;
    public GameObject dim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartDelay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartDelay()
    {
        Time.timeScale = 0;

        float pauseTime = Time.realtimeSinceStartup + 3.1f;

        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;
        countdown.gameObject.SetActive(false);
        dim.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
