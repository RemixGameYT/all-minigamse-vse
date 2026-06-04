using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitGameButton : MonoBehaviour
{
    public Button exitButton;
    void Start()
    {
        exitButton.onClick.AddListener(ExitGame);
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
