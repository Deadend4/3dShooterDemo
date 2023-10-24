using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Cue : MonoBehaviour
{
    [Header("Audio clips")]
    [SerializeField] private List<AudioClip> audioClips;

    [Header("Sorting")]
    [SerializeField] private bool isShuffled = false;
    [SerializeField] private int currentIndex = 0;

    private void Start()
    {
        if (isShuffled)
        {
            Shuffle(audioClips);
        }
    }
    public AudioClip PlayAudio()
    {
        if (currentIndex < audioClips.Count)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 1;
            if (isShuffled)
            {
                Shuffle(audioClips);
            }
        }
        return audioClips[currentIndex - 1];
    }

    public static void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
