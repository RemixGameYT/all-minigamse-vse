using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VKBullet : MonoBehaviour
{
    public float liveTime = 5f;
    public bool isHorisontal;
    public float minFireRate = 0.7f;
    public float maxFireRate = 1.0f;
    private float fireProgress = 0f;
    private float neededFireProgress = 0f;
    public GameObject miniBullet;
    private Quaternion bulletRotation;
    public PlaySound soundPlayer;
    void Start()
    {
        soundPlayer = GetComponent<PlaySound>();
        Destroy(gameObject, liveTime);
        neededFireProgress = Random.Range(minFireRate, maxFireRate);
    }

    void Update()
    {
        fireProgress += Time.deltaTime;
        if (fireProgress > neededFireProgress) 
        {
            
            if (isHorisontal) 
            {
                bulletRotation = Quaternion.Euler(0,0,-90);
            }
            else
            {
                bulletRotation = Quaternion.Euler(0,0,180);
            }
            soundPlayer.PlaySFX(0);
            Instantiate(miniBullet, transform.position, bulletRotation);
            fireProgress = 0f;
            neededFireProgress = Random.Range(minFireRate, maxFireRate);
        }
    }
}
