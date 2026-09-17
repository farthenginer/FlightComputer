/*
    17.09.2026 - 23:07 Created by Omer Faruk Simsek
*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mikey.Test;


public class MikeyDebug : MonoBehaviour
{
    public MikeyDebugger mikeyDebuggerPrefab;

    void Awake()
    {
        if(MikeyDebugger.Instance == null)
        {
            Transform mainCanvas = FindFirstObjectByType<Canvas>().transform;
            Instantiate(mikeyDebuggerPrefab, mainCanvas);
        }
    }
    public static void Log(string log, bool isImportant)
    {
        MikeyDebugger.Instance.Write(MikeyDebugger.DebugType.Message, log, isImportant);
    }

    public static void LogWarning(string log, bool isImportant)
    {
        MikeyDebugger.Instance.Write(MikeyDebugger.DebugType.Warning, log, isImportant);
    }

    public static void LogData(string log, bool isImportant)
    {
        MikeyDebugger.Instance.Write(MikeyDebugger.DebugType.Data, log, isImportant);
    }

    public static void LogError(string log, bool isImportant)
    {
        MikeyDebugger.Instance.Write(MikeyDebugger.DebugType.Error, log, isImportant);
    }
}