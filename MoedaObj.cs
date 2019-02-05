using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoedaObj : MonoBehaviour
{
    public GameObject efeitoMoeda;
    public AudioSource audioS;
    public int trava = 1;

   void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("bola") && trava == 1)
        { 
            trava--;
            audioS.Play();
            GameManager.instance.moedasIntSave += 10;
            GameManager.instance.moedasUI.text = (GameManager.instance.moedasIntSave).ToString();
            Instantiate(efeitoMoeda, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);
        }

    }
}
