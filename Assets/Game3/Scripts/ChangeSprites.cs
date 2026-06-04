using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprites : MonoBehaviour
{
    public List<Sprite> sprites;
    public SpriteRenderer currentSpriteRenderer;
    public GameObject outlineMaker;
    public float spriteChangeFrequency = 0.3f;
    private float spriteChangeProgress = 0.2f;
    void Start()
    {
        currentSpriteRenderer = gameObject.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteChangeProgress += Time.deltaTime;
        if (spriteChangeFrequency < spriteChangeProgress)
        {
            spriteChangeProgress = 0;
            Sprite newSprite = sprites[UnityEngine.Random.Range(0, sprites.Count)];
            currentSpriteRenderer.sprite = newSprite;
            SpriteRenderer shadeRenderer = outlineMaker.GetComponent<SpriteRenderer>();
            shadeRenderer.sprite = newSprite;
        }
    }
}
