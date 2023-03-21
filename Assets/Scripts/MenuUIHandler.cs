using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public InputField nameinputField;
    public Text nameText;
    public static MenuUIHandler Instance;
    public string nameKey = "PlayerName";

    public void NewStart()
    {
        string playerName = nameText.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);

    }

    public void Exit()
    {
        EditorApplication.ExitPlaymode();

        Application.Quit();
    }

 
    

}
