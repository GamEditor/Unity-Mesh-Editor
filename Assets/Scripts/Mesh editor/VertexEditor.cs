using UnityEngine;

public class VertexEditor : DragIt
{
    private MeshInfo m_MeshInfo;

    [HideInInspector] public int m_VertexIndex;   // it will fill from MeshInfo

    void Start()
    {
        m_MeshInfo = GetComponentInParent<MeshInfo>();
    }

    private new void OnMouseDrag()
    {
        base.OnMouseDrag();
        m_MeshInfo.SetVertex(m_VertexIndex, transform.localPosition);
    }
}