using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class DesafiosTxt
    {
        public int ondeEstou;
        public string desafio1, desafio2, desafio3, desafio4, desafio5, desafio6;
        public int desafio1Int = 0;
        public int desafio2Int = 0;
        public int desafio3Int = 0;
        public int desafio4Int = 0;
        public int desafio5Int = 0;
        public int desafio6Int = 0;
        public int numeroJogadas;
    }

    public List<DesafiosTxt> desafiosList;

    void ListaAdd()
    {
        foreach(DesafiosTxt desaf in desafiosList)
        {
            if (desaf.ondeEstou == OndeEstou.instance.fase)
            {   //coloca direto no inspector da unity a informação e aparece na tela

                desafio1.text = desaf.desafio1;
                desafio2.text = desaf.desafio2;
                desafio3.text = desaf.desafio3;
                desafio4.text = desaf.desafio4;
                desafio5.text = desaf.desafio5;
                desafio6.text = desaf.desafio6;

                desafioNum1 = desaf.desafio1Int;
                desafioNum2 = desaf.desafio2Int;
                desafioNum3 = desaf.desafio3Int;
                //Detalhe Importante
                desafioNum4 = desaf.desafio4Int;
                desafioNum5 = desaf.desafio5Int;
                desafioNum6 = desaf.desafio6Int;

                numJogadas = desaf.numeroJogadas;
                break;
            }
        }
    }

    public static GameManager instance;

    public int desafioNum1, desafioNum2, desafioNum3, desafioNum4, desafioNum5, desafioNum6;
    public Text desafio1, desafio2, desafio3, desafio4, desafio5, desafio6;

    public bool bolaEmCena;
    public int numJogadas;
    //public GameObject bolaPrefab;
    public GameObject[] bolaPrefab;
    [SerializeField]
    private Transform posDireta, posEsquerda, posCima, posBaixo;
    public bool jogoExecutando = true, win = false, lose = false;

    //Mao Bolinhas Tutorial
    public GameObject mao, bolinhas, seta;
    public int primeiraVez;

    //Identifica Ponto
    public int pontos = 0;

    //Tipos de pontos
    public bool rimShoot = false, swishShot = false, skyHook = false, deTabela = false;

    //Contador
    [SerializeField]
    private Animator contador;

    //Save Moeda
    public int moedasIntSave;
    public Text moedasUI;

    public Text numBolas;

    public bool adsUmaVez = false;

    public int perdeu = 0;

    void Awake()
    {
        if (PlayerPrefs.HasKey("PrimeiraVez") == false)
        {
            PlayerPrefs.SetInt("PrimeiraVez", 0);
        }
        else
        {
            primeiraVez = PlayerPrefs.GetInt("PrimeiraVez");
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
  
        }
        else
        {
            Destroy(gameObject);
        }

        moedasUI = GameObject.FindWithTag("numMoedas").GetComponent<Text>();
        moedasIntSave = ScoreManager.instance.LoadDados();
        moedasUI.text = moedasIntSave.ToString();
        numBolas = GameObject.FindWithTag("numBolas").GetComponent<Text>();
        numBolas.text = GameManager.instance.numJogadas.ToString();

        desafio1 = GameObject.FindWithTag("d1").GetComponent<Text>();
        desafio2 = GameObject.FindWithTag("d2").GetComponent<Text>();
        desafio3 = GameObject.FindWithTag("d3").GetComponent<Text>();
        desafio4 = GameObject.FindWithTag("d4").GetComponent<Text>();
        desafio5 = GameObject.FindWithTag("d5").GetComponent<Text>();
        desafio6 = GameObject.FindWithTag("d6").GetComponent<Text>();

        SceneManager.sceneLoaded += Carrega;
    }
   

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if (SceneManager.GetActiveScene().buildIndex != 16 && SceneManager.GetActiveScene().buildIndex != 17 && SceneManager.GetActiveScene().buildIndex != 18)
        {
            StartGame();
            bolaEmCena = true;
           
            posDireta = GameObject.FindWithTag("posDireita").GetComponent<Transform>();
            posEsquerda = GameObject.FindWithTag("posEsquerda").GetComponent<Transform>();
            posCima = GameObject.FindWithTag("posCima").GetComponent<Transform>();
            posBaixo = GameObject.FindWithTag("posBaixo").GetComponent<Transform>();
            //Contador
            contador = GameObject.FindWithTag("contador").GetComponent<Animator>();
            contador.Play("Contador");
            //MOEDA
            moedasUI = GameObject.FindWithTag("numMoedas").GetComponent<Text>();
            moedasIntSave = ScoreManager.instance.LoadDados();
            moedasUI.text = moedasIntSave.ToString();

            numBolas = GameObject.FindWithTag("numBolas").GetComponent<Text>();
            numBolas.text = GameManager.instance.numJogadas.ToString();

            desafio1 = GameObject.FindWithTag("d1").GetComponent<Text>();
            desafio2 = GameObject.FindWithTag("d2").GetComponent<Text>();
            desafio3 = GameObject.FindWithTag("d3").GetComponent<Text>();
            desafio4 = GameObject.FindWithTag("d4").GetComponent<Text>();
            desafio5 = GameObject.FindWithTag("d5").GetComponent<Text>();
            desafio6 = GameObject.FindWithTag("d6").GetComponent<Text>();

            ListaAdd();


            if (primeiraVez == 0 || primeiraVez == 1)
            {
                PegaSpritesTutorial();
                if (primeiraVez == 1)
                {
                    Matador(mao.gameObject, bolinhas.gameObject, seta.gameObject);
                }
            }
           

        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
        contador.Play("Contador");
        StartGame();
        ListaAdd();
        bolaEmCena = true;
        numBolas.text = GameManager.instance.numJogadas.ToString();


    }


    void Update()
    {
        //Perde
        if (numJogadas <= 0 &&  (desafioNum1 > 0 || desafioNum2 > 0 || desafioNum3 >0 || desafioNum4 > 0 || desafioNum5 > 0 || desafioNum6 > 0))
        {
            YouLose();
        }
        //Vence
        else if(numJogadas > 0 && desafioNum1 <= 0 && desafioNum2 <= 0 && desafioNum3 <= 0 && desafioNum4 <= 0 && desafioNum5 <= 0 && desafioNum6 <= 0)
        {
            YouWin();
        }
    }

    public void NascBolas()
    {
        if (!win)
        {
          
            //Nasce aleatorio
            Instantiate(bolaPrefab[LojaManager.instance.aux], new Vector2(Random.Range(posEsquerda.position.x, posDireta.position.x), Random.Range(posCima.position.y, posBaixo.position.y)), Quaternion.identity);
            bolaEmCena = true;
        }
    }

    public void DesligaTutorial()
    {
        Matador(mao.gameObject, bolinhas.gameObject, seta.gameObject);
        PlayerPrefs.SetInt("PrimeiraVez", 1);
    }

    void Matador(GameObject obj, GameObject obj2, GameObject obj3)
    {
        Destroy(obj);
        Destroy(obj2);
        Destroy(obj3);
    }

    void PegaSpritesTutorial()
    {
        mao = GameObject.FindWithTag("mao");
        bolinhas = GameObject.FindWithTag("bolinhas");
        seta = GameObject.FindWithTag("setaT");
    }

    void StartGame()
    {
        adsUmaVez = false;
        lose = false;
        win = false;
        pontos = 0;
        jogoExecutando = false;
        //Load Moedas
      //  moedasIntSave = ScoreManager.instance.LoadDados();
       // UIManager.instance.moedasUI.text = moedasIntSave.ToString("c");
    }

    public void DesafioDeFase(int fase)
    {
        if (OndeEstou.instance.fase == fase)
        {
            if (desafioNum1 == 0 && desafio1.text.Length != 0)
            {
                UIManager.instance.desafio1T.isOn = true;    
            }

            if (desafioNum2 == 0 && desafio2.text.Length != 0)
            {
                UIManager.instance.desafio2T.isOn = true;
            }

            if (desafioNum3 == 0 && desafio3.text.Length != 0)
            {
                UIManager.instance.desafio3T.isOn = true;
            }

            if (desafioNum4 == 0 && desafio4.text.Length != 0) 
            {
                UIManager.instance.desafio4T.isOn = true;
            }

            if (desafioNum5 == 0 && desafio5.text.Length != 0)
            {
                UIManager.instance.desafio5T.isOn = true;
            }

            if (desafioNum6 == 0 && desafio6.text.Length != 0)
            {
                UIManager.instance.desafio6T.isOn = true;
            }
        }
    }

    void YouWin()
    {
        if (jogoExecutando)
        {
            //Libera proxima fase se ganhar
           
            PlayerPrefs.SetInt("Level" + (OndeEstou.instance.fase+1), 1);

            win = true;
            jogoExecutando = false;
            UIManager.instance.animWin.Play("Win");
            //Save Moedas se ganhar
            ScoreManager.instance.SalvarDados(moedasIntSave);
            //Libera as fases
           
        }
    }

    void YouLose()
    {
        if (jogoExecutando)
        {
            UIManager.instance.animLose.Play("Win");
            lose = true;
            jogoExecutando = false;

            if (adsUmaVez == false)
            {
                Debug.Log("EntrouUmaVez");
               
                AppLoving.instance.ShowAd();
                adsUmaVez = true;
            }
        }
    }
}
