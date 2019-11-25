using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;
using UnityEngine.UI;
using System;
using Luminosity.IO;

public class CanvasController : MonoBehaviour
{
    private bool canvasActive = false;
    private GameObject crosshair;
    private GameObject compileButton;
    private TextMeshProUGUI scriptName;

    public GameObject textArea;
    public GameObject target;

    private void Start() {
        scriptName = GameObject.FindGameObjectWithTag("Script Name").GetComponent<TextMeshProUGUI>();
        textArea = GameObject.FindGameObjectWithTag("Script Input");
        crosshair = GameObject.Find("Crosshair");
        compileButton = GameObject.Find("Compile Button");
        compileButton.GetComponent<Button>().onClick.AddListener(() => GameObject.FindGameObjectWithTag("GameController").GetComponent<Compiler>().Compile(textArea, scriptName.text, target));
        HideCanvas();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && canvasActive) {
            HideCanvas();
        }

        if (Input.GetKeyDown(KeyCode.RightControl) && canvasActive) {
            if(target.gameObject.name == "Main Bullet") GameObject.FindGameObjectWithTag("Player").GetComponent<GunController>().bullet = target;
        }
    }

    public void ShowCanvas(string scriptToLoad) {
        canvasActive = true;
        scriptName.text = scriptToLoad;
        Cursor.visible = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<InteractController>().isInteracting = true;
        crosshair.SetActive(false);
        compileButton.SetActive(true);

        if (canvasActive) {
            player.GetComponent<FirstPersonController>().enabled = false;

            foreach (Component component in player.GetComponents(typeof(Component))) {
                if (component.GetType().ToString() == "GunController") {
                    Type t = component.GetType();
                    (player.GetComponent(t) as MonoBehaviour).enabled = false;
                }
            }

            textArea.SetActive(true);
            var files = Directory.GetFiles(Application.streamingAssetsPath, "*", SearchOption.AllDirectories);
            foreach (var file in files) {
                if (file.EndsWith(scriptToLoad)) {
                    GameObject.FindGameObjectWithTag("Script Input").GetComponent<TMP_InputField>().text = File.ReadAllText(file);
                }
            }
        }

        switch(scriptToLoad) {
            case "GunAddOn.cs":
                target = GameObject.FindGameObjectWithTag("Player");
                break;

            case "BulletAddOn.cs":
                target = GameObject.Find("Main Bullet");
                break;

            default:
                target = null;
                break;
        }
    }

    public void HideCanvas() {
        canvasActive = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FirstPersonController>().enabled = true;
        crosshair.SetActive(true);
        compileButton.SetActive(false);
        
        foreach(var component in player.GetComponents(typeof(Component))) {
            if(component.GetType().ToString() == "GunController" || component.GetType().ToString() == "GunAddOn") {
                Type t = component.GetType();
                (player.GetComponent(t) as MonoBehaviour).enabled = true;
            }
        }

        textArea.SetActive(false);
        Cursor.visible = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<InteractController>().isInteracting = false;
    }
}
