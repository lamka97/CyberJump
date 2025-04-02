using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static SceneManage instance;

    private void Awake()
    {
        if (instance == null)//Kontrola zda instance již neexistuje
        {
            instance = this; //Nastaví tuto instanci jako hlavní
            DontDestroyOnLoad(gameObject); //zajistí aby objekt nebyl znièen
        }
        else
        {
            Destroy(gameObject); //pokud instance existuje znièí duplikát aby existovala pouze jedna
        }
    }
    public void NextLevel() //Metoda pro naètení další úrovnì
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);//Naète scénu s indexem o 1 vyšší než aktuální
    }
    public void LoadScene(string sceneName) //Metoda pro naètení konkrétní scény podle jména
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
