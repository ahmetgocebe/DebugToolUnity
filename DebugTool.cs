using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTool : MonoBehaviour
{
    public Text text;
    public GameObject panel;
    public void Toggle()
    {
        if(panel != null)
        panel.SetActive(!panel.activeSelf);
    }
    public void Clear()
    {
        text.text = " ";
    }
    private void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }
    void OnEnable()
    {
        Application.logMessageReceived += LogCallback;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogCallback;
    }
    private string lastText = " ";
    void LogCallback(string logString, string stackTrace, LogType type)
    {
        if (text.text.Length > 16000) { text.text = " "; }
        if (logString != lastText)
        {
            switch (type)
            {
                case LogType.Error:
                    logString = "<color=red>" + logString + "</color>";
                    break;
                case LogType.Assert:
                    return;
                case LogType.Warning:
                    logString = "<color=yellow>" + logString + "</color>";
                    return;
                case LogType.Log:
                    logString = "<color=black>" + logString + "</color>";

                    break;
                case LogType.Exception:
                    return;
                default:
                    break;
            }
            text.text += logString + "\\r\\n\n";
            text.text += "--------------------------\n\n";
            lastText = logString;
        }

    }
}