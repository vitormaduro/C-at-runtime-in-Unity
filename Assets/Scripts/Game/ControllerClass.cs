using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerClass: MonoBehaviour
{
    TMP_Text playerText;

    public void UpdateText(string textToUpdate) {
        playerText = GameObject.Find("Character Text").GetComponent<TMP_Text>();
        playerText.text = textToUpdate;
    }
}
