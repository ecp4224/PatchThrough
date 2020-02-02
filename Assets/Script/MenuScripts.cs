using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject StoryMenu;
    public GameObject CreditMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        StoryMenu.SetActive(false);
        CreditMenu.SetActive(false);

    }
    public void OpenStoryMenu()
    {
        MainMenu.SetActive(false);
        StoryMenu.SetActive(true);
        CreditMenu.SetActive(false);

    }
    public void OpenCreditMenu()
    {
        MainMenu.SetActive(false);
        StoryMenu.SetActive(false);
        CreditMenu.SetActive(true);

    }

    public void OpenGameplay()
    {
        SceneManager.LoadScene("World", LoadSceneMode.Single);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
