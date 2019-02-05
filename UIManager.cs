using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Toggle desafio1T, desafio2T, desafio3T, desafio4T, desafio5T, desafio6T;

    public Button store1, store2, store3;



  
    //Teste Mata Bola
    public GameObject killBall1, killBall2;
    public AudioSource morteBola;
    //Win Lose
    public GameObject panelWin, panelLose;
    public Animator animWin, animLose;

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

        SceneManager.sceneLoaded += Carrega;

        if (SceneManager.GetActiveScene().buildIndex != 16 && SceneManager.GetActiveScene().buildIndex != 17 && SceneManager.GetActiveScene().buildIndex != 18)
        {
           
            panelLose = GameObject.FindWithTag("losePanel");
            panelWin = GameObject.FindWithTag("winPainel");
            animWin = panelWin.GetComponent<Animator>();
            animLose = panelLose.GetComponent<Animator>();
            


            killBall1 = GameObject.FindWithTag("kb1");
            killBall2 = GameObject.FindWithTag("kb2");

            desafio1T = GameObject.FindWithTag("togg1").GetComponent<Toggle>();
            desafio2T = GameObject.FindWithTag("togg2").GetComponent<Toggle>();
            desafio3T = GameObject.FindWithTag("togg3").GetComponent<Toggle>();
            desafio4T = GameObject.FindWithTag("togg4").GetComponent<Toggle>();
            desafio5T = GameObject.FindWithTag("togg5").GetComponent<Toggle>();
            desafio6T = GameObject.FindWithTag("togg6").GetComponent<Toggle>();

            store1 = GameObject.FindWithTag("store1").GetComponent<Button>();
            store2 = GameObject.FindWithTag("store2").GetComponent<Button>();
            store3 = GameObject.FindWithTag("store3").GetComponent<Button>();

            store1.onClick.AddListener(goStore);
            store2.onClick.AddListener(goStore);
            store3.onClick.AddListener(goStore);




        }
      
       
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if (SceneManager.GetActiveScene().buildIndex != 16 && SceneManager.GetActiveScene().buildIndex != 17 && SceneManager.GetActiveScene().buildIndex != 18)
        {

           
          

            desafio1T = GameObject.FindWithTag("togg1").GetComponent<Toggle>();
            desafio2T = GameObject.FindWithTag("togg2").GetComponent<Toggle>();
            desafio3T = GameObject.FindWithTag("togg3").GetComponent<Toggle>();
            desafio4T = GameObject.FindWithTag("togg4").GetComponent<Toggle>();
            desafio5T = GameObject.FindWithTag("togg5").GetComponent<Toggle>();
            desafio6T = GameObject.FindWithTag("togg6").GetComponent<Toggle>();
       
          

            killBall1 = GameObject.FindWithTag("kb1");
            killBall2 = GameObject.FindWithTag("kb2");

            panelLose = GameObject.FindWithTag("losePanel");
            panelWin = GameObject.FindWithTag("winPainel");
            animWin = panelWin.GetComponent<Animator>();
            animLose = panelLose.GetComponent<Animator>();

            store1 = GameObject.FindWithTag("store1").GetComponent<Button>();
            store2 = GameObject.FindWithTag("store2").GetComponent<Button>();
            store3 = GameObject.FindWithTag("store3").GetComponent<Button>();

            store1.onClick.AddListener(goStore);
            store2.onClick.AddListener(goStore);
            store3.onClick.AddListener(goStore);

        }
    }

    void Start()
    {
        
    }

    public void goStore()
    {
        if (AppLoving.instance.mostrou)
        {
            AppLovin.HideAd();
        }
        SceneManager.LoadScene("Store");
    }

}
