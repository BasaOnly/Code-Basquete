using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaTripla : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D bola1, bola2, bolaAtual, bolaPrefab;
    [SerializeField]
    private int trava = 0;
    [SerializeField]
    private bool libera = false;
    [SerializeField]
    private Vector3 startPos;


    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !bolaAtual.isKinematic  && trava == 0)
        {
          
            libera = true;
            startPos = transform.position;
            bola1 = Instantiate(bolaPrefab, new Vector3(startPos.x, startPos.y + 0.8f, startPos.z), Quaternion.identity);
            bola2 = Instantiate(bolaPrefab, new Vector3(startPos.x, startPos.y + 0.4f, startPos.z), Quaternion.identity);

            trava = 1;
        }
    }
    
    void FixedUpdate()
    {
        if (libera)
        {
            bola1.velocity = bolaAtual.velocity;
            bola2.velocity = bolaAtual.velocity;

            bola1.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
            bola2.AddForce(Vector2.up * 4, ForceMode2D.Impulse);

            libera = false;
        }
    }
}
