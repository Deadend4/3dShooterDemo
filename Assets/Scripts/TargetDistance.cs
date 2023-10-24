using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetDistance : MonoBehaviour
{
    [SerializeField] GameObject Muzzle;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask layerMask;
    [SerializeField] TextMeshProUGUI textDistance;
    // Start is called before the first frame update
    private void Start()
    {
        textDistance.enabled = true;
    }
    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Muzzle.transform.position, Muzzle.transform.forward, out hitInfo, maxDistance, layerMask))
        {
            float currentDistance = Vector3.Distance(Muzzle.transform.position, hitInfo.point);
            textDistance.text = string.Format("{0:N2}", currentDistance) + " м";
        }
    }
}
