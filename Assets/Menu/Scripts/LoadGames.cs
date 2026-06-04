using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGames : MonoBehaviour
{
    public Button game1Button;
    public Button game2Button;
    public Button game3Button;
    public Button game4Button;
    void Start()
    {
        game1Button.onClick.AddListener(LoadGame1);
        game2Button.onClick.AddListener(LoadGame2);
        game3Button.onClick.AddListener(LoadGame3);
        game4Button.onClick.AddListener(LoadGame4);
    }
    void LoadGame1()
    {
        SceneManager.LoadScene("Game1");
    }
    void LoadGame2()
    {
        SceneManager.LoadScene("Game2");
    }
    void LoadGame3()
    {
        SceneManager.LoadScene("Game3");
    }
    void LoadGame4()
    {
        SceneManager.LoadScene("Game4");
    }
}
