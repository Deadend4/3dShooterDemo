using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DestroyDecal : MonoBehaviour
{
    public float lifetime = 10f;
    public bool deactivate = false;

    private float life;
    private float fadeout;

    private DecalProjector projector;
    void OnEnable()
    {
        projector = GetComponent<DecalProjector>();
        // Debug.Log("projector fade factor = " + projector.fadeFactor + "; fade scale = " + projector.fadeScale);
        //Start lifetime coroutine
        life = lifetime;
        fadeout = life * projector.fadeScale;
        StopAllCoroutines();
        StartCoroutine("holeUpdate");
    }

    IEnumerator holeUpdate()
    {
        while (life > 0f)
        {
            life -= Time.deltaTime;
            if (life <= fadeout)
            {
                projector.fadeFactor = Mathf.Lerp(0f, 1f, life / fadeout);
                //color.a = Mathf.Lerp(0f, orgAlpha, life/fadeout);
            }

            yield return null;
        }
        Destroy(gameObject);
    }
}
