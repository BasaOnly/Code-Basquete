using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SegueBola : MonoBehaviour
{
    public Image seta;
    public Transform alvo;
    public static bool alvoInvisivel = false;

    // Start is called before the first frame update
    void Start()
    {
        alvo = GameObject.FindWithTag("bola").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    
        if(GameManager.instance.bolaEmCena == true && alvo == null)
        {
            alvo = GameObject.FindWithTag("bola").GetComponent<Transform>();
        }

        if (alvoInvisivel && GameManager.instance.jogoExecutando)
        {
            Segue();
            VisualizaSeta(alvoInvisivel);
        }
        else
        {
            VisualizaSeta(alvoInvisivel);
        }
     
    }

    void Segue()
    {
        if (!alvo)
        {
            return;
        }

        Vector2 aux;
        //aux pega a pos da seta
        aux = seta.rectTransform.position;
        //aux = seta = posicao da bola
        aux.x = alvo.position.x;
        //posicao da seta original = posicao da aux q é = posicao da bola
        seta.rectTransform.position = aux;
        // Seria mais simples assim, mas a unity nao deixa. Por isso o codigo a cima.
        // seta.rectTransform.position.x = alvo.position.x;

    }

    void VisualizaSeta(bool condicao)
    {   
            seta.enabled = condicao;
        
       

        if (!GameManager.instance.jogoExecutando)
        {
            seta.enabled = false;
        }
    }
}
