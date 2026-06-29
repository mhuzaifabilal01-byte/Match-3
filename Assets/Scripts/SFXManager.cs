using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
   public static SFXManager instance;
    public AudioSource gemBreak, bombExplosion, stoneBreak, roundOver,music;
    private void Awake()
    {
        instance = this; 
    }
    public void GemBreak()
    {
        gemBreak.Stop();
        gemBreak.pitch = Random.Range(0.8f, 1.2f);
        gemBreak.Play();
    }
    public void BombExplosion()
    {
        bombExplosion.Stop();
        bombExplosion.pitch = Random.Range(0.8f, 1.2f);
        bombExplosion.Play();
    }
    public void StoneBreak()
    {
        stoneBreak.Stop();
        stoneBreak.pitch = Random.Range(0.8f, 1.2f);
        stoneBreak.Play();
    }
    public void RoundOver()
    {
        music.Stop();
        roundOver.Play();
    }
}
