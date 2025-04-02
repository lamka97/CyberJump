using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    AudioManager audioManager; //Prom�nn� pro spr�vu zvuk�

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Zajist� aby objekt nebyl zni�en p�i na��t�n� nov� sc�ny
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // Najde objekt kter� m� tag "Audio" a z�sk� komponentu AudioManager
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Zjist� jestli se toho dotkne postava
        {
            audioManager.PlaySFX(audioManager.finish); // P�ehraje zvuk dokon�en� �rovn�
            UnlockNewLevel(); //Zavol� funkci pro odemknut� nov� �rovn�
            SceneManage.instance.NextLevel(); //P�epne na dal�� �rove� (sc�nu)
        }
    }
    void UnlockNewLevel() //Funkce pro odemknut� nov� �rovn�
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex")) //Zjist�me jestli je aktu�ln� �rove� vy��� nebo rovna �rovni kter� je ulo�en� v playerprefs
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1); //Aktualizuje hodnotu na hodnotu aktu�ln� �rovn� + 1
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1); //zv��� hodnotu odemknut�ch �rvn� o 1
            PlayerPrefs.Save(); //ulo�en� do playerprefs
        }
    }
}
