using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using TMPro;
using UnityEngine.PlayerLoop;

public class Raycaster : MonoBehaviour
{
    // Reference to the camera in the scene
    public Camera MainCamera;
    [Header("Character Settings")]
    [SerializeField] Animator playerAnimator;
    [SerializeField] FirstPersonController fpsController;

    [Header("Wall's Detection Settings")]
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private float wallDistance;


    [Header("Interaction's Detection Settings")]
    [SerializeField] private LayerMask interactionMask;
    [SerializeField] private float interactionDistance;

    [Header("Keybinds")]
    [SerializeField] public KeyCode interact = KeyCode.E;

    [Header("UI")]
    [SerializeField] TMP_Text textInfo;
    [SerializeField] Animator textAnimator;

    private bool isWalling = false;
    private Interact tempInteract = null;
    private TakeAmmo tempTakeAmmo = null;
    private Vector3 Ray_start_position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fpsController = GetComponent<FirstPersonController>();
    }

    void Update()
    {
        releaseRay();
    }

    private void releaseRay()
    {
        Ray ray = MainCamera.ScreenPointToRay(Ray_start_position);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, wallDistance, wallMask))
        {
            // The ray intersected with an object, do something with it
            if (!isWalling)
            {
                isWalling = !isWalling;
                playerAnimator.SetBool("isWall", isWalling);
            }
        }
        else
        {
            if (isWalling)
            {
                isWalling = !isWalling;
                playerAnimator.SetBool("isWall", isWalling);
            }
        }

        if (Physics.Raycast(ray, out hitInfo, interactionDistance, interactionMask))
        {
            if (tempInteract == null)
            {
                //tempInteract = hitInfo.transform.GetComponent<Interact>();

                if (hitInfo.transform.TryGetComponent<Interact>(out tempInteract))
                {
                    textAnimator.SetBool("isShowing", true);
                    textInfo.text = "(" + interact + ") " + tempInteract.currentObject.actionText;
                }
                else if (hitInfo.transform.TryGetComponent<TakeAmmo>(out tempTakeAmmo))
                {
                    textAnimator.SetBool("isShowing", true);
                    textInfo.text = "(" + interact + ") " + "Взять " + tempTakeAmmo.AmmoCount + " патронов " + tempTakeAmmo.WeaponData.Caliber;

                }


            }

            if (Input.GetKeyDown(interact))
            {
                if (tempInteract != null)
                    tempInteract.UseInteraction();
                if (tempTakeAmmo != null)
                    tempTakeAmmo.RecalculateAmmo();
            }
        }
        else
        {
            textAnimator.SetBool("isShowing", false);
            tempInteract = null;
            tempTakeAmmo = null;
        }
    }
}
