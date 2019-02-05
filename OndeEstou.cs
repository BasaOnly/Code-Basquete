using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour
{
    public int fase = -1;
    public static OndeEstou instance;
    public GameObject gameManager, uiManager;

   
    

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().buildIndex == 16 || SceneManager.GetActiveScene().buildIndex == 17)
        {
            gameManager = GameObject.FindWithTag("gm");
            uiManager = GameObject.FindWithTag("uim");
            Destroy(gameManager);
            Destroy(uiManager);

           
        }

        SceneManager.sceneLoaded += VerificaFase;
    }

    void VerificaFase(Scene cena, LoadSceneMode modo)
    {

        if (SceneManager.GetActiveScene().buildIndex == 16 || SceneManager.GetActiveScene().buildIndex == 17)
        {
            gameManager = GameObject.FindWithTag("gm");
            uiManager = GameObject.FindWithTag("uim");
            Destroy(gameManager);
            Destroy(uiManager);
        }
        fase = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {   
       if(SceneManager.GetActiveScene().buildIndex == 18 )
        {
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 16)
        {
            if (Input.GetKey("escape"))
            {
                SceneManager.LoadScene("Menu_Inicial");
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 17)
        {
            if (Input.GetKey("escape"))
            {
                SceneManager.LoadScene("Menu_Inicial");
            }
        }
    }

   
}
