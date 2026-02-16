
using UnityEngine;
using System.Collections.Generic;


public class Ground : MonoBehaviour
{
    [Header("Main configs")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Camera cam;

    bool isVisible;
    bool canMove;

    [Header("Ores configs")]
    [SerializeField] OreDatabase oreDatabase;
    List<GameObject> placedOres = new List<GameObject>();

    private void Start()
    {

        PlaceOre();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        isVisible = IsVisibleFromCamera();

        if (!isVisible && canMove)
        {
            transform.position -= new Vector3(0, 36f, 0);
            canMove = false;

            PlaceOre();
        }
        else if (isVisible)
        {
            canMove = true;
        }
    }

    bool IsVisibleFromCamera()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(planes, spriteRenderer.bounds);
    }

    private void PlaceOre()
    {
        for (int i = 0; i < placedOres.Count; i++)
        {
            if (placedOres[i] != null) { Destroy(placedOres[i]); }
        }
        placedOres.Clear();

        int oreNumber = UnityEngine.Random.Range(oreDatabase.minOreNumber, oreDatabase.maxOreNumber);

        for (int i = 0; i < oreNumber; i++)
        { 
            Vector3 newPlace = Vector3.zero;
            newPlace.x = Random.Range(-oreDatabase.placeArea, oreDatabase.placeArea);
            newPlace.y = Random.Range(-oreDatabase.placeArea, oreDatabase.placeArea);

            placedOres.Add(Instantiate(oreDatabase.ore, newPlace + transform.position, Quaternion.identity));
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * oreDatabase.placeArea * 2);
    }
}
