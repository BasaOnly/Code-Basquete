using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiShot : MonoBehaviour
{
    public int trava = 0;
    [SerializeField]
    private AudioSource audioS;

    void Update()
    {
        if (GameManager.instance.swishShot && trava == 0)
        {
            trava = 1;
            audioS.Play();
        }
    }
}
