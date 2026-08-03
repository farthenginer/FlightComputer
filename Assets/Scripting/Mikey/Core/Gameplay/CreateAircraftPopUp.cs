/*
    27.07.2026 - 15:15 Created by Omer Faruk Simsek
*/

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class CreateAircraftPopUp : MonoBehaviour
{
    #region Core Variables

    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private TMP_InputField _altitudeField;
    [SerializeField] private TMP_Dropdown _directionValue;
    [SerializeField] private Button _createButton;

    Vector2 _cachedPosition;

    #endregion

    public void Initialize(Vector2 position)
    {
        _createButton.onClick.AddListener(() => Submit()); //Add listener a button
        _cachedPosition = position;
    }

    private void Submit()
    {
        //string to enum (Parse)
        GameManager.directions _direction = (GameManager.directions)_directionValue.value;

        GuestAircraftInitData _guestAircraft = new GuestAircraftInitData
        {
            aircraftName = _nameField.text,
            altitudeOffset = int.Parse(_altitudeField.text), //force casting. Because Input field is numeric.
            direction = _direction
        };

        GameManager.Instance.CreateAircraft(_guestAircraft, _cachedPosition);
        ClosePopUp();
    }

    private void ClosePopUp()
    {
        UIController.Instance.CloseAddCanvas(gameObject);
    }
}
