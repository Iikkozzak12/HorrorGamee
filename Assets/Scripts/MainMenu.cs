using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //Panele:
    //Ekran g³owny menu
    public GameObject mainScreen;

    //Ekran ustawieñ
    public GameObject optionsScreen;

    //Ekran napisów koñcowych
    public GameObject creditsScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created





    //Metody


    //Menu G³owne

    //Rozpoczêcie nowej gry
    public void StartGame()
    {
        //£adowanie sceny z poziomem gry
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
