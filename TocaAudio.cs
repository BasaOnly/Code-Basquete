﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TocaAudio : MonoBehaviour
{
  public static void TocadorAudio(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        if (!source.isPlaying)
        {
            source.Play();
        }
    }

}
