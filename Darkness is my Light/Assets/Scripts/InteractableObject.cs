using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public string objectName;
    public GameObject entity;
    public GameObject highlight;
    public bool triggered;
    public bool used;
    public bool stay;

    public SpriteRenderer lightmap;
    public bool changeAlpha;

    public Image fadeImage;

    private bool isInTransition;
    private float transition;
    private bool isShowing;
    private float duration;

    public void Fade(bool showing, float duration)
    {
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E) && triggered == true && !used)
        {
            if (objectName == "lightSwitchBedroom")
            {
                if(changeAlpha == true)
                {
                    lightmap.color = new Color(255, 255, 255, lightmap.color.a);
                }
                entity.SetActive(true);
                Flowchart.Instance.bedroomLightsOn = true;
            }
            if (objectName == "glasses" && Flowchart.Instance.bedroomLightsOn == true)
            {
                entity.SetActive(false);
                Flowchart.Instance.pickupGlasses = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
            }
            if(objectName == "bathroom" && Flowchart.Instance.pickupGlasses == true)
            {
                Player.Instance.moveSpeed = 0;
                StartCoroutine("FadeCheck");
                Flowchart.Instance.bathroomWashUp = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
            }
            if (objectName == "bedroomBottle" && Flowchart.Instance.bathroomWashUp == true)
            {
                entity.SetActive(false);
                Flowchart.Instance.bottleTaken = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
            }
            if (objectName == "bedroomExit" && Flowchart.Instance.bottleTaken == true)
            {
                UIManager.Instance.StartCoroutine("Fading");
                Flowchart.Instance.exitBedroom = true;
            }
        }

        if (!isInTransition)
            return;


        transition += isShowing ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        fadeImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);

        if (transition > 1 || transition < 0)
            isInTransition = false;
    }

    public IEnumerator FadeCheck()
    {
        Fade(true, 1);
        yield return new WaitForSeconds(1);
        Fade(false, 1);
        Player.Instance.moveSpeed = 3;
        StopCoroutine("FadeCheck");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !stay)
        {
            triggered = true;
            if (!used)
            {
                highlight.SetActive(true);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && stay)
        {
            triggered = true;
            if (changeAlpha == true)
            {
                lightmap.color = new Color(255, 255, 255, 255);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggered = false;
            if (changeAlpha == true && stay)
            {
                lightmap.color = new Color(255, 255, 255, 0.5f);
            }
            if (!used && !stay)
            {
                highlight.SetActive(false);
            }
        }
    }
}
