using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRepeat : MonoBehaviour
{
    private float _spriteWidth;

    void Start()
    {
        BoxCollider2D groundCollider = GetComponent<BoxCollider2D>();
        _spriteWidth = groundCollider.size.x;
    }

    void Update()
    {
        if(transform.position.x < -_spriteWidth)
        {
            transform.Translate(Vector2.right * 2f * _spriteWidth);
        }
    }
}
