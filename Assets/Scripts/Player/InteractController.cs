using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Luminosity.IO;

public class InteractController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI message;
    public bool isInteracting = false;

    new Camera camera;
    bool isTurretDeployed = false;

    private void Start() {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        message = GameObject.Find("Interact Message").GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update() {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(new Vector3((Screen.width) / 2, (Screen.height) / 2));

        if (InputManager.GetAction(0, "Interact").AnyInput && !isInteracting) {
            if(Physics.Raycast(ray, out hit, 2f)) {
                if(hit.collider.gameObject.name.Contains("[INT]")) {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<CanvasController>().ShowCanvas(hit.collider.gameObject.tag);
                }
            }
        }

        if (InputManager.GetAction(0, "Deploy Turret").AnyInput && !isInteracting) {
            if (Physics.Raycast(ray, out hit, 10f) && !isTurretDeployed && hit.distance > 3f) {
                Instantiate(Resources.Load("Prefabs/Turret"), hit.point, Quaternion.identity);
                isTurretDeployed = true;
            }

            Debug.DrawLine(gameObject.transform.position, hit.point, Color.green, 10f);
        }

        if (Physics.Raycast(ray, out hit, 2f)) {
            if(hit.collider.gameObject.name.Contains("[INT]") && !isInteracting) {
                message.enabled = true;
                message.text = "Aperte " + InputManager.GetAction(0, "Interact").GetBinding(0).Positive + " para interagir";
            }
            else {
                message.enabled = false;
            }
        }
        else {
            message.enabled = false;
        }
    }
}
