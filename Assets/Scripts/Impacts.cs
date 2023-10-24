using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impacts : MonoBehaviour
{
    [Header("Impacts")]
    [SerializeField] public GameObject impactWood;
    [SerializeField] public GameObject impactMetal;
    [SerializeField] public GameObject impactConcrete;

    [Header("Sounds")]
    [SerializeField] public AudioClip soundWood;
    [SerializeField] public AudioClip soundMetal;
    [SerializeField] public AudioClip soundConcrete;

    [Header("Decals")]
    [SerializeField] public GameObject decalWood;
    [SerializeField] public GameObject decalMetal;
    [SerializeField] public GameObject decalConcrete;
}
