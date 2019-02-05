using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Carregando : MonoBehaviour
{
  


    void Start()
    {
        StartCoroutine(LoadGameProg());

    }
   

    IEnumerator LoadGameProg()
    {

        yield return new WaitForSeconds(0.5f);

        AsyncOperation async = Application.LoadLevelAsync("Menu_Inicial");

        //se a operacao termino
        while (!async.isDone)
        {

            
            yield return null;
        }
    }
}
