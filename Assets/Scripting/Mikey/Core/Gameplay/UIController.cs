/*
    26.07.2026 - 22:40 Created by Omer Faruk Simsek
*/

using UnityEngine;

public class UIController : Singleton<UIController>
{
    #region UI Variables
    
    [SerializeField] private GameObject _rcCanvasPrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _additionalCanvasOffset;

    [SerializeField] private GameObject _addCanvasPrefab;

    #endregion

    #region Canvas Variables
    
    private bool _canvasIsOpen;
    public bool _canvasPermission = true;
    private GameObject _cachedCanvas;

    Vector2 _cachedRCPosition;

    #endregion

    void Update()
    {
        RightClickCanvas();
    }

    private void RightClickCanvas()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 _position = _camera.ScreenToWorldPoint(Input.mousePosition);
            RightCanvasCall(_position);
        }
    }

    /// <summary>
    /// Create activity menu with right click.
    /// </summary>
    /// <param name="position">Guest aircraft reference position</param> 
    private void RightCanvasCall(Vector2 position)
    {
        if (_canvasIsOpen)
        {
            Destroy(_cachedCanvas);
            _canvasIsOpen = false;
        }
        if (!_canvasPermission) //Turn on from AC
            return;

        var newCanvas = Instantiate(_rcCanvasPrefab, (position + _additionalCanvasOffset), Quaternion.identity);
        newCanvas.GetComponent<Canvas>().worldCamera = _camera;
        newCanvas.GetComponent<RCInitializer>().Initialize(AddButtonPressed, RemoveButtonPressed);

        _cachedRCPosition = position;
        _cachedCanvas = newCanvas;
        _canvasIsOpen = true;
        _canvasPermission = true;

        try
        {
            Invoke(nameof(DestroyRcCanvas), 3);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    private void DestroyRcCanvas()
    {
        CancelInvoke(nameof(DestroyRcCanvas));

        Destroy(_cachedCanvas);
        _canvasIsOpen = false;
    }

    #region Buttons

    private void AddButtonPressed()
    {
        DestroyRcCanvas(); //destroy
        _canvasPermission = false;

        CreateAircraftPopUp caPopup =  Instantiate(_addCanvasPrefab, null).GetComponent<CreateAircraftPopUp>();
        caPopup.Initialize(_cachedRCPosition);
    }

    private void RemoveButtonPressed()
    {
        DestroyRcCanvas(); //destroy

        //Remove//
    }

    #endregion

    public void CloseAddCanvas(GameObject canvas)
    {
        Destroy(canvas);
        _canvasPermission = true;
    }
}
