using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    public GameObject playerHUD;
    public GameObject deathScreen;
    public GameObject victoryScreen;
    private void Awake()
    {
        Instance = this;
    }

    public void RetrunToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void ShowDeathScreen()
    {
        playerHUD.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ShowVictoryScreen()
    {
        playerHUD.gameObject.SetActive(false);
        victoryScreen.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void LastCheckpoint()
    {
        playerHUD.gameObject.SetActive(true);
        deathScreen.gameObject.SetActive(false);
        var player = GameObject.Find("Player");


        var health = player.GetComponent<Health>();
        health.Heal(100);
        if (CheckPointManager.instance.lastCheckpoint == null)
        {
            RestartLevel();
            return;

        }
        player.transform.position = CheckPointManager.instance.lastCheckpoint.transform.position + new Vector3(0f, 10f, 0f);
        var pickups = GameObject.Find("ItemPickUps");
        for (int i = 0; i < pickups.transform.childCount; i++)
        {
            pickups.transform.GetChild(i).gameObject.SetActive(true);
        }
        player.GetComponent<RagdollController>().DeactivateRagdoll();
        player.GetComponent<CharacterController>().enabled = true;
    }
}
