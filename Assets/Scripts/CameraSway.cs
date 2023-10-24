using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    #region Variables
    public float intensity;
    public float smooth;
    private Quaternion originRotation;
    #endregion
    // Start is called before the first frame update
    #region Monobehaviour Callbacks
    private void Start()
    {
        originRotation = transform.localRotation;
    }
    private void Update()
    {
        UpdateSway();
    }
    #endregion
    #region Private Methods
    void UpdateSway()
    {
        float x_mouse = Input.GetAxis("Mouse X");
        float y_mouse = Input.GetAxis("Mouse Y");
        Quaternion x_adj = Quaternion.AngleAxis(-intensity * x_mouse, Vector3.up);
        Quaternion y_adj = Quaternion.AngleAxis(intensity * y_mouse, Vector3.right);
        Quaternion target_rotation = originRotation * x_adj * y_adj;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, smooth * Time.deltaTime);
    }
    #endregion
}
