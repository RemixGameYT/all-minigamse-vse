using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInAStraightFrickingLine : MonoBehaviour
{
    public float speed = 10f;
    public float liveTime = 5f;
    public Vector3 direction;
    void Start()
    {
        Destroy(gameObject, liveTime);
        float angleRad = transform.eulerAngles.z * Mathf.Deg2Rad;
        direction = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    void Update()
    {
        transform.position = transform.position + direction*speed*Time.deltaTime;
    }
}
