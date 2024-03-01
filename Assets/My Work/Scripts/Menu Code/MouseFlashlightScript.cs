using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFlashlightScript : MonoBehaviour
{
    Vector3 MousePosition;
    void Start()
    {
        FollowMouse();
    }

    void Update()
    {
        FollowMouse(); 
    }

    protected void FollowMouse()
    {
        MousePosition = Input.mousePosition;
        MousePosition = Camera.main.ScreenToWorldPoint(new Vector3(MousePosition.x, MousePosition.y, 1f));
        transform.position = MousePosition;
    }
}
