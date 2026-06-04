using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource maxAudioSource;
    public List<AudioClip> clip;
    public GameObject maxChecker;
    public PlayerAttacker maxHandler;
    public float maxEffect;
    void Start()
    {
        maxChecker = GameObject.FindWithTag("Generator");
        maxHandler = maxChecker.GetComponent<PlayerAttacker>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        maxEffect = maxHandler.ReturnMaxOpacity();
    }
    public void PlaySFX(int ind)
    {
        if (!(gameObject.tag == "Generator" && ind == 0))
        {
            audioSource.volume = maxEffect * maxEffect * maxEffect * maxEffect;
            audioSource.PlayOneShot(clip[ind]);
        }
        else
        {
            maxAudioSource.PlayOneShot(clip[ind]);
        }
        
    }
}
