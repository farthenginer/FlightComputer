/*
    17.09.2026 - 21:57 Created by Omer Faruk Simsek
*/

using UnityEngine;
using TMPro;

namespace Mikey.Test
{
    public class MikeyDebugger : Singleton<MikeyDebugger>
    {
        public Transform context;

        [SerializeField] GameObject debugLabelPrefab;
        [SerializeField] DebugTheme debugTheme;


        public void Write(DebugType type, string log, bool isImportant)
        {
            TMP_Text debugLabel = Instantiate(debugLabelPrefab, context).GetComponent<TMP_Text>();

            debugLabel.text = log;
            debugLabel.fontStyle = isImportant ? FontStyles.Bold : FontStyles.Normal;

            switch(type)
            {
                case DebugType.Message:
                debugLabel.color = debugTheme.messageColor;
                break;
                case DebugType.Warning:
                debugLabel.color = debugTheme.warningColor;
                break;
                case DebugType.Data:
                debugLabel.color = debugTheme.dataColor;
                break;
                case DebugType.Error:
                debugLabel.color = debugTheme.errorColor;
                break;
            }
        }

        public enum DebugType //types
        {
            Message,
            Warning,
            Data,
            Error
        }
    }

    [System.Serializable]
    public class DebugTheme
    {
        public Color messageColor;
        public Color warningColor;
        public Color dataColor;
        public Color errorColor;
    }
}