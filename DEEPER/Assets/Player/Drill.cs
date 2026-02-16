
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Drill : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Shake cameraShake;
    [SerializeField] VisualEffect groundDebris;
    [SerializeField] VisualEffect sparks;

    private void Start()
    {
        sparks.Stop();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Ore ore = coll.gameObject.GetComponent<Ore>();

        if (ore != null)
        {
            StartCoroutine(Mining(ore));
            sparks.transform.position = coll.ClosestPoint(transform.position);
        }
    }

    IEnumerator Mining(Ore ore)
    {
        player.canMove = false;
        //groundDebris.Stop();
        sparks.Play();
        cameraShake.Play();
         

        while (ore != null && ore.hardness > 0)
        {
            ore.hardness -= Time.deltaTime;
            yield return null;
        }

        player.canMove = true;
        //groundDebris.Play();
        sparks.Stop();
        cameraShake.Stop();

        if (ore != null)
        {
            ore.OnMined();
            Destroy(ore.gameObject);
        }
    }

}
