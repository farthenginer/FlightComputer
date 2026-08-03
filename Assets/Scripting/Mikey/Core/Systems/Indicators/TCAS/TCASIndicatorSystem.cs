/*
    21.07.2026 - 21:07 Created by Omer Faruk Simsek
*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TCASIndicatorSystem : Singleton<TCASIndicatorSystem>
{

    public List<Sprite> _threatSpriteList;
    public List<Sprite> _actionSpriteList;

    [SerializeField] private GameObject _TCASMapReference;
    [SerializeField] private GameObject _TCASObjectReference;


    private void Start()
    {
        /*
        CreateNode(new TCASConfiguration.AircraftInfo
        {
            altitude = 2000, //for demo,
            action = TCASConfiguration.actionStatus.climb,
            threatLevel = TCASConfiguration.threatLevel.potentialThreat
        });*/
    }

    private Dictionary<GuestAircraft, TCASAircraftModel> _activeNodes = new();

    public void UpdateOrCreateNode(GuestAircraft source, TCASConfiguration.AircraftInfo info)
    {
        if (_activeNodes.TryGetValue(source, out var existingNode))
        {
            existingNode.Initialize(info);
        }
        else
        {
            var nodeObj = Instantiate(_TCASObjectReference, _TCASMapReference.transform);
            var model = nodeObj.GetComponent<TCASAircraftModel>();
            model.Initialize(info);
            _activeNodes[source] = model;
        }

        Vector3 targetPos = new Vector3(info.relativePosition.x, 0f, info.relativePosition.y);
        _activeNodes[source].transform.localPosition = targetPos;
    }

    public void RemoveStaleNodes(List<GuestAircraft> stillInRange)
    {
        var toRemove = _activeNodes.Keys.Where(k => !stillInRange.Contains(k)).ToList();
        foreach (var key in toRemove)
        {
            Destroy(_activeNodes[key].gameObject);
            _activeNodes.Remove(key);
        }
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
        public Vector2 relativePosition;
    }
}
