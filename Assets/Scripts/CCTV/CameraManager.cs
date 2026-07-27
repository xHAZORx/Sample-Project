using TMPro;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("CCTV Cameras")]
    [SerializeField] private Camera[] cameras;

    [Header("Render Texture")]
    [SerializeField] RenderTexture renderTexture;

    [Header("UI")]
    [SerializeField] TMP_Text cameraNameText;

    private int currentCameraIndex = 0;

    private void Start()
    {
        UpdateCamera();
    }

    public void NextCamera()
    {
        currentCameraIndex++;

        if (currentCameraIndex >= cameras.Length)
        {
            currentCameraIndex = 0;
        }

        UpdateCamera();
    }

    public void PreviousCamera()
    {
        currentCameraIndex--;

        if (currentCameraIndex < 0)
        {
            currentCameraIndex = cameras.Length - 1;
        }

        UpdateCamera();
    }

    private void UpdateCamera()
    {
        // Remove Render Texture from every camera
        foreach (Camera camera in cameras)
        {
            camera.targetTexture = null;
        }

        // Assign Render Texture to selected camera
        cameras[currentCameraIndex].targetTexture = renderTexture;

        // Update UI
        cameraNameText.text = cameras[currentCameraIndex].name;
    }
}