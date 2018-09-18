using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

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
        SceneManager.LoadScene(index);
    }
}
