using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Specs", menuName = "MarioFootball/PlayerSpecs")]
public class PlayerSpecs : ScriptableObject
{
    public new string name;

    public float weight;

    [Range(0f, 1f)]
    public float accuracy = 0.7f;

    [Range(1f, 20f)]
    public float speed = 4f;

    [Range(5f, 45f)]
    public float shootRange = 30f;

    [Range(0f, 5f)]
    public float tackleRange = 3f;

    [Range(0f, 10f)]
    public float headButtRange = 4f;

    [Range(0f, 5f)]
    public float stunTime = 1f;

    public GameObject prefab;
}
