using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float aimingTime = 0.1f;
    [SerializeField] GameObject hands;
    private Vector3 basePosition;
    private Quaternion baseRotation;
    [SerializeField] Transform pistolTransform;
    [SerializeField] Transform rifleTransform;
    [SerializeField] Transform newTransform;
    [SerializeField] GameObject crosshair;
    Vector3 velocity;
    bool isMoving = false;
    bool isOrigin = false;
    WeaponData weaponData;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            basePosition = hands.transform.localPosition;
            baseRotation = hands.transform.localRotation;
            weaponData = GetComponent<WeaponShoot>().WeaponData;
            if (weaponData.name.Equals("1_PM"))
            {
                newTransform.localPosition = pistolTransform.localPosition;
                newTransform.localRotation = pistolTransform.localRotation;
            }
            else
            {
                newTransform.localPosition = rifleTransform.localPosition;
                newTransform.localRotation = rifleTransform.localRotation;
            }
            isMoving = true;
            isOrigin = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isMoving = true;
            isOrigin = false;
        }
        if (isMoving)
        {
            if (isOrigin)
            {
                hands.transform.localPosition = Vector3.SmoothDamp(hands.transform.localPosition,
                                            newTransform.localPosition, ref velocity, aimingTime);
                hands.transform.localRotation = Quaternion.RotateTowards(hands.transform.localRotation, newTransform.localRotation, aimingTime);
                if (hands.transform.localPosition.Equals(newTransform.localPosition))
                {
                    isMoving = false;
                }
            }
            else
            {
                hands.transform.localPosition = Vector3.SmoothDamp(hands.transform.localPosition,
                                            basePosition, ref velocity, aimingTime);
                hands.transform.localRotation = Quaternion.RotateTowards(hands.transform.localRotation, baseRotation, aimingTime);
                if (hands.transform.localPosition.Equals(basePosition))
                {
                    isMoving = false;
                }
            }
        }

    }
}
