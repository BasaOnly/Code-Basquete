using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomGeral : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioS;
    [SerializeField]
    private AudioClip clip;

   

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("bola") || col.gameObject.CompareTag("clone") || col.gameObject.CompareTag("boliche"))
        {
            TocaAudio.TocadorAudio(audioS, clip);
        }
    }
}
