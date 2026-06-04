using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhaseOut : MonoBehaviour
{
    public float disappearingTime;
    private float disappearingProgress;
    public TMP_Text sprite;
    public RectTransform scaler;
    public RectTransform startScale;
    public GameObject playerObject;
    void Start()
    {
        sprite = gameObject.GetComponent<TMP_Text>();
        scaler = gameObject.GetComponent<RectTransform>();
        playerObject = GameObject.FindWithTag("Player");
        startScale = scaler;
    }

    void Update()
    {
        Player hasStarted = playerObject.GetComponent<Player>();
        if (hasStarted.Check_If_Started())
        {
            Destroy(gameObject, disappearingTime);
            disappearingProgress += Time.deltaTime / disappearingTime;
            Color newColor = sprite.color;
            newColor.a = 1 - disappearingProgress;
            sprite.color = newColor;
            scaler.localScale = startScale.localScale * (1 - disappearingProgress);
        }
    }
}
