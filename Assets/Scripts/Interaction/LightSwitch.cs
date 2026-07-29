using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] Light roomLight;

    public void Interact()
    {
        roomLight.enabled = !roomLight.enabled;
    }
}