﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public GameObject ButtonsHolder;
    public GameObject SquadBuilder;
    public GameObject BackgroundImage;

    // Use this for initialization
    void Start () {
        //TODO: Adjust size for small screen resolutions
        ButtonsHolder.transform.position = new Vector3(Screen.width / 20, Screen.height - Screen.height / 20, 0.0f);
        BackgroundImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height * 16f/9f, Screen.height);
        //BackgroundImage.transform.position = new Vector3(Screen.width - Screen.height, 0, 0);
        Global.test = "Changed";
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void StartNewGame()
    {
        SceneManager.LoadScene("Battle");
    }

    public void OpenSquadronBuilder()
    {
        ButtonsHolder.SetActive(false);
        SquadBuilder.SetActive(true);
    }

    public void CloseSquadronBuilder()
    {
        SquadBuilder.SetActive(false);
        ButtonsHolder.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
