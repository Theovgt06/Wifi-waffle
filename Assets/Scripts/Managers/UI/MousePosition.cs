using UnityEngine;

public class MousePosition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 screenPosition;
    public Vector2 worldPosition;
    void Start()
    {
        Cursor.visible = false;
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
