
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public static bool IsUsingMonitor = false;

    [SerializeField] float interactDistance = 3f;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject interactPrompt;
    [SerializeField] InputActionReference interactAction;

    private IInteractable currentInteractable;

    private void OnEnable()
    {
        if (interactAction != null)
            interactAction.action.Enable();
    }

    private void OnDisable()
    {
        if (interactAction != null)
            interactAction.action.Disable();
    }

    private void Update()
    {
        if (IsUsingMonitor)
        {
            interactPrompt.SetActive(false);
            currentInteractable = null;
            return;
        }

        CheckForInteractable();

        if (interactAction != null &&
            interactAction.action.WasPressedThisFrame() &&
            currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            IInteractable interactable =
                hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                currentInteractable = interactable;
                interactPrompt.SetActive(true);
                return;
            }
        }

        currentInteractable = null;
        interactPrompt.SetActive(false);
    }
}
