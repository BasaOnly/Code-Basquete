using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShootBall : MonoBehaviour
{
    public static ShootBall instance;
    private float forca = 2.0f;

    private Vector2 startPos;
    private bool tiro = false;
    private bool mirando = false;
    [SerializeField]
    private GameObject dotsGO;
    private List<GameObject> caminho;
    [SerializeField]
    private Rigidbody2D myRBody;
    [SerializeField]
    private Collider2D myCollider;

    //Variaveis aux
    [SerializeField]
    private float valx, valy;

    //Tipo de jogadas
    private bool bateuAro = false;
    private bool bateuTabela = false;

    //Marcou Ponto
    public static bool fezPonto;
    [SerializeField]
    private bool liberaSky;

    [SerializeField]
    private Animator rym, sky, swish, table;

    public static int travaCesta;

    //Pontos Cesta
    public GameObject skyP, swishP, tableP, rymP;

    public LayerMask layer;


    void Start()
    {
        travaCesta = 1;
        rym = GameObject.FindWithTag("rym").GetComponent<Animator>();
        sky = GameObject.FindWithTag("skyHook").GetComponent<Animator>();
        swish = GameObject.FindWithTag("swish").GetComponent<Animator>();
        table = GameObject.FindWithTag("table").GetComponent<Animator>();

        liberaSky = false;
        fezPonto = false;
        dotsGO = GameObject.FindWithTag("dots");
        myRBody.isKinematic = true;
        myCollider.enabled = false;
        startPos = transform.position;
        //pega todos os filhos atravvés do dots.transform, joga eles numa lista e converte ele para gameObect
        //t é igual foreach representa todos gameobjects
        caminho = dotsGO.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        //faz todos os filhos de dotsGO sumir a imagem
        for (int x=0;x<caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = false;
        }
        
       

    }


    void MostraCaminho()
    {
        for(int x=0; x<caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = true;
        }
    }

    void EscondeCaminho()
    {
        for(int x=0; x < caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = false;
        }
    }

    Vector2 PegaForca(Vector3 mouse)
    {   //AUMENTA E DIMINUI SENSIBILIDADE DE TUDO AQUI
        return (new Vector2(startPos.x + valx, startPos.y + valy) - new Vector2(mouse.x, mouse.y)) * forca * 1.5f;
    }

    //FORMULA DE FISICA REAL
    Vector2 CaminhoPonto(Vector2 posInicial, Vector2 velInicial, float tempo)
    {
        return posInicial + velInicial * tempo + 0.5f * Physics2D.gravity * tempo * tempo;
    }


    void CalculaCaminho()
    {
        Vector2 vel = PegaForca(Input.mousePosition) *  Time.fixedDeltaTime / myRBody.mass;

        for(int x = 0; x < caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = true;
            float t = x / 20f;
            Vector3 point = CaminhoPonto(transform.position, vel, t);
            point.z = 1.0f;
            caminho[x].transform.position = point;
        }
    }

    void Mirando()
    {
        if (tiro == true)
        {
            return;
        }
        Vector3 mouseWP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWP.z = 0f;
        RaycastHit2D hit = Physics2D.Raycast(mouseWP, Vector2.zero, Mathf.Infinity, layer.value);
        if (hit.collider != null)
        {
            if (Input.GetMouseButton(0))
        {
            
                if (GameManager.instance.primeiraVez == 0)
                {
                    GameManager.instance.DesligaTutorial();
                }

                if (mirando == false)
                {
                    mirando = true;
                    startPos = Input.mousePosition;
                    CalculaCaminho();
                    MostraCaminho();

                }
                else
                {
                    CalculaCaminho();
                }
            }
            else if (mirando && tiro == false)
            {
                myRBody.isKinematic = false;
                myCollider.enabled = true;
                tiro = true;
                mirando = false;
                myRBody.AddForce(PegaForca(Input.mousePosition));
                EscondeCaminho();
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance.jogoExecutando)
        {
            if (!myRBody.gameObject.CompareTag("clone"))
            {
                Mirando();
            }
        }
        }

    void Update()
    {
        if (GameManager.instance.jogoExecutando)
        {
            if (!myRBody.isKinematic)
            {
                if (bateuTabela == false)
                {
                    //Rimshot
                    if (bateuAro && fezPonto && !liberaSky)
                    {
                        Instantiate(rymP, gameObject.transform.position, Quaternion.identity);
                        GameManager.instance.moedasIntSave += 2;
                        GameManager.instance.moedasUI.text = (GameManager.instance.moedasIntSave).ToString();
                        GameManager.instance.rimShoot = true;
                        rym.Play("Shoot");
                        fezPonto = false;
                        GameManager.instance.desafioNum1--;
                        GameManager.instance.DesafioDeFase(OndeEstou.instance.fase);
                    }
                    //SwishShot
                    else if (fezPonto && !liberaSky)
                    {
                        Instantiate(swishP, gameObject.transform.position, Quaternion.identity);
                        GameManager.instance.moedasIntSave += 5;    
                        GameManager.instance.moedasUI.text = (GameManager.instance.moedasIntSave).ToString();
                        GameManager.instance.swishShot = true;
                        swish.Play("Shoot");
                        fezPonto = false;
                        GameManager.instance.desafioNum4--;
                        GameManager.instance.DesafioDeFase(OndeEstou.instance.fase);

                    }
                }
                //SkyHook
                if (liberaSky && fezPonto)
                {
                    Instantiate(skyP, gameObject.transform.position, Quaternion.identity);
                    GameManager.instance.moedasIntSave += 3;                
                    GameManager.instance.moedasUI.text = (GameManager.instance.moedasIntSave).ToString();
                    GameManager.instance.skyHook = true;
                    sky.Play("Shoot");
                    fezPonto = false;
                    GameManager.instance.desafioNum3--;
                    GameManager.instance.DesafioDeFase(OndeEstou.instance.fase);
                }
                //Table
                if (bateuTabela && fezPonto)
                {
                    GameManager.instance.desafioNum2--;
                    GameManager.instance.DesafioDeFase(OndeEstou.instance.fase);
                    Instantiate(tableP, gameObject.transform.position, Quaternion.identity);
                    GameManager.instance.moedasIntSave += 1;
                    GameManager.instance.moedasUI.text = (GameManager.instance.moedasIntSave).ToString();
                    GameManager.instance.deTabela = true;
                    table.Play("Shoot");
                    fezPonto = false;
                   // GameManager.instance.desafioNum4--;
                    //GameManager.instance.DesafioDeFase(OndeEstou.instance.fase);

                }
            }
        }
       
    }

   
    void OnBecameInvisible()
    {    
            SegueBola.alvoInvisivel = true;    
    }

    void OnBecameVisible()
    {
        SegueBola.alvoInvisivel = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("aro"))
        {
            bateuAro = true;
        }
        if (col.gameObject.CompareTag("tabela"))
        {
            bateuTabela = true;
        }
      

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("sky"))
        {
            liberaSky = true;
        }
       
    }
}
