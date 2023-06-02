using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[RequireComponent(typeof(SphereCollider),typeof(Rigidbody))]
public class HandUIController : MonoBehaviour
{

    [SerializeField]
    private InputActionProperty triggerAction;

    private UnityEvent _event;

    private void OnEnable()
    {
        triggerAction.action.Enable();

    }

    private void OnDisable()
    {
        triggerAction.action.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractiveUI interactiveUI;
        if (other.CompareTag("InteractiveUI"))
        {
            interactiveUI = other.GetComponent<IInteractiveUI>();
            interactiveUI.HighlightUIObject();
            ListenForTriggerButton(interactiveUI.GetUnityEvent());
        }
        else
        {
            interactiveUI = null;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IInteractiveUI interactiveUI;
        if (other.CompareTag("InteractiveUI"))
        {
            interactiveUI = other.GetComponent<IInteractiveUI>();
            interactiveUI.HighlightUIObject();
            CancelListenForTriggerButton();
        }
        else
        {
            interactiveUI = null;
        }
    }

    public void ListenForTriggerButton(UnityEvent unityEvent)
    {
        triggerAction.action.performed += OnTriggerButtonPressed;
        _event = unityEvent;

    }

    public void CancelListenForTriggerButton()
    {
        triggerAction.action.performed -= OnTriggerButtonPressed;

    }

    private void OnTriggerButtonPressed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {

            _event.Invoke();
        }
    }
}
