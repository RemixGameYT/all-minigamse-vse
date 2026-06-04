using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float maxHorizontalHoverDistance = 0f;
    public float maxVerticalHoverDistance = 0f;
    public float hoverSpeed = 3f;
    public float hoverValue = 0f;
    private float hoverProgress = 0f;
    private Vector3 hoverPosition = Vector3.zero;
    public Vector3 startPosition;
    private Vector3 direction;
    void Start()
    {
        startPosition = transform.position;
        direction = new Vector3(maxHorizontalHoverDistance, maxVerticalHoverDistance, 0);
    }

    void Update()
    {
        hoverProgress += Time.deltaTime*hoverSpeed;
        hoverValue = Mathf.Sin(hoverProgress);
        hoverPosition = direction * hoverValue;
        Vector3 newPosition = hoverPosition + startPosition;
        transform.position = newPosition;
    }
}
