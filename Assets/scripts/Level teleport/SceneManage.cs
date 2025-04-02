using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static SceneManage instance;

    private void Awake()
    {
        if (instance == null)//Kontrola zda instance ji� neexistuje
        {
            instance = this; //Nastav� tuto instanci jako hlavn�
            DontDestroyOnLoad(gameObject); //zajist� aby objekt nebyl zni�en
        }
        else
        {
            Destroy(gameObject); //pokud instance existuje zni�� duplik�t aby existovala pouze jedna
        }
    }
    public void NextLevel() //Metoda pro na�ten� dal�� �rovn�
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);//Na�te sc�nu s indexem o 1 vy��� ne� aktu�ln�
    }
    public void LoadScene(string sceneName) //Metoda pro na�ten� konkr�tn� sc�ny podle jm�na
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
