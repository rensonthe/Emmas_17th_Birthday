using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour {

    public static Dialog Instance { get; set; }

    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public bool solo;
    public bool noAnim;
    public bool auto;
    public bool finished = false;
    public bool dontStart = false;

    public GameObject continueButton;
    public Animator textDisplayAnim;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if(dontStart == false)
        {
            StartCoroutine(Type());
        }
    }

    void Update()
    {
        FinishSentece();
    }

    void FinishSentece()
    {
        if (textDisplay.text == sentences[index])
        {
            if (!noAnim)
            {
                textDisplayAnim.SetTrigger("Fadeout");
            }
            if (!solo && !finished)
            {
                if (auto == false)
                {
                    continueButton.SetActive(true);
                }
                finished = true;
            }
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1 && finished == true)
        {
            Debug.Log("run");
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            finished = false;
        }
        else
        {
            Debug.Log("ran");
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }
}
