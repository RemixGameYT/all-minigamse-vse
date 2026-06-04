using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class TelegramBullet : MonoBehaviour
{
    public float timeToShoot = 1f;
    public float timeStopAiming = 0.7f;
    public float speed = 200f;
    public float liveTime = 5f;
    private GameObject playerToTarget;
    private float counter = 0f;
    public Vector3 direction;
    public SpriteRenderer sprite;
    private bool alreadyShot = false;
    public PlaySound soundPlayer;
    void Start()
    {
        soundPlayer = GetComponent<PlaySound>();
        playerToTarget = GameObject.FindWithTag("Player");
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (counter < timeStopAiming)
        {
            RotateToTarget(playerToTarget.transform);
            float angleRad = transform.eulerAngles.z * Mathf.Deg2Rad;
            direction = new Vector3(-Mathf.Sin(angleRad), Mathf.Cos(angleRad), 0).normalized;
        }
        else if (counter > timeToShoot)
        {
            if (!alreadyShot)
            {
                alreadyShot = true;
                soundPlayer.PlaySFX(0);
            }
            transform.position = transform.position + direction * speed * Time.deltaTime;
        }
        else 
        {
            sprite.color = new Color((255-255*(counter-timeStopAiming)/(timeToShoot - timeStopAiming))/255, (255 - 128 * (counter - timeStopAiming) / (timeToShoot - timeStopAiming)) / 255, 1f);
        }
        Player playerScript = playerToTarget.GetComponent<Player>();
        if (playerScript.Check_If_Started())
        {
            counter += Time.deltaTime;
            Destroy(gameObject, liveTime);
        }
    }
    void RotateToTarget(Transform target)
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
