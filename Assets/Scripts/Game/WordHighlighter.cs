using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class WordHighlighter : MonoBehaviour
{
    GameObject text;

    string[] blueNames = new string[] { "int ",
                                    "float ",
                                    "double ",
                                    "string ",
                                    "void ",
                                    "private ",
                                    "public ",
                                    "new ",
                                    "using ",
                                    "bool ",
                                    "true ",
                                    "false ",
                                    "if ",
                                    "else ",
                                    "foreach ",
                                    "for",
                                    "typeof ",
                                    "is ",
                                    "return ",
                                    "var ",
                                    "try ",
                                    "class ",
                                    "Start",
                                    "Update",
                                    "catch "};

    private void Start() {
        text = GameObject.FindGameObjectWithTag("Script Input");
    }

    public void Highlight() {
        string toB = text.GetComponent<TMP_InputField>().text;
        toB = Regex.Replace(toB, "<[^>]*>", "", RegexOptions.Singleline);
        toB = Regex.Replace(toB, "\\\"(.*?)\\\"", "Testing</color>", RegexOptions.Singleline);

        for(int i = 0; i < blueNames.Length; i++)
            toB = Regex.Replace(toB, blueNames[i], "<color=#0000ffff>" + blueNames[i] + "</color>", RegexOptions.Singleline);
        Debug.Log("Changing...");
        text.GetComponent<TMP_InputField>().text = toB;
    }
}
