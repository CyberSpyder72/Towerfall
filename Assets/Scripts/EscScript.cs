using UnityEngine;
using UnityEngine.InputSystem;

public class EscScript : MonoBehaviour
{
    public Texture2D cursor;

    public InputAction escInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        escInput.Enable();
        Cursor.visible = true;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
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
