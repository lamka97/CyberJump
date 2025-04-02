using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    AudioManager audioManager; //Promìnná pro správu zvukù

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Zajistí aby objekt nebyl znièen pøi naèítání nové scény
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // Najde objekt který má tag "Audio" a získá komponentu AudioManager
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Zjistí jestli se toho dotkne postava
        {
            audioManager.PlaySFX(audioManager.finish); // Pøehraje zvuk dokonèení úrovnì
            UnlockNewLevel(); //Zavolá funkci pro odemknutí nové úrovnì
            SceneManage.instance.NextLevel(); //Pøepne na další úroveò (scénu)
        }
    }
    void UnlockNewLevel() //Funkce pro odemknutí nové úrovnì
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex")) //Zjistíme jestli je aktuální úroveò vyšší nebo rovna úrovni která je uložená v playerprefs
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1); //Aktualizuje hodnotu na hodnotu aktuální úrovnì + 1
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1); //zvýší hodnotu odemknutých úrvní o 1
            PlayerPrefs.Save(); //uložení do playerprefs
        }
    }
}
