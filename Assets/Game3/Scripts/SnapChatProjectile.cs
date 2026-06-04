using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SnapChatProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float liveTime = 5f;
    public Vector3 direction;
    public Vector3 startingScale;
    public float currentScale = 1f;
    public float scalingSpeed = 0.3f;
    void Start()
    {
        startingScale = transform.localScale;
        Destroy(gameObject, liveTime);
        float angleRad = transform.eulerAngles.z * Mathf.Deg2Rad;
        direction = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    void Update()
    {
        transform.position = transform.position + direction * speed * Time.deltaTime;
        currentScale += Time.deltaTime * scalingSpeed;
        transform.localScale = startingScale * currentScale;
    }
}
