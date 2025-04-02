using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer; //Odkaz na AudioMixer, kter� ��d� hlasitost zvuk�
    [SerializeField] private Slider musicSlider; //Odkaz na slider pro hudbu
    [SerializeField] private Slider SFX_Slider; //// Odkaz na slider pro zvukov� efekty (SFX)

    private void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume")) //Kontrola jestli u�ivatel m� ulo�en� data o hlasitosti
        {
            LoadVolume(); //kdy� ano na�te ulo�en� data hlasitosti
        }
        else //pokud ne
        {
            SetMusicVolume(); //nastav� v�choz� hodnotu hlasitosti hudby
            SetSFXVolume(); //nastav� v�choz� hodnotu hlasitosti efekt�
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value; //Z�sk� hodnotu ze slideru pro hudbu
        myMixer.SetFloat("music", Mathf.Log10(volume)*20); //p�evod hodnoty na decibely pro AudioMixer
        PlayerPrefs.SetFloat("musicVolume", volume); //ulo�� novou hodnotu do PlayerPrefs
    }
    public void SetSFXVolume()
    {
        float volume = SFX_Slider.value; //z�sk� hodnotu ze slideru pro SFX
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX_Volume", volume);
    }
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume"); //na�te hudbu ze souboru ulo�en�ch dat
        SFX_Slider.value = PlayerPrefs.GetFloat("SFX_Volume"); //na�te efekty ze souboru ulo�en�ch dat

        SetMusicVolume(); //Aplikuje hodnotu na AudioMixer
        SetSFXVolume();
    }
}
