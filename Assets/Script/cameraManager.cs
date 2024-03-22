using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Tooltip("Drag your salonCamera here")]
    [SerializeField] CinemachineVirtualCamera salonCamera;

    [Tooltip("Drag your roomCamera here")]
    [SerializeField] CinemachineVirtualCamera roomCamera;

    private CinemachineVirtualCamera activeCamera;

    /// <summary>
    /// Set the roomCamera as the default active camera
    /// Because the player is already into the salon area, Unity considere the trigger
    /// so we begin with roomCamera to initiate properly to salonCamera
    /// </summary>
    void Start()
    {
        SetActiveCamera(roomCamera);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChangeCameraBox"))
        {
            ToggleCameras();
        }
    }

    private void ToggleCameras()
    {
        if (activeCamera == salonCamera)
        {
            SetActiveCamera(roomCamera);
        }
        else
        {
            SetActiveCamera(salonCamera);
        }
    }

    /// <summary>
    /// Update the Camera priority to set the new active camera
    /// </summary>
    /// <param name="newActiveCamera"></param>
    private void SetActiveCamera(CinemachineVirtualCamera newActiveCamera)
    {
        if (activeCamera != null)
        {
            activeCamera.Priority = 10;
        }

        newActiveCamera.Priority = 100;

        activeCamera = newActiveCamera;
    }
}
