using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactDistance = 3f;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject interactPrompt;

    private IInteractable currentInteractable;

    void Update()
    {
        CheckForInteractable();

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

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
