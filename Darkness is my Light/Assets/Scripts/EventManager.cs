using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    public class EventManager : PostEffectsBase
    {
        public bool awakenEyes;
        public bool bedroomNight;
        public GameObject dialog;
        public GameObject[] eyes;
        bool awakenTrigger = false;

        BlurOptimized blur;

        float tParam = 0;
        float speed = 0.125f;
        float varToBeLerped = 0;

        // Use this for initialization
        void Start()
        {
            if (bedroomNight)
            {
                dialog.gameObject.SetActive(true);
            }
            if (awakenEyes)
            {
                eyes[0].gameObject.SetActive(true);
            }
            blur = GetComponent<BlurOptimized>();
        }

        // Update is called once per frame
        void Update()
        {
            if(awakenTrigger == true)
            {
                RemoveBlur();
            }
            HandleKeys();
        }

        void HandleKeys()
        {
            if (Input.anyKeyDown && Flowchart.Instance.awaken == false)
            {
                if(awakenTrigger == false)
                {
                    StartCoroutine("Awaken");
                    awakenTrigger = true;
                    StartCoroutine("ChangeBool");
                }
            }
            if (Input.anyKeyDown && Flowchart.Instance.bedroomGetUp == false && Flowchart.Instance.awaken == true)
            {
                UIManager.Instance.StartCoroutine("Fading");
                Flowchart.Instance.bedroomGetUp = true;
            }
        }

        IEnumerator ChangeBool()
        {
            yield return new WaitForSeconds(9);
            Flowchart.Instance.awaken = true;
            StopCoroutine("ChangeBool");
        }

        public void RemoveBlur()
        {
            if (tParam < 1)
            {
                tParam += Time.deltaTime * speed;
                varToBeLerped = Mathf.Lerp(5, 0, tParam);
            }
            blur.blurIterations = (int)varToBeLerped;
        }

        private IEnumerator Awaken()
        {            
            eyes[0].gameObject.SetActive(false);
            eyes[1].gameObject.SetActive(true);
            yield return new WaitForSeconds(5);
            eyes[1].gameObject.SetActive(false);
            eyes[2].gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            eyes[2].gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            dialog.gameObject.SetActive(true);
        }
    }
}