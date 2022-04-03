using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class SaveStuff
{
    public static float backGroundAudioVolume;
    public static float inGameAudioVolume;
}

public class Menu : MonoBehaviour
{
    public GameObject mainMenuHolder;
    public GameObject optionsMenuHolder;
    public AudioSource backGroundAudioVolume;
    public Transform inGameAudioVolume;

    public Slider gameVolumeSlider;
    public Slider playerVolumeSlider;
    private AudioSource gameBackgroundAudiosource;
    public AudioSource[] Audios;

    void Start()
    {
        gameVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        playerVolumeSlider.value = PlayerPrefs.GetFloat("PlayerVolume");
        Audios = inGameAudioVolume.GetComponents<AudioSource>();
    }
  
    public void Play()
    {
        SceneManager.LoadScene("Done_Main");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        mainMenuHolder.SetActive(false);
        optionsMenuHolder.SetActive(true);
    }

    public void MainMenu()
    {
        mainMenuHolder.SetActive(true);
        optionsMenuHolder.SetActive(false);
    }
  
    public void SetGameVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("MusicVolume", slider.value);
        SaveStuff.backGroundAudioVolume = PlayerPrefs.GetFloat("MusicVolume");
        backGroundAudioVolume.volume = slider.value;
    }

    public void SetPlayerVolume(Slider slider)
    {
        for (int i = 0; i < Audios.Length; i++)
        {
            Audios[i].volume = slider.value;
        }
        PlayerPrefs.SetFloat("PlayerVolume", slider.value);
        SaveStuff.inGameAudioVolume = PlayerPrefs.GetFloat("PlayerVolume");
    }
}