/*
    27.07.2026 - 15:22 Created by Omer Faruk Simsek
*/

using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //instance
        }
        else
        {
            Destroy(instance);
            instance = this;
        }
    }

    public void CreateAircraft(GuestAircraftInitData data, Vector2 position)
    {
        _guestFactory.Spawn(data, position);
    }
}
