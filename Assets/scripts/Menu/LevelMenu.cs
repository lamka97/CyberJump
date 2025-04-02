using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons; //pole tla��tek pro v�b�r �rovn�
    public GameObject levelButtons; 
    private void Awake()
    {
        ButtonsToArray(); //napln� pole buttons tla��tky ze sc�ny
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); //Zjist� po�et odem�en�ch level� z ulo�en�ch dat, v�choz� hodnota je 1
        for (int i = 0; i < buttons.Length; i++) //deaktivuje v�echna tla��tka
        {
            buttons[i].interactable = false; //nastav� jako neaktivn�
        }
        for (int i = 0;i < unlockedLevel; i++) //aktivuje po�et tla��tek podle odem�en�ch level�
        {
            buttons[i].interactable = true; //nastav� dan� tla��tka jako aktivn�
        }
    }
    public void OpenLevel(int levelID)//Metoda pro na�ten� vybran� �rovn�
    {
        string levelName = "Level " + levelID; //Vytvo�� n�zev sc�ny na z�klad� ��sla �rovn�
        SceneManager.LoadScene(levelName); //Na�te sc�nu s dan�m n�zvem
    }
    void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;//Po�et tla��tek v objektu levelButtons
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>(); //p�i�ad� tla��tka do pole
        }
    }
}
