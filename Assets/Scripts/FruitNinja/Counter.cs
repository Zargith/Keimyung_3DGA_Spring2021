using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{

    float t = 0;

    [SerializeField] GameObject a;
    [SerializeField] GameObject b;
    [SerializeField] GameObject c;
    [SerializeField] GameObject d;

    void Update()
    {
        t += Time.deltaTime;
        if (t < 1)
        {
            a.SetActive(true);
            b.SetActive(false);
            c.SetActive(false);
            d.SetActive(false);
        }
        if (t > 1 && t < 2)
        {
            a.SetActive(false);
            b.SetActive(true);
            c.SetActive(false);
            d.SetActive(false);
        }
        if (t > 2 && t < 3)
        {
            a.SetActive(false);
            b.SetActive(false);
            c.SetActive(true);
            d.SetActive(false);
        }
        if (t > 3 && t < 4)
        {
            a.SetActive(false);
            b.SetActive(false);
            c.SetActive(false);
            d.SetActive(true);
        } 
        if (t > 4)
        {
            a.SetActive(false);
            b.SetActive(false);
            c.SetActive(false);
            d.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
