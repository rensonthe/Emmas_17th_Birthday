using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Fungus;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public Flowchart flowchart;

    public string objectName;
    public GameObject entity;
    public GameObject highlight;
    public GameObject originalCollision;
    public GameObject changeCollision;
    public bool triggered;
    public bool used;
    public bool stay;
    public bool change;
    private bool changed = false;
    public SpriteRenderer lightmap;
    public bool changeAlpha;
    public bool notBedroom;

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
                AudioManager.instance.PlaySound2D("lightSwitch");
                if (changeAlpha == true)
                {
                    lightmap.color = new Color(255, 255, 255, lightmap.color.a);
                }
                entity.SetActive(true);
                flowchart.SendFungusMessage("1");
                TriggerFlowchart.Instance.bedroomLightsOn = true;
            }
            if (objectName == "glasses" && TriggerFlowchart.Instance.bedroomLightsOn == true)
            {
                entity.SetActive(false);
                TriggerFlowchart.Instance.pickupGlasses = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
                flowchart.SendFungusMessage("2");
            }
            if(objectName == "bathroom" && TriggerFlowchart.Instance.pickupGlasses == true)
            {
                Player.Instance.moveSpeed = 0;
                StartCoroutine("FadeCheck");
                TriggerFlowchart.Instance.bathroomWashUp = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
            }
            if (objectName == "bedroomBottle" && TriggerFlowchart.Instance.bathroomWashUp == true)
            {
                entity.SetActive(false);
                TriggerFlowchart.Instance.bottleTaken = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
                flowchart.SendFungusMessage("4");
            }
            if (objectName == "bedroomExit" && TriggerFlowchart.Instance.bottleTaken == true)
            {
                AudioManager.instance.PlaySound2D("door");
                UIManager.Instance.StartCoroutine("Fading");
                TriggerFlowchart.Instance.exitBedroom = true;
            }
            if (objectName == "lightSwitchUS" && TriggerFlowchart.Instance.exitBedroom == true)
            {
                AudioManager.instance.PlaySound2D("lightSwitch");
                if (changeAlpha == true)
                {
                    lightmap.color = new Color(255, 255, 255, lightmap.color.a);
                }
                entity.SetActive(true);
                TriggerFlowchart.Instance.bedroomLightsOn = true;
            }
            if (objectName == "boxes" && TriggerFlowchart.Instance.bedroomLightsOn == true)
            {
                TriggerFlowchart.Instance.boxes = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
                flowchart.SendFungusMessage("1");
            }
            if (objectName == "other_door" && TriggerFlowchart.Instance.boxes == true)
            {
                TriggerFlowchart.Instance.otherDoor = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
                flowchart.SendFungusMessage("2");
            }
            if (objectName == "downStairs" && TriggerFlowchart.Instance.otherDoor == true)
            {                
                UIManager.Instance.StartCoroutine("Fading");
                TriggerFlowchart.Instance.downStairs = true;
            }
            if (objectName == "lightSwitchDS" && TriggerFlowchart.Instance.downStairs == true)
            {
                AudioManager.instance.PlaySound2D("lightSwitch");
                if (changeAlpha == true)
                {
                    lightmap.color = new Color(255, 255, 255, lightmap.color.a);
                }
                entity.SetActive(true);
                TriggerFlowchart.Instance.bedroomLightsOn = true;
            }
            if (objectName == "food" && TriggerFlowchart.Instance.bedroomLightsOn == true)
            {
                TriggerFlowchart.Instance.eaten = true;
                highlight.SetActive(false);
                triggered = false;
                used = true;
                flowchart.SendFungusMessage("1");
            }
            if (objectName == "dsBottle" && TriggerFlowchart.Instance.eaten == true)
            {
                TriggerFlowchart.Instance.drank = true;
                entity.SetActive(true);
                highlight.SetActive(false);
                triggered = false;
                used = true;
                flowchart.SendFungusMessage("2");
            }
            if (objectName == "chair" && TriggerFlowchart.Instance.drank == true)
            {
                UIManager.Instance.StartCoroutine("Fading");
                TriggerFlowchart.Instance.sat = true;
            }
            if (objectName == "upBottle" && TriggerFlowchart.Instance.downStairs == true)
            {
                entity.SetActive(false);
                TriggerFlowchart.Instance.bottleTaken = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
                flowchart.SendFungusMessage("1");
            }
            if (objectName == "upSleep" && TriggerFlowchart.Instance.bottleTaken == true)
            {
                UIManager.Instance.StartCoroutine("Fading");
                TriggerFlowchart.Instance.downStairs = true;
            }
            if (objectName == "sleepBottle" && TriggerFlowchart.Instance.bedroomGetUp == true)
            {
                TriggerFlowchart.Instance.bottleTaken = true;
                entity.SetActive(true);
                highlight.SetActive(false);
                triggered = false;
                used = true;
                flowchart.SendFungusMessage("1");
            }
            if (objectName == "sleepGlasses" && TriggerFlowchart.Instance.bottleTaken == true)
            {
                TriggerFlowchart.Instance.pickupGlasses = true;
                entity.SetActive(true);
                highlight.SetActive(false);
                triggered = false;
                used = true;
                flowchart.SendFungusMessage("2");
            }
            if (objectName == "sleepBathroom" && TriggerFlowchart.Instance.pickupGlasses == true)
            {
                Player.Instance.moveSpeed = 0;
                StartCoroutine("FadeCheck");
                TriggerFlowchart.Instance.bathroomWashUp = true;
                triggered = false;
                used = true;
                highlight.SetActive(false);
            }
            if (objectName == "sleep" && TriggerFlowchart.Instance.bathroomWashUp == true)
            {
                UIManager.Instance.StartCoroutine("Fading");
                TriggerFlowchart.Instance.sat = true;
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
        AudioManager.instance.PlaySound2D("sink");
        yield return new WaitForSeconds(6);
        AudioManager.instance.PlaySound2D("teeth");
        yield return new WaitForSeconds(6);
        AudioManager.instance.PlaySound2D("clothes");
        yield return new WaitForSeconds(3);
        Fade(false, 1);
        Player.Instance.ChangeClothes();
        flowchart.SendFungusMessage("3");
        StopCoroutine("FadeCheck");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !stay)
        {
            triggered = true;
            if (!used && !change)
            {
                highlight.SetActive(true);
            }
        }
        if (other.tag == "Player" && TriggerFlowchart.Instance.bedroomLightsOn == true)
        {
            if(change == true && !changed)
            {
                originalCollision.SetActive(false);
                changeCollision.SetActive(true);
                Player.Instance.ChangeScale();
                changed = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && stay && TriggerFlowchart.Instance.bedroomLightsOn == true)
        {
            if (notBedroom == false)
            {
                lightmap.sortingOrder = 1;
            }
            else
            {
                lightmap.sortingOrder = 3;
            }

            triggered = true;
            if (changeAlpha == true)
            {
                Debug.Log("ran");
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
            if (!used && !stay && !change)
            {
                highlight.SetActive(false);
            }
        }
    }
}
