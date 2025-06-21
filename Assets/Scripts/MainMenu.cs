using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //Panele:
    //Ekran g�owny menu
    public GameObject mainScreen;

    //Ekran ustawie�
    public GameObject optionsScreen;

    //Ekran napis�w ko�cowych
    public GameObject creditsScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created





    //Metody


    //Menu G�owne

    //Rozpocz�cie nowej gry
    public void StartGame()
    {
        //�adowanie sceny z poziomem gry
        SceneManager.LoadScene("Level_1");
    }

    //Zamykanie gry
    public void QuitGame()
    {
        Application.Quit();
    }






    //Opcje






    //Napisy
}
