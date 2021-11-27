using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTriggerZoomShift : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera playerVirtualCamera;
    [SerializeField]
    public float cameraOrthographicStartSize = 5f;  // Default is 5.0f;
    [SerializeField]
    public float cameraOrthographicEndSize = 10f;

    // Variables for smooth camera transitions
    [SerializeField]
    public int interpolationFramesCount = 90; // Number of frames to completely interpolate between the 2 positions
    private int elapsedFrames = 0;

    private bool transitionTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            cameraOrthographicStartSize = playerVirtualCamera.m_Lens.OrthographicSize;
            transitionTriggered = true;
        }
    }
    private void FixedUpdate()
    {
        if(transitionTriggered)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
            elapsedFrames++;
            playerVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(cameraOrthographicStartSize, cameraOrthographicEndSize, interpolationRatio);

            if(elapsedFrames > interpolationFramesCount)
            {
                transitionTriggered = false;
                elapsedFrames = 0;
            }
        }
    }
}
