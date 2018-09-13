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

        if (Input.GetKeyDown(KeyCode.E) && triggered == true && !used && Dialog.Instance.finished == true)
        {
            if (objectName == "lightSwitchBedroom")
            {
                if(changeAlpha == true)
                {
                    lightmap.color = new Color(255, 255, 255, lightmap.color.a);
                }
                entity.SetActive(true);
                TriggerFlowchart.Instance.bedroomLightsOn = true;
                Dialog.Instance.NextSentence();
            }
            if (objectName == "glasses" && TriggerFlowchart.Instance.bedroomLightsOn == true && Dialog.Instance.finished == true)
            {
                entity.SetActive(false);
                TriggerFlowchart.Instance.pickupGlasses = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
                Dialog.Instance.NextSentence();
            }
            if(objectName == "bathroom" && TriggerFlowchart.Instance.pickupGlasses == true && Dialog.Instance.finished == true)
            {
                Player.Instance.moveSpeed = 0;
                StartCoroutine("FadeCheck");
                TriggerFlowchart.Instance.bathroomWashUp = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
            }
            if (objectName == "bedroomBottle" && TriggerFlowchart.Instance.bathroomWashUp == true && Dialog.Instance.finished == true)
            {
                entity.SetActive(false);
                TriggerFlowchart.Instance.bottleTaken = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
                Dialog.Instance.NextSentence();
            }
            if (objectName == "bedroomExit" && TriggerFlowchart.Instance.bottleTaken == true && Dialog.Instance.finished == true)
            {
                UIManager.Instance.StartCoroutine("Fading");
                TriggerFlowchart.Instance.exitBedroom = true;
            }
            if (objectName == "lightSwitchUS" && TriggerFlowchart.Instance.exitBedroom == true && Dialog.Instance.finished == true)
            {
                if (changeAlpha == true)
                {
                    lightmap.color = new Color(255, 255, 255, lightmap.color.a);
                }
                entity.SetActive(true);
                TriggerFlowchart.Instance.bedroomLightsOn = true;
                Dialog.Instance.NextSentence();
            }
            if (objectName == "boxes" && TriggerFlowchart.Instance.bedroomLightsOn == true && Dialog.Instance.finished == true)
            {
                TriggerFlowchart.Instance.boxes = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
                Dialog.Instance.NextSentence();
            }
            if (objectName == "other_door" && TriggerFlowchart.Instance.boxes == true && Dialog.Instance.finished == true)
            {
                TriggerFlowchart.Instance.otherDoor = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
                Dialog.Instance.NextSentence();
            }
            if (objectName == "downStairs" && TriggerFlowchart.Instance.otherDoor == true && Dialog.Instance.finished == true)
            {
                UIManager.Instance.StartCoroutine("Fading");
                TriggerFlowchart.Instance.downStairs = true;
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
        yield return new WaitForSeconds(5);
        Fade(false, 1);
        Dialog.Instance.NextSentence();
        Player.Instance.ChangeClothes();
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
        if (other.tag == "Player" && stay && TriggerFlowchart.Instance.bedroomLightsOn == true)
        {
            triggered = true;
            if (changeAlpha == true)
            {
                lightmap.color = new Color(255, 255, 255, 1);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            triggered = false;
            if (changeAlpha == true && stay && TriggerFlowchart.Instance.bedroomLightsOn == true)
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
