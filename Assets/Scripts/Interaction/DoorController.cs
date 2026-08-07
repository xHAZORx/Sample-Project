using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public float openAngle = 90f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openedRotation;

    void Start()
    {
        closedRotation = transform.rotation;

        openedRotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + openAngle,
            transform.eulerAngles.z);
    }

    public void Interact()
    {
        ToggleDoor();
    }

    public void ToggleDoor()
    {
        if (isOpen)
            transform.rotation = closedRotation;
        else
            transform.rotation = openedRotation;

        isOpen = !isOpen;
    }
}