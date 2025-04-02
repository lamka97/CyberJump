using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu; //Odkaz na GameObject pause menu. SerializeField umožní jeho nastavení v editoru Unity.

    public void Pause() //metoda pro zastavení hry
    {
        pauseMenu.SetActive(true); //zobrazí pause menu
        Time.timeScale = 0; //zastaví hru
    }
    public void Resume()//metoda pro pokraèování ve høe
    {
        pauseMenu.SetActive(false); //skryje pause menu
        Time.timeScale = 1; //obnoví chod hry
    }
    public void Restart() //metoda pro restartování dané úrovnì
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //naète znovu aktuální scénu
        Time.timeScale = 1;
    }
    public void Home() //metoda pro návrat do hlavního menu
    {
        SceneManager.LoadScene("main menu"); //naète scénu s názvem main menu
        Time.timeScale = 1;
    }
}
