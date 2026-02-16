using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Many : MonoBehaviour
{
    [SerializeField] InputAction startAction;
    [SerializeField] GameObject[] otherUi;

    private void OnEnable()
    {
        startAction.Enable();
    }
    private void OnDisable()
    {
        startAction.Disable();
    }

    private void Start()
    {
        StartCoroutine(OpenMany());
    }

    private IEnumerator OpenMany()
    {
        for (int i = 0; i < otherUi.Length; i++)
        { otherUi[i].gameObject.SetActive(false); }

        Time.timeScale = 0;
        yield return new WaitUntil(() => startAction.IsPressed());
        Time.timeScale = 1;
        gameObject.SetActive(false);

        for (int i = 0; i < otherUi.Length; i++)
        { otherUi[i].gameObject.SetActive(true); }
    }
}
