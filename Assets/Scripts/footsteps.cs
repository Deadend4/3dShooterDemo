using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class footsteps : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float walkingTime;
    [SerializeField] AudioSource audioSource;
    [Header("Sounds")]
    [SerializeField] private List<AudioClip> woodFeet;
    [SerializeField] private List<AudioClip> metalFeet;
    [SerializeField] private List<AudioClip> concreteFeet;

    [Header("Layers")]
    [SerializeField] LayerMask groundMask;

    StarterAssetsInputs starterAssetsInputs;
    private float maxDistance = 2f;
    RaycastHit hitInfo;

    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (starterAssetsInputs.move != Vector2.zero)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, maxDistance, groundMask))
            {
                PlayFootsteps(hitInfo.transform.tag);
            }
        }

    }

    void PlayFootsteps(string tag)
    {
        if (tag == "metal")
        {
            StartCoroutine(Walking(metalFeet));
        }
    }
    IEnumerator Walking(List<AudioClip> audios)
    {
        audioSource.PlayOneShot(audios[Random.Range(0, audios.Count)]);
        yield return new WaitForSeconds(walkingTime);
        StartCoroutine(Walking(metalFeet));
    }
}
