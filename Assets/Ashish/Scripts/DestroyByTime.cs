using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float lifetime;

    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.volume = SaveStuff.inGameAudioVolume;
        Destroy(gameObject, lifetime);
    }
}       
