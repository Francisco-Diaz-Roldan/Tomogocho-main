using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] private float _initialScrollSpeed = 5; 

    private float _scrollSpeed;
    private float _timer;


    void Start()
    {
        _scrollSpeed = _initialScrollSpeed;
    }

    void Update()
    {
        transform.Translate(Vector2.left * _scrollSpeed * Time.deltaTime);
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        float speedDivider = 10f;
        _timer += Time.deltaTime;
        _scrollSpeed = _initialScrollSpeed + _timer / speedDivider;
    }
}