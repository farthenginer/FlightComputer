/*
    27.07.2026 - 16:35 Created by Omer Faruk Simsek
*/

using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public struct GuestAircraftInitData
{
    public string aircraftName;
    public float altitudeOffset;
    public GameManager.directions direction;

    public GuestAircraftInitData(string aircraftName, float altitudeOffset, GameManager.directions direction)
    {
        this.aircraftName = aircraftName;
        this.altitudeOffset = altitudeOffset;
        this.direction = direction;
    }
}

public class GuestAircraft : MonoBehaviour
{
    public float stepSize;
    public float stepDelay;
    public float Altitude;

    public string Name;

    public GuestAircraftInitData _data;
    public bool initialized;
    public bool movePermission;

    public GameManager.directions Direction;

    [SerializeField] private CanvasGroup _canvas;

    public void Initialize(GuestAircraftInitData initData)
    {
        if (initialized)
            return;

        Name = initData.aircraftName;
        Altitude = initData.altitudeOffset;
        Direction = initData.direction;

        _data = initData;
        initialized = true;

        stepSize = 1;
        stepDelay = 1;
        StartCoroutine(Movement());
    }

    #region events
    
    private void OnMouseEnter()
    {
        Debug.Log("E");
        Show(true);
    }
    private void OnMouseExit()
    {
        Debug.Log("X");
        Show(false);
    }

    private void Show(bool value)
    {
        StartCoroutine(ShowRoutine(value));
    }
    IEnumerator ShowRoutine(bool value)
    {
        float _current = 0;
        float _time = 0.5f;

        yield return new WaitForSeconds(.5f);
        
        while (_current < _time)
        {
            _current += Time.deltaTime;

            if (value)
            {
                _canvas.alpha = Mathf.Lerp(_canvas.alpha, 1, _current / _time);
            }
            else
            {
                _canvas.alpha = Mathf.Lerp(_canvas.alpha, 0, _current / _time);
            }
            yield return null;
        }
    }

    #endregion
    
    IEnumerator Movement()
    {
        while (true)
        {
            transform.position += transform.up * stepSize;
            yield return new WaitForSeconds(stepDelay);
        }
    }
}