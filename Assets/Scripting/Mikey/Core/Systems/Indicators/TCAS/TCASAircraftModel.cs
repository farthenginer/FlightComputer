/*
    19.07.2026 - 20:46 Created by Omer Faruk Simsek
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TCASAircraftModel : MonoBehaviour
{
    #region Core Variables
    
    //Visualization
    public TCASConfiguration.threatLevel _threatLevel;
    public TCASConfiguration.actionStatus _actionStatus;
    public int _actionValue;

    #endregion

    #region UI Variables
    
    //Object References
    [SerializeField] private SpriteRenderer _threatSprite;
    [SerializeField] private Image _actionStatusImg;
    [SerializeField] private TMP_Text _actionValueText;

    #endregion

    public void Initialize(TCASConfiguration.AircraftInfo _config)
    {
        switch (_config.threatLevel)
        {
            case TCASConfiguration.threatLevel.noThreat:
                _threatSprite.sprite = TCASIndicatorSystem.instance._threatSpriteList[0];
                break;
            case TCASConfiguration.threatLevel.proximateThreat:
                _threatSprite.sprite = TCASIndicatorSystem.instance._threatSpriteList[1];
                break;
            case TCASConfiguration.threatLevel.potentialThreat:
                _threatSprite.sprite = TCASIndicatorSystem.instance._threatSpriteList[2];
                break;
            case TCASConfiguration.threatLevel.collisionThreat:
                _threatSprite.sprite = TCASIndicatorSystem.instance._threatSpriteList[3];   
                break;
        }
        _actionStatusImg.sprite = GetActionSprite(_config.action, _config.threatLevel);
        _actionValue = (int)_config.altitude;
        _actionValueText.text = GetAltitudeDifference(_actionValue);
    }

    public Sprite GetActionSprite(TCASConfiguration.actionStatus _status, TCASConfiguration.threatLevel _level)
    {
        if (_status == TCASConfiguration.actionStatus.none)
            return null;

        List<Sprite> _actionList = TCASIndicatorSystem.instance._actionSpriteList;

        switch (_level)
        {
            case TCASConfiguration.threatLevel.noThreat:
                //null
            case TCASConfiguration.threatLevel.proximateThreat:
                return _status == TCASConfiguration.actionStatus.climb ? _actionList[0] : _actionList[1];

            case TCASConfiguration.threatLevel.potentialThreat:
                return _status == TCASConfiguration.actionStatus.climb ? _actionList[2] : _actionList[3];

            case TCASConfiguration.threatLevel.collisionThreat:
                return _status == TCASConfiguration.actionStatus.climb ? _actionList[4] : _actionList[5];
        }

        return null;
    }

    public string GetAltitudeDifference(int value)
    {
        char[] _integerValue = value.ToString().ToCharArray();
        string _value = value >= 0 ? $"+{_integerValue[0]}{_integerValue[1]}" : $"-{_integerValue[0]}{_integerValue[1]}";
        return _value;
    }
}
