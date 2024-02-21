using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera salonCamera; // Drag your salon camera here in the Inspector
    public CinemachineVirtualCamera roomCamera;  // Drag your room camera here in the Inspector

    private CinemachineVirtualCamera activeCamera; // To keep track of the currently active camera

    // Start is called before the first frame update
    void Start()
    {
        /* Set the room camera as the default active camera
        Because the player is already into the salon area, Unity considere the trigger
        so we begin with roomCamera to initiate properly to salonCamera */
        SetActiveCamera(roomCamera);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChangeCameraBox"))
        {
            // Toggle between the cameras
            ToggleCameras();
        }
    }

    private void ToggleCameras()
    {
        if (activeCamera == salonCamera)
        {
            // Switch to room camera
            SetActiveCamera(roomCamera);
        }
        else
        {
            // Switch back to salon camera
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
