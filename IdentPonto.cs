using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentPonto : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioS;
    [SerializeField]
    private AudioClip clip;
 

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("bola") || col.gameObject.CompareTag("clone") || col.gameObject.CompareTag("boliche"))
        {
            if (ShootBall.travaCesta == 1) {
                ShootBall.travaCesta--;
                ShootBall.fezPonto = true;
                TocaAudio.TocadorAudio(audioS, clip);
                GameManager.instance.desafioNum5--;
            }
           

        }
    }
}
