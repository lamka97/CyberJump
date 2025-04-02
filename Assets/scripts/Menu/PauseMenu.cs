using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; //Odkaz na GameObject pause menu. SerializeField umo�n� jeho nastaven� v editoru Unity.

    public void Pause() //metoda pro zastaven� hry
    {
        pauseMenu.SetActive(true); //zobraz� pause menu
        Time.timeScale = 0; //zastav� hru
    }
    public void Resume()//metoda pro pokra�ov�n� ve h�e
    {
        pauseMenu.SetActive(false); //skryje pause menu
        Time.timeScale = 1; //obnov� chod hry
    }
    public void Restart() //metoda pro restartov�n� dan� �rovn�
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //na�te znovu aktu�ln� sc�nu
        Time.timeScale = 1;
    }
    public void Home() //metoda pro n�vrat do hlavn�ho menu
    {
        SceneManager.LoadScene("main menu"); //na�te sc�nu s n�zvem main menu
        Time.timeScale = 1;
    }
}
