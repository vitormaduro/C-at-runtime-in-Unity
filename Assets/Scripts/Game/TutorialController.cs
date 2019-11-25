using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Luminosity.IO;

public class TutorialController : MonoBehaviour
{
    private int currentMessage = 0;
    private List<string> tutorialMessages;
    private bool isTutorialActive = false;
    private bool isOldTutorialOpen = false;
    private TextMeshProUGUI messageObject;
    private TextMeshProUGUI oldTutorial;

    private void Start() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetTutorial(string xmlFileName) {
        tutorialMessages = GetComponent<XmlReader>().ReadXml(xmlFileName, "text", "message");
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Additive);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Tutorial") {
            isTutorialActive = true;
            messageObject = GameObject.FindGameObjectWithTag("Tutorial Text").GetComponent<TextMeshProUGUI>();
            messageObject.text = tutorialMessages[0];
            oldTutorial = GameObject.Find("Old Tutorial").GetComponent<TextMeshProUGUI>();
            oldTutorial.text = "";

            foreach (string s in tutorialMessages) {
                oldTutorial.text += s;
            }

            oldTutorial.gameObject.SetActive(false);
        }
    }

    private void Update() {
        if(isTutorialActive) {
            if (InputManager.GetAction(0, "Next Tutorial").GetButtonDown()) {
                if(currentMessage < tutorialMessages.Count - 1) {
                    currentMessage++;
                    messageObject.text = tutorialMessages[currentMessage];
                } else {
                    SceneManager.UnloadSceneAsync("Tutorial");
                    isTutorialActive = false;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<GunController>().enabled = true;
                }
            } else if (InputManager.GetAction(0, "Previous Tutorial").GetButtonDown()) {
                if(currentMessage > 0) {
                    currentMessage--;
                    messageObject.text = tutorialMessages[currentMessage];
                }
            }
        }

        if (InputManager.GetAction(0, "Past Tutorial").GetButtonDown()) {
            switch (isOldTutorialOpen) {
                case true:
                    oldTutorial.gameObject.SetActive(false);
                    isOldTutorialOpen = false;
                    break;

                case false:
                    oldTutorial.gameObject.SetActive(true);
                    isOldTutorialOpen = true;
                    break;
            }
        }
    }
}
