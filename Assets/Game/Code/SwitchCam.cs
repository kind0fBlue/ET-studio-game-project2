using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SwitchCam : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private int priorityBoostAmount = 10;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private InputAction aimAction;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        cinemachineVirtualCamera.Priority += priorityBoostAmount;
    }

    private void CancelAim()
    {
        cinemachineVirtualCamera.Priority -= priorityBoostAmount;
    }
}
