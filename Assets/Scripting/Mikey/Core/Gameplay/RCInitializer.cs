using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RCInitializer : MonoBehaviour
{
    [SerializeField] private Button addButton;
    [SerializeField] private Button removeButton;
    
    public void Initialize(UnityAction addButtonEvent, UnityAction removeButtonEvent) 
    {
        //initializer
        addButton.onClick.AddListener(addButtonEvent);
        removeButton.onClick.AddListener(removeButtonEvent);
    }
}
