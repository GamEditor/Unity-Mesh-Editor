using UnityEngine;

public class Vector3OrTransformArray
{
    public static readonly string[] choices = { "Vector3", "Transform", "Path" };
    public static readonly int vector3Selected = 0;
    public static readonly int transformSelected = 1;
    public static readonly int iTweenPathSelected = 2;

    public int selected = 0;
    public Vector3[] vectorArray;
    public Transform[] transformArray;
    public string pathName;
}