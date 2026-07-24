/*
    21.07.2026 - 21:07 Created by Omer Faruk Simsek
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TCASIndicatorSystem : MonoBehaviour
{

    public static TCASIndicatorSystem instance; //for instance

    public List<Sprite> _threatSpriteList;
    public List<Sprite> _actionSpriteList;

    [SerializeField] private GameObject _TCASMapReference;
    [SerializeField] private GameObject _TCASObjectReference;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateNode(new TCASConfiguration.AircraftInfo
        {
            altitude = 2000, //for demo,
            action = TCASConfiguration.actionStatus.climb,
            threatLevel = TCASConfiguration.threatLevel.potentialThreat
        });
    }

    public void CreateNode(TCASConfiguration.AircraftInfo _aircraftInfo)
    {
        var _tcasNode = GameObject.Instantiate(_TCASObjectReference, _TCASMapReference.transform);
        _tcasNode.transform.localPosition = Vector3.zero;
        _tcasNode.GetComponent<TCASAircraftModel>().Initialize(_aircraftInfo);
    }
}

[System.Serializable]
public class TCASConfiguration
{
    public enum threatLevel
    {
        noThreat,
        proximateThreat,
        potentialThreat,
        collisionThreat
    }

    public enum actionStatus
    {
        none,
        climb,
        descend
    }

    public struct AircraftInfo
    {
        public float altitude;
        public actionStatus action;
        public threatLevel threatLevel;
    }
}
