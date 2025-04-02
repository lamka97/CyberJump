using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons; //pole tlaøítek pro výbìr úrovnì
    public GameObject levelButtons; 
    private void Awake()
    {
        ButtonsToArray(); //naplní pole buttons tlaèítky ze scény
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); //Zjistí poèet odemèených levelù z uložených dat, výchozí hodnota je 1
        for (int i = 0; i < buttons.Length; i++) //deaktivuje všechna tlaèítka
        {
            buttons[i].interactable = false; //nastaví jako neaktivní
        }
        for (int i = 0;i < unlockedLevel; i++) //aktivuje poèet tlaèítek podle odemèených levelù
        {
            buttons[i].interactable = true; //nastaví dané tlaèítka jako aktivní
        }
    }
    public void OpenLevel(int levelID)//Metoda pro naètení vybrané úrovnì
    {
        string levelName = "Level " + levelID; //Vytvoøí název scény na základì èísla úrovnì
        SceneManager.LoadScene(levelName); //Naète scénu s daným názvem
    }
    void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;//Poèet tlaèítek v objektu levelButtons
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>(); //pøiøadí tlaèítka do pole
        }
    }
}
