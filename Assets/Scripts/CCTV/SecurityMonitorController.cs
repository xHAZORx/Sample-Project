using UnityEngine;

public class SecurityMonitorController : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject cctvUI;
    [SerializeField] MonoBehaviour mouseLook;
    [SerializeField] GameObject interactPrompt;

    private bool usingMonitor = false;

    public void Interact()
    {
        if (!usingMonitor)
        {
            OpenMonitor();
        }
    }

    private void Update()
    {
        if (usingMonitor && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMonitor();
        }
    }

    private void OpenMonitor()
{
    usingMonitor = true;
    PlayerInteraction.IsUsingMonitor = true;

    cctvUI.SetActive(true);

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}

    private void CloseMonitor()
    {
    usingMonitor = false;
    PlayerInteraction.IsUsingMonitor = false;

    cctvUI.SetActive(false);

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    }
}