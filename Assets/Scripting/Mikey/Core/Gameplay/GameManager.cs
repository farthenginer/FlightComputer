/*
    27.07.2026 - 15:22 Created by Omer Faruk Simsek
*/

using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    #region Core Variables

    [SerializeField] private GuestAircraftFactory _guestFactory;

    #endregion

    //struct
    public enum directions
    {
        south,
        north,
        west,
        east,
        southwest,
        northwest,
        southeast,
        northeast
    }

    protected override void Awake()
    {
        base.Awake();
        //initialize
    }

    public void CreateAircraft(GuestAircraftInitData data, Vector2 position)
    {
        _guestFactory.Spawn(data, position);
    }
}
