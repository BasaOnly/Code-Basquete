using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SomContador : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip[] clip;

    void TocaInicio()
    {
        if (!audioS.isPlaying)
        {
            audioS.clip = clip[0];
            audioS.Play();
        }
    }
    void TocaFim()
    {
        if (!audioS.isPlaying)
        {
            audioS.clip = clip[1];
            audioS.Play();
        }
    }

    void EventoContagem()
    {
      
        gameObject.GetComponent<Text>().enabled = false;
        GameManager.instance.jogoExecutando = true;
        if (AppLoving.instance.mostrou)
        {
            AppLovin.HideAd();
        }
     }
}
