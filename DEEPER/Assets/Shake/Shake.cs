using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] bool playOnAwake;
    public Vector3 axis;
    public float scale;
    public float speed;
    Coroutine shakeCoroutine;

    private void Start()
    {
        if (playOnAwake) { Play(); }
    }

    public void Play()
    {
        if (shakeCoroutine == null)
        shakeCoroutine = StartCoroutine(Shaking());
    }
    public void Stop()
    {
        if (shakeCoroutine != null) 
        {
            StopCoroutine(shakeCoroutine);
            shakeCoroutine = null;
        }
    }
    IEnumerator Shaking()
    {
        while (true)
        {
            Vector3 random = Random.insideUnitSphere;

            Vector3 offset = new Vector3(random.x * axis.x, random.y * axis.y,random.z * axis.z) * scale;

            transform.position += offset;
            yield return new WaitForSeconds(1f / speed);
            transform.position -= offset;
        }
    }
}
