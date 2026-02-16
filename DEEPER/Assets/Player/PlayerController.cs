
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] InputAction rotAction;
    [SerializeField] float speed;
    [SerializeField] float stoppingSpeed;
    [SerializeField] float rotSpeed;
    [SerializeField] float maxAngle;
    [SerializeField] float xBounds;
    float maxSpeed;

    public bool canMove;

    [Header("Fuel")]
    [SerializeField] Image fuelScale;
    [SerializeField] float maxFuel;
    public float currentFuel;
    [SerializeField] float fuelConsumption;
    Coroutine failCoroutine;

    [Header ("Effects")]
    [SerializeField] VisualEffect groundDebris;
    private void OnEnable()
    {
        rotAction.Enable();
    }
    private void OnDisable()
    {
        rotAction.Disable();
    }

    private void Start()
    {
        Cursor.visible = false;
        maxSpeed = speed;    
    }

    private void Update()
    {
        Move();
        Fuel();
    }
    private void Move()
    {
        if (!canMove) {return;}

        transform.position += -transform.up * speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x) > Mathf.Abs(xBounds))
        {
            Vector3 targetPosition = transform.position;
            if(transform.position.x > 0) { targetPosition.x = xBounds; }
            else { targetPosition.x = -xBounds; }

            transform.position = targetPosition;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, speed * 2 * Time.deltaTime);
        }

        float turnInput = rotAction.ReadValue<float>();
        transform.Rotate(0, 0, turnInput * rotSpeed * Time.deltaTime);

        float angle = transform.eulerAngles.z;
        if (angle > 180f)
            angle -= 360f;

        angle = Mathf.Clamp(angle, -maxAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Fuel()
    {
        fuelScale.fillAmount = currentFuel / maxFuel;

        currentFuel -= fuelConsumption * Time.deltaTime;

        if (currentFuel > maxFuel) { currentFuel = maxFuel; }

        if (currentFuel <= 0 && failCoroutine == null)
        {
            failCoroutine = StartCoroutine(Fail());
        }
        else if (currentFuel > 0 && failCoroutine != null)
        {
            StopCoroutine(failCoroutine);
            failCoroutine = null;
            speed = maxSpeed;
            groundDebris.Play();
        }

    }

    private IEnumerator Fail()
    {
        yield return new WaitForSeconds(0.5f);
        groundDebris.Stop();
        while (speed > 0.1f)
        {
            speed = Mathf.Lerp(speed, 0, stoppingSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * xBounds * 2);
    }
}
