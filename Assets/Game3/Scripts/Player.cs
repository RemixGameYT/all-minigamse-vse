using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Vector2 moveToPos;
    public Vector2 currentPos;
    public float Speed;
    public Vector3 mouseWorldPos;
    public float BoxLimits;
    public float BoxScale = 1f;
    public CircleCollider2D Collider;
    public GameObject battleBox;
    public GameObject HitObject;
    private TMP_Text HitText;
    public GameObject TimeObject;
    private TMP_Text TimeText;
    public TMP_Text FinalText;
    public GameObject GameOverText;
    public GameObject MenuBG;
    public GameObject ScoreAndTimeCounters;
    public Image panel;
    private float TimeElapsed = 0f;
    private float actualScore = 0f;
    public int Score = 0;
    public float InvFrames = 1f;
    private float InvCurr = 0f;
    private float blink = 0f;
    public int blinkAmount = 6;
    public SpriteRenderer currentSprite;
    private bool hasStarted = false;
    private int maxTime = 15;
    public int TelegramDamage;
    public int TikTokDamage;
    public int SnapChatDamage;
    public int VKDamage;
    public PlaySound soundPlayer;
    private bool playedEndgameSFX = false;
    public float textBlinkSpeed = 2f;
    private float blinkProgress = 1f;
    private float blinkProgressT = 1f;
    private int lastSecs = 0;
    public bool HitYet = false;
    public GameObject menuMusic;
    private AudioSource menuSource;
    public AudioClip timeClip;
    public GameObject startUI;
    void Start()
    {
        HitText = HitObject.GetComponent<TMP_Text>();
        TimeText = TimeObject.GetComponent<TMP_Text>();
        Time.timeScale = 1f;
        soundPlayer = GetComponent<PlaySound>();
        battleBox.transform.localScale = transform.localScale / 6.7f * BoxLimits * BoxScale;
        BoxLimits -= transform.localScale.y / 2;
        Collider = GetComponent<CircleCollider2D>();
        currentSprite = GetComponent<SpriteRenderer>();
        Instantiate(battleBox);
        menuSource = menuMusic.GetComponent<AudioSource>();
    }
    void Update()
    {

        Blink(HitObject, false);
        BlinkT(TimeObject, false);
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.V) && !hasStarted)
        {
            maxTime = 3600;
        }
        if (hasStarted)
        {
            actualScore = Mathf.Clamp(actualScore, 0f, 100f);
            actualScore += Time.deltaTime * 100 / 15;
            Score = Mathf.RoundToInt(actualScore);
            HitText.text = $"{Score}";
            FinalText.text = $"Твой счёт: \n{Score} из 100";
            TimeElapsed += Time.deltaTime;
        }
        int seconds = Mathf.CeilToInt(TimeElapsed);
        if (seconds != lastSecs && maxTime - seconds + 1 <= 3)
        {
            BlinkT(TimeObject, true);
        }
        lastSecs = seconds;
        if (seconds == maxTime+1)
        {
            Time.timeScale = 0f;
            GameOverText.SetActive(true);
            MenuBG.SetActive(true);
            ScoreAndTimeCounters.SetActive(false);
            menuSource.Stop();
        }
        if (maxTime - seconds + 1 <= 3 && !playedEndgameSFX)
        {
            playedEndgameSFX = true;
            soundPlayer.PlaySFX(1);
        }
        else
        {
            TimeText.text = $"{TimeSpan.FromSeconds(maxTime - seconds).ToString(@"mm\:ss")}";
        }
        if (Input.GetMouseButton(0))
        {
            if (!hasStarted)
            {
                menuSource.volume = 1f;
                menuSource.clip = timeClip;
                menuSource.Play();
                startUI.SetActive(false);
                MenuBG.SetActive(false);
                ScoreAndTimeCounters.SetActive(true);
                hasStarted = true;
            }
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            mouseWorldPos.z = 0f;
            moveToPos = mouseWorldPos;
        }
        if (InvCurr > 0)
        {
            InvCurr -= Time.deltaTime;
            blink = Mathf.Cos(InvCurr * 4 * blinkAmount);
            currentSprite.color = new Color((191 + 64 * blink) / 255, (191 + 64 * blink) / 255, (191 + 64 * blink) / 255);
        }
        else
        {
            InvCurr = 0f;
            blink = 1f;
            currentSprite.color = new Color(1f, 1f, 1f);
        }
        currentPos = transform.position;
        Vector2 direction = (moveToPos - currentPos).normalized;
        Vector2 newPos;
        float dist;
        dist = Vector2.Distance(currentPos, moveToPos);
        if (dist < Speed * Time.deltaTime)
        {
            newPos = currentPos + direction * dist * Time.deltaTime;
        }
        else
        {
            newPos = currentPos + direction * Speed * Time.deltaTime;
        }
        newPos.x = Math.Clamp(newPos.x, -BoxLimits, BoxLimits);
        newPos.y = Math.Clamp(newPos.y, -BoxLimits, BoxLimits);
        transform.position = newPos;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "TelegramBullet" && InvCurr <= 0)
        {
            actualScore -= TelegramDamage;
            HitText.text = $"Счёт: {Mathf.Max(Score, 0)}";
            FinalText.text = $"Счёт: {Mathf.Max(Score, 0)}";
            Destroy(other.gameObject);
            InvCurr = InvFrames;
            soundPlayer.PlaySFX(0);
            Blink(HitObject, true);
            HitYet = true;
        }
        if (other.gameObject.tag == "TikTokBullet" && InvCurr <= 0)
        {
            actualScore -= TikTokDamage;
            Score = Mathf.RoundToInt(actualScore);
            HitText.text = $"{Score}";
            FinalText.text = $"Твой счёт: \n{Score} из 100";
            Destroy(other.gameObject);
            InvCurr = InvFrames/4;
            soundPlayer.PlaySFX(0);
            Blink(HitObject, true);
            HitYet = true;
        }
        if (other.gameObject.tag == "VKBullet" && InvCurr <= 0)
        {
            actualScore -= VKDamage;
            Score = Mathf.RoundToInt(actualScore);
            HitText.text = $"{Score}";
            FinalText.text = $"Твой счёт: \n{Score} из 100";
            Destroy(other.gameObject);
            InvCurr = InvFrames;
            soundPlayer.PlaySFX(0);
            Blink(HitObject, true);
            HitYet = true;
        }
        if (other.gameObject.tag == "SnapChatBullet" && InvCurr <= 0)
        {
            actualScore -= SnapChatDamage;
            Score = Mathf.RoundToInt(actualScore);
            HitText.text = $"{Score}";
            FinalText.text = $"Твой счёт: \n{Score} из 100";
            Destroy(other.gameObject);
            InvCurr = InvFrames;
            soundPlayer.PlaySFX(0);
            Blink(HitObject, true);
            HitYet = true;
        }
    }
    public bool Check_If_Started()
    {
        return hasStarted;
    }
    public void Blink(GameObject objectToBlink, bool blinkNow)
    {
        TMP_Text objectSprite = objectToBlink.GetComponent<TMP_Text>();
        if (blinkNow)
        {
            blinkProgress = 0f;
        }
        blinkProgress += Time.deltaTime * textBlinkSpeed;
        objectSprite.color = new Color(1f, 1f*blinkProgress, 1f*blinkProgress);
    }
    public void BlinkT(GameObject objectToBlink, bool blinkNow)
    {
        TMP_Text objectSprite = objectToBlink.GetComponent<TMP_Text>();
        if (blinkNow)
        {
            blinkProgressT = 0f;
        }
        blinkProgressT += Time.deltaTime * textBlinkSpeed;
        objectSprite.color = new Color(1f, 1f * blinkProgressT, 1f * blinkProgressT);
    }
}
