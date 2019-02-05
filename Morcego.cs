using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morcego : MonoBehaviour
{
    public SliderJoint2D morcego;
    public JointMotor2D aux;
    private Transform trans;
    private int trava;



    void Start()
    {
        trans = GetComponent<Transform>();
        morcego = GetComponent<SliderJoint2D>();
        aux = morcego.motor;
        trava = 0;
    }


    void Update()
    {

        if (morcego.limitState == JointLimitState2D.UpperLimit)
        {  
            //Move
            aux.motorSpeed = -2;
            morcego.motor = aux;
            //Flip
            if (trava == 0)
            {
                trava++;
                trans.localScale = new Vector3(-trans.localScale.x, trans.localScale.y, trans.localScale.z);
            }
        }

        if (morcego.limitState == JointLimitState2D.LowerLimit)
        {
            
            //Move
            aux.motorSpeed = 2;
            morcego.motor = aux;
            //Flip
            if (trava == 1)
            {
                trava--;
                trans.localScale = new Vector3(-trans.localScale.x, trans.localScale.y, trans.localScale.z);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("bola"))
        {
            Destroy(col.gameObject);
            UIManager.instance.morteBola.Play();
            GameManager.instance.bolaEmCena = false;



            if (col.gameObject.CompareTag("bola"))
            {
                GameManager.instance.numJogadas--;
                GameManager.instance.numBolas.text = GameManager.instance.numJogadas.ToString();

                if (GameManager.instance != null && GameManager.instance.numJogadas > 0)
                {
                    GameManager.instance.NascBolas();
                }
            }
        }

       
    }
    }
