using TMPro;
using UnityEngine;

public class DepthLabel : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI TMP;
    float startY;
    private void Start()
    {
        startY = player.position.y;
    }

    private void Update()
    {
        TMP.text = Mathf.Round(Mathf.Abs(player.position.y - startY)).ToString() + "M";
    }

}
