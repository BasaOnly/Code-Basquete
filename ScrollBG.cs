using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBG : MonoBehaviour
{
    public RawImage back;

    // Update is called once per frame
    void Update()
    {
        back.uvRect = new Rect(0.02f * Time.time, 0, 1, 1);
     
    }
}
