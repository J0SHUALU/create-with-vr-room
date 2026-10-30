using UnityEngine;
using UnityEngine.InputSystem;

public class MenuToggle : MonoBehaviour
{
    [SerializeField] InputActionProperty toggleAction;
    [SerializeField] GameObject menu;
    [SerializeField] AudioSource toggleSound;
    [SerializeField] bool startOpen;

    void Start()
    {
        if (menu != null)
            menu.SetActive(startOpen);
    }

    void OnEnable()
    {
        if (toggleAction.action == null)
            return;
        toggleAction.action.Enable();
        toggleAction.action.performed += HandleToggle;
    }

    void OnDisable()
    {
        if (toggleAction.action != null)
            toggleAction.action.performed -= HandleToggle;
    }

    void HandleToggle(InputAction.CallbackContext context)
    {
        Toggle();
    }

    public void Toggle()
    {
        SetOpen(!menu.activeSelf);
    }

    public void SetOpen(bool open)
    {
        menu.SetActive(open);
        if (toggleSound != null)
            toggleSound.Play();
    }
}
