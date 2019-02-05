using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMoedaSprit : MonoBehaviour
{
    private float vel = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * vel * Time.deltaTime);     
    }

    void Destroi()
    {
        Destroy(gameObject);
    }
}
