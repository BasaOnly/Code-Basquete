using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpaShots : MonoBehaviour
{
 
    void DesativaShoots()
    {
        GameManager.instance.swishShot = false;
        GameManager.instance.skyHook = false;
        GameManager.instance.deTabela = false;
        GameManager.instance.rimShoot = false;

    }

}
