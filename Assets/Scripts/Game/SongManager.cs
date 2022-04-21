using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource hitSong;
    [SerializeField]
    private AudioSource shootSong;

    private static SongManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static void HitSong()
    {
        instance.hitSong.Play();
    }    
    public static void ShootSong()
    {
        instance.shootSong.Play();
    }
}