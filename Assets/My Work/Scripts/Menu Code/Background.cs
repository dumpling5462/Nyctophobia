using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Vector3 StartPos;
    [SerializeField] float MoveModifier;
    void Start()
    {
        StartPos = transform.position;
    }


    void Update()
    {
        Vector2 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float posX = Mathf.Lerp(transform.position.x, StartPos.x - (pos.x * MoveModifier), 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, StartPos.y - (pos.y * MoveModifier), 2f * Time.deltaTime);

        transform.position = new Vector3(posX, posY, 0);
    }
}
