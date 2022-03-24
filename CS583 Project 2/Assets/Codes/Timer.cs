using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject display;
    public int secs = 120;
    public bool decrease = false;

    void Start()
    {
        display.GetComponent<Text>().text = " " + secs;
    }

    void Update()
    {
        if (decrease == false && secs > 0)
        {
            StartCoroutine(timer());
        }
    }


    IEnumerator timer()
    {
        decrease = true;
        yield return new WaitForSeconds(1);
        secs = secs - 1;
        display.GetComponent<Text>().text = " " + secs;
        decrease = false;

    }
}
