using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoltaBola : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D bolaAtual, bolaPrefab;
    [SerializeField]
    private int trava = 2;
    [SerializeField]
    private bool libera = false;
    [SerializeField]
    private Vector3 startPos;


    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !bolaAtual.isKinematic && trava > 0)
        {
         
            libera = true;
            startPos = transform.position;
            Instantiate(bolaPrefab, new Vector3(startPos.x, startPos.y - 0.4f, startPos.z), Quaternion.identity);
            trava--;
        }
    }

}
