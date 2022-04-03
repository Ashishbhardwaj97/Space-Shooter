using UnityEngine;
using System.Collections;

public class Done_DestroyByTime : MonoBehaviour
{
	public float lifetime;

	void Start ()
	{
        AudioSource audio = GetComponent<AudioSource>();
        audio.volume = SaveStuff.inGameAudioVolume;
        Destroy (gameObject, lifetime);
        audio.Play();
	}
}
