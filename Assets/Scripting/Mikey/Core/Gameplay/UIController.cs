/*
    26.07.2026 - 22:40 Created by Omer Faruk Simsek
*/

using UnityEngine;

public class UIController : MonoBehaviour
{
    #region UI Variables
    
    [SerializeField] private GameObject _rcCanvasPrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 _additionalCanvasOffset;
    #endregion

    #region Canvas Variables
    
    private bool _canvasIsOpen;
    private GameObject _cachedCanvas;

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
    private void RightCanvasCall(Vector2 position)
    {
        if (_canvasIsOpen)
        {
            Destroy(_cachedCanvas);
            _canvasIsOpen = false;
        }

        var newCanvas = Instantiate(_rcCanvasPrefab, (position + _additionalCanvasOffset), Quaternion.identity);
        newCanvas.GetComponent<Canvas>().worldCamera = _camera;
        newCanvas.GetComponent<RCInitializer>().Initialize(AddButtonPressed, RemoveButtonPressed);

        _cachedCanvas = newCanvas;
        _canvasIsOpen = true;
    }

    #region Buttons
    private void AddButtonPressed()
    {
        //destroy canvas
        Destroy(_cachedCanvas);
        _canvasIsOpen = false;

        //startup
    }
    private void RemoveButtonPressed()
    {
        //destroy canvas
        Destroy(_cachedCanvas);
        _canvasIsOpen = false;

        //startup
    }
    #endregion
}
