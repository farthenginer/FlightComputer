using UnityEngine;
using System;

public class AudioEngineUITest : MonoBehaviour
{
    string[] gpwsNames = Enum.GetNames(typeof(AudioPool.GPWS_Sounds));
    string[] tcasNames = Enum.GetNames(typeof(AudioPool.TCAS_Sounds));

    void OnGUI()
    {
        DrawGroup(gpwsNames, 20, "gpws"); //GUI Test Buttons
        DrawGroup(tcasNames, 320,"tcas"); //GUI Test Buttons
    }

    void DrawGroup(string[] names, float startY, string type)
    {

        float scale = Screen.width / 1920f;

        float w = 150 * scale;
        float h = 40 * scale;
        float s = 10 * scale;
        //Scale

        int columns = 5;

        for (int i = 0; i < names.Length; i++)
        {
            int row = i / columns;
            int col = i % columns;

            if (GUI.Button(
                new Rect(
                    60 * scale + col * (w + s),
                    startY * scale + row * (h + s),
                    w,
                    h),
                names[i]))
            {
                Debug.Log("Clicked");   
                
                if (type == "tcas")
                {
                    if (Enum.TryParse<AudioPool.TCAS_Sounds>(names[i], out var sound)) //Enum Check
                    {
                        AudioEngine.audioEngine.PlayTCAS(sound);
                        Debug.Log($"Playing: {sound}");
                    }
                }
                else
                {
                    if (Enum.TryParse<AudioPool.GPWS_Sounds>(names[i], out var sound)) //Enum Check
                    {
                        AudioEngine.audioEngine.PlayGPWS(sound);
                        Debug.Log($"Playing: {sound}");
                    }
                }
            }
        }
    }
}
