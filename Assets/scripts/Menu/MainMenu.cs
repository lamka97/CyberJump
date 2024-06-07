using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1); //Díky tomuhle naèteme scénu která je na indexu èíslo 1
    }

    public void QuitGame()
    {
        Application.Quit(); //Díky tomuhle øádku vypneme hru
    }
}
