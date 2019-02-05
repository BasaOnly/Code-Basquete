using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour
{

    [SerializeField]
    private Transform objE, objD, bola;
    private Vector3 volta;
    [SerializeField]

    private float t = 1;

   

    // Update is called once per frame
    void Update()
    {

        
        if (GameManager.instance.jogoExecutando)
        {

            if (bola == null && GameManager.instance.bolaEmCena && !GameManager.instance.win && !GameManager.instance.win)
            {
                //procura tag

                transform.position = new Vector3(Mathf.SmoothStep(objD.position.x, Camera.main.transform.position.x, t), this.transform.position.y, this.transform.position.z);
                bola = GameObject.FindGameObjectWithTag("bola").GetComponent<Transform>();

            }
            else if (GameManager.instance.bolaEmCena && !GameManager.instance.win && !GameManager.instance.win)
            {
                Vector3 posCam = transform.position;
                posCam.x = bola.position.x;
                posCam.x = Mathf.Clamp(posCam.x, objE.position.x, objD.position.x);
                transform.position = posCam;
            }

        }
        else
        {

            if (transform.position.x != objD.position.x && !GameManager.instance.jogoExecutando && !GameManager.instance.win && !GameManager.instance.win)
            {
                
                t -= 0.06f * Time.deltaTime;
                transform.position = new Vector3(Mathf.SmoothStep(objD.position.x, Camera.main.transform.position.x, t), this.transform.position.y, this.transform.position.z);
                volta = new Vector3(Mathf.SmoothStep(objD.position.x, Camera.main.transform.position.x, t), this.transform.position.y, this.transform.position.z);
            }
        }





        }
    }



