using UnityEngine;

[CreateAssetMenu(fileName = "Attributes", menuName = "ScriptableObjects/AttributesScriptableObject", order = 1)]
public class AttributesScriptableObject : ScriptableObject
{
    //status data
    public int hunger;
    public int stamina;
    public int intellectScore;
    public int frustration;
}
