using UnityEngine;

[CreateAssetMenu(fileName = "OreDatabase", menuName = "Scriptable Objects/OreDatabase")]
public class OreDatabase : ScriptableObject
{
    public GameObject ore;
    public int maxOreNumber;
    public int minOreNumber;
    public float placeArea;
}
