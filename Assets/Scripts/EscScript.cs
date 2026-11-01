using UnityEngine;
using UnityEngine.InputSystem;

public class EscScript : MonoBehaviour
{
    public InputAction escInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        escInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (escInput.WasPressedThisFrame())
        {
            Debug.Log("quitting");
            Application.Quit();
        }
    }
}
