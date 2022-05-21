using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rank", menuName = "ScriptableObjects/Rank", order = 1)]
public class Rank : ScriptableObject
{
    public Color Color;
    public float WeaponCooldown;
    public int Healthpoints;
    public int WeaponClipSize;
    public string Label;
    [SerializeField]
    private string Tag;
    public string Weapon;

    public string GetTag()
    {
        switch (Tag)
        {
            case "$number":
                return Mathf.Floor(Random.Range(1000f, 9000f)).ToString();
            default:
                return Tag;
        }
    }
}
