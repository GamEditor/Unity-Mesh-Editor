using System.Collections.Generic;
using UnityEngine;

public class VertexEditor : DragIt
{
    private MeshInfo m_MeshInfo;

    private List<int> m_VertexIndex;   // it will fill from MeshInfo by calling AddVertexIndex()

    private void Awake()
    {
        m_VertexIndex = new List<int>();
    }

    void Start()
    {
        m_MeshInfo = GetComponentInParent<MeshInfo>();
    }

    private new void OnMouseDrag()
    {
        base.OnMouseDrag();
        m_MeshInfo.SetVertex(m_VertexIndex, transform.localPosition);
    }

    /// <summary>
    /// Adds an VertexIndex 
    /// </summary>
    /// <param name="index">The index of vertex</param>
    public void AddVertexIndex(int index)
    {
        m_VertexIndex.Add(index);
    }
}