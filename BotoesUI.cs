using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotoesUI : MonoBehaviour
{
    public Animator tutorial, panelInfo;
    public Animator pause, btnConfig, panelConfig;
    public Button pauseBtn;
    public int configInt, configInt2; 
    
    void Start()
    {
        configInt = 1;
        configInt2 = 1;
    }

    public void AbreTutorial()
    {
        if (!GameManager.instance.win && !GameManager.instance.lose)
        {   if (pauseBtn.interactable == true)
            {
                Time.timeScale = 0;
                tutorial.Play("Panel");
            }
        }

    }

    public void FechaTutorial()
    {
      
        Time.timeScale = 1;
        tutorial.Play("PanelR");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(OndeEstou.instance.fase);
       
    }

    public void Pause()
    {
        if (!GameManager.instance.win && !GameManager.instance.lose)
        {
            pauseBtn.interactable = false;
            Time.timeScale = 0;
            pause.Play("Win");
            LojaManager.instance.sobe.interactable = false;
            LojaManager.instance.desce.interactable = false;
            LojaManager.instance.menuImg.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }

    public void DesPause()
    {
        pauseBtn.interactable = true;
        Time.timeScale = 1;
        pause.Play("WinR");
        LojaManager.instance.sobe.interactable = true;
        LojaManager.instance.desce.interactable = true;
        LojaManager.instance.menuImg.color = new Color(1, 1, 1, 1);
    }

    public void Fases()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu_Fases");
        if (AppLoving.instance.mostrou)
        {
            AppLovin.HideAd();
        }
    }
    
    public void ProximaFase()
    {
        OndeEstou.instance.fase++;
        SceneManager.LoadScene(OndeEstou.instance.fase);
      
    }

    public void BtnConfig()
    {
        if (configInt == 1)
        {
            configInt--;
            btnConfig.Play("BtnConfig");
            panelConfig.Play("PanelConfig");
        }
        else
        {
            panelConfig.Play("PanelConfigR");
            btnConfig.Play("BtnConfigR");
            configInt++;
        }
    }

    public void vaiStore()
    {
        SceneManager.LoadScene("Store");
    }

    public void voltaInicio()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu_Inicial");
    }

    public void FreeStyle()
    {
        SceneManager.LoadScene("FreeStyle");
    }

    public void botaoInfo()
    {
        if (configInt2 == 1)
        {
            configInt2--;
            panelInfo.Play("Info");
        
        }
        else
        {
            panelInfo.Play("InfoR");
            configInt2++;
        }
    }

}
