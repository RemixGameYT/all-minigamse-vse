using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    public GameObject playerObject;
    private Transform target;
    public float minDistanceFromPlayer = 30f;
    public float maxDistanceFromPlayer = 50f;
    public GameObject TelegramBullet;
    public int minTelegramBulletsPerAttack;
    public int maxTelegramBulletsPerAttack;
    public GameObject TikTokBullet;
    public GameObject VKVerticalBullet;
    public GameObject VKHorizontalBullet;
    public GameObject SnapChatBullet;
    public GameObject MaxJumpscare;
    public float MaxJumpscareSpeed = 5f;
    private float MaxJumpscareProgress = 1f;
    public float VKOffset = 40f;
    public float fireRate = 1.1f;
    public float maxFireRate = 0.5f;
    public float fireRateChange = 0.1f;
    public float timeTillNextSpawn = 1f;
    public float telegramBulletBuff = 0.2f;
    public PlaySound soundPlayer;
    private bool maxMode = false;
    void Start()
    {
        soundPlayer = GetComponent<PlaySound>();
    }
    void Update()
    {
        ChangeMaxOpacity();
        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.X))
        {
            maxMode = true;
        }
        Player playerScript = playerObject.GetComponent<Player>();
        if (playerScript.Check_If_Started())
        {
            timeTillNextSpawn -= Time.deltaTime;
        }
        if (timeTillNextSpawn <= 0)
        {
            int attackChoice = UnityEngine.Random.Range(0, 101);
            if (fireRate > maxFireRate)
            {
                fireRate -= fireRateChange;
            }
            if (maxMode && attackChoice%10==0)
            {
                int telegramBulletsPerAttack = UnityEngine.Random.Range(minTelegramBulletsPerAttack, maxTelegramBulletsPerAttack + 1);
                SpawnMAXBullet(telegramBulletsPerAttack);
            }
            else if (attackChoice < 40)
            {
                int telegramBulletsPerAttack = UnityEngine.Random.Range(minTelegramBulletsPerAttack, maxTelegramBulletsPerAttack+1);
                SpawnTelegramBullets(telegramBulletsPerAttack);
            }
            else if (attackChoice < 60)
            {
                SpawnTikTokBullets();
            }
            else if (attackChoice < 80)
            {
                SpawnVKBullets();
            }
            else if (attackChoice < 100)
            {
                SpawnSnapChatBullet();
            }
            else if (playerScript.HitYet == false)
            {
                int telegramBulletsPerAttack = UnityEngine.Random.Range(minTelegramBulletsPerAttack, maxTelegramBulletsPerAttack + 1);
                SpawnMAXBullet(telegramBulletsPerAttack);
            }
            else
            {
                int telegramBulletsPerAttack = UnityEngine.Random.Range(minTelegramBulletsPerAttack, maxTelegramBulletsPerAttack + 1);
                SpawnTelegramBullets(telegramBulletsPerAttack);
            }
            timeTillNextSpawn = fireRate;
        }
    }
    private void SpawnTelegramBullets(int amount)
    {
        soundPlayer.PlaySFX(1);
        for (int i = 0; i < amount; i++)
        {
            timeTillNextSpawn -= telegramBulletBuff;
            float distanceFromPlayer = UnityEngine.Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
            Vector3 target = playerObject.transform.position;
            float directonRad = UnityEngine.Random.Range(-90, 90);
            Vector3 bulletDirection = new Vector3(Mathf.Cos(directonRad) * distanceFromPlayer, Mathf.Sin(directonRad) * distanceFromPlayer);
            Vector3 newPos = target + bulletDirection;
            Instantiate(TelegramBullet, newPos, Quaternion.identity);
        }
    }
    private void SpawnTikTokBullets()
    {
        soundPlayer.PlaySFX(2);
        float distanceFromPlayer = UnityEngine.Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        Vector3 target = playerObject.transform.position;
        float directonRad = UnityEngine.Random.Range(-90, 90);
        Vector3 bulletDirection = new Vector3(Mathf.Cos(directonRad) * distanceFromPlayer, Mathf.Sin(directonRad) * distanceFromPlayer);
        Vector3 newPos = target + bulletDirection;
        Instantiate(TikTokBullet, newPos, Quaternion.identity);
    }
    private void SpawnVKBullets()
    {
        soundPlayer.PlaySFX(3);
        Vector3 newPos = transform.position;
        Vector3 horizontalOffset = new Vector3(0f, VKOffset, 0f);
        Vector3 verticalOffset = new Vector3(VKOffset, 0f, 0f);
        Instantiate(VKHorizontalBullet, newPos+horizontalOffset, Quaternion.identity);
        Instantiate(VKVerticalBullet, newPos + verticalOffset, Quaternion.identity);
    }

    private void SpawnSnapChatBullet()
    {
        soundPlayer.PlaySFX(4);
        float distanceFromPlayer = UnityEngine.Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        Vector3 target = playerObject.transform.position;
        float directonRad = UnityEngine.Random.Range(-90, 90);
        Vector3 bulletDirection = new Vector3(Mathf.Cos(directonRad) * distanceFromPlayer, Mathf.Sin(directonRad) * distanceFromPlayer);
        Vector3 newPos = target + bulletDirection;
        Instantiate(SnapChatBullet, newPos, Quaternion.identity);
    }
    private void SpawnMAXBullet(int amount)
    {
        SpriteRenderer maxRenderer = MaxJumpscare.GetComponent<SpriteRenderer>();
        Color maxOpacity = maxRenderer.color;
        MaxJumpscareProgress = 0f;
        maxOpacity.a = 1f;
        maxRenderer.color = maxOpacity;
        soundPlayer.PlaySFX(0);
        float distanceFromPlayer = UnityEngine.Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        Vector3 target = playerObject.transform.position;
        float directonRad = UnityEngine.Random.Range(-90, 90);
        Vector3 bulletDirection = new Vector3(Mathf.Cos(directonRad) * distanceFromPlayer, Mathf.Sin(directonRad) * distanceFromPlayer);
        Vector3 newPos = target + bulletDirection;
        Instantiate(TikTokBullet, newPos, Quaternion.identity);
        newPos = transform.position;
        Vector3 horizontalOffset = new Vector3(0f, VKOffset, 0f);
        Vector3 verticalOffset = new Vector3(VKOffset, 0f, 0f);
        Instantiate(VKHorizontalBullet, newPos + horizontalOffset, Quaternion.identity);
        Instantiate(VKVerticalBullet, newPos + verticalOffset, Quaternion.identity);
        distanceFromPlayer = UnityEngine.Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        target = playerObject.transform.position;
        directonRad = UnityEngine.Random.Range(-90, 90);
        bulletDirection = new Vector3(Mathf.Cos(directonRad) * distanceFromPlayer, Mathf.Sin(directonRad) * distanceFromPlayer);
        newPos = target + bulletDirection;
        Instantiate(SnapChatBullet, newPos, Quaternion.identity);
        for (int i = 0; i < amount; i++)
        {
            distanceFromPlayer = UnityEngine.Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
            target = playerObject.transform.position;
            directonRad = UnityEngine.Random.Range(-90, 90);
            bulletDirection = new Vector3(Mathf.Cos(directonRad) * distanceFromPlayer, Mathf.Sin(directonRad) * distanceFromPlayer);
            newPos = target + bulletDirection;
            Instantiate(TelegramBullet, newPos, Quaternion.identity);
        }
    }
    public void ChangeMaxOpacity()
    {
        SpriteRenderer maxRenderer = MaxJumpscare.GetComponent<SpriteRenderer>();
        Color maxOpacity = maxRenderer.color;
        MaxJumpscareProgress += Time.deltaTime*MaxJumpscareSpeed;
        maxOpacity.a = 1f-MaxJumpscareProgress;
        maxRenderer.color = maxOpacity;
    }
    public float ReturnMaxOpacity()
    {
        return MaxJumpscareProgress;
    }
}
