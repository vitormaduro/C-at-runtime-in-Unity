using Microsoft.CSharp;
using System;
using System.IO;
using System.CodeDom.Compiler;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CSharpCompiler;
using System.Collections;
using UnityEngine.Networking;

public class Compiler : MonoBehaviour {

    public TextMeshProUGUI consoleOutput;
    public TMP_InputField input;

    DeferredSynchronizeInvoke synchronizedInvoke;
    ScriptBundleLoader loader;

    private void Start() {
        consoleOutput = GameObject.FindGameObjectWithTag("Console Output").GetComponent<TextMeshProUGUI>();
        input = GameObject.FindGameObjectWithTag("Script Input").GetComponent<TMP_InputField>();
    }

    public void Compile(GameObject textArea, string scriptName, GameObject attachTarget) {
        File.WriteAllText(Application.streamingAssetsPath + "/" + scriptName, input.text);
        consoleOutput.text = "";

        synchronizedInvoke = new DeferredSynchronizeInvoke();

        loader = new ScriptBundleLoader(synchronizedInvoke) {
            logWriter = new UnityLogTextWriter(),
            createInstance = (Type t) => {
                if (typeof(Component).IsAssignableFrom(t)) {
                    string scriptToDelete = "";

                    switch (attachTarget.gameObject.tag) {
                        case "Player":
                            scriptToDelete = "GunAddOn";
                            break;

                        case "Bullet":
                            scriptToDelete = "BulletAddOn";
                            break;
                    }

                    foreach (Component component in attachTarget.GetComponents(typeof(Component))) {
                        if (component.GetType().ToString() == scriptToDelete) {
                            Destroy(attachTarget.GetComponent(component.GetType()));
                        }
                    }

                    return attachTarget.AddComponent(t);
                }
                else return Activator.CreateInstance(t);
            },
            destroyInstance = (object instance) => {
                if (instance is Component) Destroy(instance as Component);
            }
        };

        try {
            var sourceFolder = Application.streamingAssetsPath;
            var files = Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories);
            foreach (var file in files) {
                if (file.EndsWith(scriptName)) {
                    loader.LoadAndWatchScriptsBundle(new[] { file });
                }
            }
            consoleOutput.text = "Compile successful";
        }
        catch(Exception e) {
            consoleOutput.text = e.ToString();
        }

        synchronizedInvoke = new DeferredSynchronizeInvoke();
    }
}