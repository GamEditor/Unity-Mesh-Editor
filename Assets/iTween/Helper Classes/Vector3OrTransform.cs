using UnityEngine;

public class Vector3OrTransform
{
    public static readonly string[] choices = { "Vector3", "Transform" };
    public static readonly int vector3Selected = 0;
    public static readonly int transformSelected = 1;
    public int selected = 0;
    public Vector3 vector;
    public Transform transform;
}