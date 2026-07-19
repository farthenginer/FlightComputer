/*
    19.07.2026 - 20:46 Created by Omer Faruk Simsek
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TCASAircraftModel : MonoBehaviour
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

    #region Core Variables
    
    //Visualization
    public threatLevel _threatLevel;
    public actionStatus _actionStatus;
    public int _actionValue;

    #endregion

    #region UI Variables
    
    //Object References
    [SerializeField] private Image _threatImg;
    [SerializeField] private Image _actionStatusImg;

    [SerializeField] private TMP_Text _actionValueText;

    //Asset References
    [HideInInspector] public List<Sprite> _threatSprites;
    [HideInInspector] public List<Sprite> _statusSprites;

    #endregion

    public void SetThreat(threatLevel _threat)
    {
        switch (_threat)
        {
            case threatLevel.noThreat:
                //_threatImg.sprite = 
                break;
            case threatLevel.proximateThreat:
                break;
            case threatLevel.potentialThreat:
                break;
            case threatLevel.collisionThreat:
                break;
        }
    }
}
