﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

    public static UIManager Instance { get; set; }
    
    public int index;
    public string levelName;

    public Image fadeImage;
    public Animator animator;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayGame()
    {
        StartCoroutine("Fading");
    }

    public IEnumerator Fading()
    {
        Debug.Log("run");
        animator.SetBool("Fade", true);
        yield return new WaitUntil(() => fadeImage.color.a == 1);
        SceneManager.LoadScene(index);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(8);
    }

    public void CopyLink()
    {
        string s = "https://drive.google.com/drive/folders/1HuF9-htW1eidTYhFKe9ZGPUuvt6G7Bxa?usp=sharing";
        s.CopyToClipboard();
        Copy.CopyToClipboard(s);
    }

    public void CopyLinkYoutube()
    {
        string s = "https://www.youtube.com/watch?v=YA9ekAnCPOQ&index=3&list=LL2ecOE_gG5ywGnFSNfdkDqg&t=0s";
        s.CopyToClipboard();
        Copy.CopyToClipboard(s);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
