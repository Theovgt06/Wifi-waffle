
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 screenPosition;
    public Vector2 worldPosition;
    public Texture2D cursorTexture;
    void Start()
    {
        Vector2 hotspot = new Vector2(cursorTexture.height/2,cursorTexture.width/2);
        Cursor.visible = true;
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        transform.position = worldPosition;
    }
}
