using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class FollowMouse : MonoBehaviour
{
    void OnEnable()
    {
        Cursor.visible = false;
    }

    void OnDisable()
    {
        Cursor.visible = true;
    }

	void Update ()
    {
        // this works with canvases which are set to "Screen Space - Overlay" only.
        transform.position = Mouse.current.position.ReadValue();
	}
}
