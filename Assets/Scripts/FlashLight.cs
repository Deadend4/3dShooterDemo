using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    Light flashLight;
    [SerializeField] AudioSource playerAudioSource;
    [SerializeField] KeyCode flashlightKeyCode = KeyCode.T;
    [SerializeField] Cue lightSwitchSound;
    private void Start()
    {
        flashLight = GetComponent<Light>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(flashlightKeyCode))
            SwitchLightState();
    }

    private void SwitchLightState()
    {
        playerAudioSource.PlayOneShot(lightSwitchSound.PlayAudio());
        flashLight.enabled = !flashLight.enabled;
    }
}
