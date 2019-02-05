using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	//Músicas
	public AudioClip[] clips;
	public AudioSource audioS;
    public int pause;
	

	public static AudioManager instance;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
		else
		{
			Destroy (gameObject);
		}
        pause = -1;
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (pause == 1)
        {
            audioS.Pause();
        }
        else if (!audioS.isPlaying)
        {
            audioS.clip = GetRandom();
            audioS.Play();
        }
    }

	AudioClip GetRandom()
	{
		return clips [Random.Range (0, clips.Length)];
	}


}
