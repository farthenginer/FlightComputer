/*
    28.07.2026 - 16:59 Created by Omer Faruk Simsek
*/

//Aircraft Core Script from Mikey

using UnityEngine;
using UnityEngine.Events;


public class Aircraft : MonoBehaviour
{
    public UnityEvent TCASDetection;

    private void Start()
    {
        TCASDetection.Invoke();
    }
}

