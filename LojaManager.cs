using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LojaManager : MonoBehaviour
{
    public static LojaManager instance;

    //LOJA
    public List<int> bolas;
    public Image menuImg;
    public Sprite[] imagemSp;
    public int aux = 0;
    public Text moedasStore;

    public Button[] compraBtn;

    public Button sobe, desce;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        //LOJA

        if (SceneManager.GetActiveScene().buildIndex != 16 && SceneManager.GetActiveScene().buildIndex != 18)
        {
            if (SceneManager.GetActiveScene().buildIndex != 16 && SceneManager.GetActiveScene().buildIndex != 17 && SceneManager.GetActiveScene().buildIndex != 18)
            {
                menuImg = GameObject.FindWithTag("imgBolaLoja").GetComponent<Image>();

            }
            bolas = new List<int>();
            bolas.Add(0);

            if (!PlayerPrefs.HasKey("Bola0"))
            {
                PlayerPrefs.SetInt("Bola0", bolas[0]);
            }

            for (int i = 1; i < PlayerPrefs.GetInt("list_Count"); i++)
            {
                bolas.Add(PlayerPrefs.GetInt("Bola" + i));
            }
        }
  
        SceneManager.sceneLoaded += Carrega;
    }




        void Carrega(Scene cena, LoadSceneMode modo)
        {
            if (SceneManager.GetActiveScene().buildIndex != 16 && SceneManager.GetActiveScene().buildIndex != 18)
            {

           
            bolas = new List<int>();
                bolas.Add(0);
                if (!PlayerPrefs.HasKey("Bola0"))
                {
                    PlayerPrefs.SetInt("Bola0", bolas[0]);
                }

                for (int i = 1; i < PlayerPrefs.GetInt("list_Count"); i++)
                {
                    bolas.Add(PlayerPrefs.GetInt("Bola" + i));
                }

            if (SceneManager.GetActiveScene().buildIndex != 16 && SceneManager.GetActiveScene().buildIndex != 17 && SceneManager.GetActiveScene().buildIndex != 18)
            {
                menuImg = GameObject.FindWithTag("imgBolaLoja").GetComponent<Image>();
                menuImg.sprite = imagemSp[PlayerPrefs.GetInt("Bola" + bolas[0])];
                sobe = GameObject.FindWithTag("btnCima").GetComponent<Button>();
                desce = GameObject.FindWithTag("btnBaixo").GetComponent<Button>();

                sobe.onClick.AddListener(CimaBolas);
                desce.onClick.AddListener(BaixoBolas);
                aux = 0;

                
            }
           
            AtualizaBtnBola();
            }

        if (SceneManager.GetActiveScene().name == "Store")
        {
            moedasStore = GameObject.FindWithTag("storeCoin").GetComponent<Text>();
            moedasStore.text = ScoreManager.instance.LoadDados().ToString();
        }
    }

    
        void Start()
        {
        //LOJA
        if (SceneManager.GetActiveScene().buildIndex != 16 && SceneManager.GetActiveScene().buildIndex != 17 && SceneManager.GetActiveScene().buildIndex != 18)
        {
            menuImg.sprite = imagemSp[PlayerPrefs.GetInt("Bola" + bolas[0])];

        }
        }


    public void Compra(int id)
    {
        if (id == 1)
        {
            if (ScoreManager.instance.LoadDados() >= 75)
            {
                ChamaCompra(1);
                ScoreManager.instance.PerdeMoedas(75);
                moedasStore.text = ScoreManager.instance.LoadDados().ToString();
            }
            else
            {
                print("Sem money");
            }
        }
        else if (id == 2)
        {
            if (ScoreManager.instance.LoadDados() >= 100)
            {
                ChamaCompra(2);
                ScoreManager.instance.PerdeMoedas(100);
                moedasStore.text = ScoreManager.instance.LoadDados().ToString();

            }
            else
            {
                print("Sem money");
            }
        }


    }

    void ChamaCompra(int id) { 
            bolas.Add(id);
            PlayerPrefs.SetInt("list_Count", bolas.Count);
            PlayerPrefs.SetInt("Bola" + id, id);
            //Se ja comprou fica falso
            compraBtn[id - 1].interactable = false;
            
            //Abre outro botao
            if(id != 2)
        {
            compraBtn[id].interactable = true;
        }

        if (bolas.Contains(id))
        {
            compraBtn[id - 1].GetComponentInChildren<Text>().text = "Comprado";
            compraBtn[id - 1].GetComponentInChildren<Text>().color = new Color (0,1,0,1);

        }


    }
   


        void AjustaBolasBtn(int x)
    {
        compraBtn[x].interactable = false;
        compraBtn[x].GetComponentInChildren<Text>().text = "Comprado";
        compraBtn[x].GetComponentInChildren<Text>().color = new Color(0,1,0,1);

    }

    void BaixoBolas()
    {
        if (aux < bolas.Count - 1)
        {
            aux++;
            menuImg.sprite = imagemSp[PlayerPrefs.GetInt("Bola" + aux)];
        }
    }

    void CimaBolas()
    {
        if (aux >=  1)
        {
            aux--;
            menuImg.sprite = imagemSp[PlayerPrefs.GetInt("Bola" + aux)];
        }
    }

    void AtualizaBtnBola()
    {
        if (OndeEstou.instance.fase == 17)
        {
            compraBtn = new Button[2];

            compraBtn[0] = GameObject.FindWithTag("bntCompraUm").GetComponent<Button>();
            compraBtn[1] = GameObject.FindWithTag("btnCompraDois").GetComponent<Button>();

            compraBtn[0].onClick.AddListener(() => Compra(1));
            compraBtn[1].onClick.AddListener(() => Compra(2));

            if (bolas.Contains(1))
            {
                AjustaBolasBtn(0);
                if (!bolas.Contains(2))
                {
                    compraBtn[1].interactable = true;
                }
            }

            if (bolas.Contains(2))
            {
                AjustaBolasBtn(1);
            }

        }
    }

    void Update()
    {
     
        if (SceneManager.GetActiveScene().buildIndex != 16 && SceneManager.GetActiveScene().buildIndex != 17 && SceneManager.GetActiveScene().buildIndex != 18){
            if (GameManager.instance.win || GameManager.instance.lose)
            {
                sobe.interactable = false;
                desce.interactable = false;
                menuImg.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            }
            else if (GameManager.instance.jogoExecutando)
            {
                sobe.interactable = true;
                desce.interactable = true;
                menuImg.color = new Color(1, 1, 1, 1);
            }
         
        }
    }

}

