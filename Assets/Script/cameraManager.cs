using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera salonCamera;
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

    private void SetActiveCamera(CinemachineVirtualCamera newActiveCamera)
    {
        // Disable the current active virtual camera
        if (activeCamera != null)
        {
            activeCamera.gameObject.SetActive(false);
        }

        // Enable the new active virtual camera
        newActiveCamera.gameObject.SetActive(true);

        // Update the activeCamera reference
        activeCamera = newActiveCamera;
    }
}
