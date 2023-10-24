using UnityEngine;

public class AutoTiling : MonoBehaviour
{
    [SerializeField] bool isPlane = false;
    [SerializeField] float planeScale = 4f;
    [SerializeField] float baseScale = 1f;
    void Start()
    {
        float scaleX = transform.localScale.x;
        float scaleY = transform.localScale.y;
        Material inputMat = GetComponent<Renderer>().material;
        if (isPlane)
        {
            inputMat.mainTextureScale = new Vector2(scaleX * planeScale, scaleY * planeScale);
        }
        else
        {
            GetComponent<Renderer>().material.mainTextureScale = new Vector2(scaleX * baseScale, scaleY * baseScale);
        }


    }
}
