using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    [Header("Enemy Detection")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float enemyDistance = 100f;
    [SerializeField] Camera MainCamera;
    private Vector3 Ray_start_position = new Vector3(Screen.width / 2, Screen.height / 2, 0);

    void Update()
    {
        releaseRay();
    }

    private void releaseRay()
    {
        Ray ray = MainCamera.ScreenPointToRay(Ray_start_position);

        RaycastHit hitInfo;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitInfo, enemyDistance, enemyMask))
            {
                switch (hitInfo.transform.tag)
                {
                    case "head":
                        Debug.Log("HEADSHOT");
                        break;
                    case "hand":
                        Debug.Log("HANDSHOT");
                        break;

                    default:
                        Debug.Log("SHOT");
                        break;
                }
            }
        }

    }
}