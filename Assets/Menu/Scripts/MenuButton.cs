using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Button menuButton;
    void Start()
    {
        menuButton.onClick.AddListener(LoadMenu);
    }
    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

