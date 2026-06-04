using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthorsButton : MonoBehaviour
{
    public Button newGameButton;
    void Start()
    {
        newGameButton.onClick.AddListener(StartNextGame);
    }
    void StartNextGame()
    {
        SceneManager.LoadScene("Authors");
    }
}
