/*
    27.07.2026 - 15:22 Created by Omer Faruk Simsek
*/

using UnityEngine;

using Mikey.Test;

public class GameManager : Singleton<GameManager>
{
    #region Core Variables

    [SerializeField] private GuestAircraftFactory _guestFactory;

    [SerializeField] private MikeyDebug _mikeyDebug;

    [SerializeField] private GameObject _mikeyDebugPrefab;

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
        _mikeyDebug = FindFirstObjectByType<MikeyDebug>();

        if(_mikeyDebug == null)
        {
            _mikeyDebug = Instantiate(_mikeyDebugPrefab).GetComponent<MikeyDebug>();
        }
    }

    void Start()
    {
        //Demo logs :)
        MikeyDebug.Log("First Log! 1234 @", false);
        MikeyDebug.LogData($"CPU Threads: {SystemInfo.processorCount}", false);
        MikeyDebug.LogData($"CPU: {SystemInfo.processorType}", false);
        MikeyDebug.LogData($"GPU: {SystemInfo.graphicsDeviceName}", false);
        MikeyDebug.LogData($"GPU Memory: {SystemInfo.graphicsMemorySize} MB", false);
        MikeyDebug.LogData($"System RAM: {SystemInfo.systemMemorySize} MB", false);
        MikeyDebug.LogData($"OS: {SystemInfo.operatingSystem}", false);
        MikeyDebug.LogData($"Unity Version: {Application.unityVersion}", false);
        MikeyDebug.LogData($"Screen: {Screen.width}x{Screen.height}", false);
        MikeyDebug.LogData($"Refresh Rate: {Screen.currentResolution.refreshRateRatio.value:F0} Hz",false);
        MikeyDebug.LogData($"Target FPS: {Application.targetFrameRate}", false);
        MikeyDebug.LogData($"Platform: {Application.platform}", false);
        MikeyDebug.LogData($"Graphics API: {SystemInfo.graphicsDeviceType}", false);
        MikeyDebug.LogData($"Graphics Shader Level: {SystemInfo.graphicsShaderLevel}",false);
        MikeyDebug.LogData($"Device: {SystemInfo.deviceModel}", true);
    }

    public void CreateAircraft(GuestAircraftInitData data, Vector2 position)
    {
        _guestFactory.Spawn(data, position);
    }
}