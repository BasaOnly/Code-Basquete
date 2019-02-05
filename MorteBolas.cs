using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorteBolas : MonoBehaviour
{


    [SerializeField]
    private float vidaBola = 1f;
    [SerializeField]
    private Color cor;
    [SerializeField]
    private Renderer bolaRender;
    [SerializeField]
    private bool tocouChao = false;

   

    
    void Start()
    {
        bolaRender = gameObject.GetComponent<Renderer>();
       //PEGA A COR DA BOLA
       cor = bolaRender.material.GetColor("_Color");
       
    }

    void Update()
    {
        if (tocouChao)
        {
            MataBola();
        }

        if (GameManager.instance.jogoExecutando)
        {

            if(UIManager.instance.killBall1.transform.position.x > this.transform.position.x || UIManager.instance.killBall2.transform.position.x < this.transform.position.x)
            {
                    UIManager.instance.morteBola.Play();

                    GameManager.instance.bolaEmCena = false;
                    Destroy(this.gameObject);


                    if (gameObject.CompareTag("bola"))
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


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ground"))
        {
          
            tocouChao = true;
        }
    }

    void MataBola()
    {
        if (vidaBola > 0)
        {
            vidaBola -= Time.deltaTime;
            //EFEITO QUE FAZ A BOLA INDO DESAPARECENDO
            bolaRender.material.SetColor("_Color", new Color(cor.r, cor.g, cor.b, vidaBola));
        }
        if (vidaBola <= 0)
        {
            GameManager.instance.bolaEmCena = false;
            Destroy(gameObject);
          

            if (gameObject.CompareTag("bola"))
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
