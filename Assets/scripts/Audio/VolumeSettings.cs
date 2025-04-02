using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer; //Odkaz na AudioMixer, který øídí hlasitost zvukù
    [SerializeField] private Slider musicSlider; //Odkaz na slider pro hudbu
    [SerializeField] private Slider SFX_Slider; //// Odkaz na slider pro zvukové efekty (SFX)

    private void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume")) //Kontrola jestli uživatel má uložená data o hlasitosti
        {
            LoadVolume(); //když ano naète uložená data hlasitosti
        }
        else //pokud ne
        {
            SetMusicVolume(); //nastaví výchozí hodnotu hlasitosti hudby
            SetSFXVolume(); //nastaví výchozí hodnotu hlasitosti efektù
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value; //Získá hodnotu ze slideru pro hudbu
        myMixer.SetFloat("music", Mathf.Log10(volume)*20); //pøevod hodnoty na decibely pro AudioMixer
        PlayerPrefs.SetFloat("musicVolume", volume); //uloží novou hodnotu do PlayerPrefs
    }
    public void SetSFXVolume()
    {
        float volume = SFX_Slider.value; //získá hodnotu ze slideru pro SFX
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX_Volume", volume);
    }
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume"); //naète hudbu ze souboru uložených dat
        SFX_Slider.value = PlayerPrefs.GetFloat("SFX_Volume"); //naète efekty ze souboru uložených dat

        SetMusicVolume(); //Aplikuje hodnotu na AudioMixer
        SetSFXVolume();
    }
}
