using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class MeshInfo : MonoBehaviour
{
    private Mesh m_Mesh;
    private MeshCollider m_MeshCollider;

    public VertexEditor m_MiniCubePrefab;

    private List<Vector3> m_Vertices;

    void Start()
    {
        m_Mesh = GetComponent<MeshFilter>().mesh;
        m_MeshCollider = GetComponent<MeshCollider>();

        // make ready to edit mesh
        m_Vertices = GetMeshVertices();
        GenerateVerticesEditorObjects();
    }

    public void SetVertex(int index, Vector3 vertex)
    {
        m_Vertices[index] = vertex;

        //m_Mesh.SetVertices(m_Vertices);
        m_Mesh.vertices = m_Vertices.ToArray();
        //m_MeshCollider.mesh
        m_Mesh.RecalculateNormals();
    }

    private List<Vector3> GetMeshVertices()
    {
        return m_Mesh.vertices.ToList();
    }

    private void GenerateVerticesEditorObjects()
    {
        // tip: we don't need to have any reference to instantiated mini cubes
        // because the cube is independent itself

        for (int i = 0; i < m_Vertices.Count; i++)
        {
            VertexEditor meshEditor = Instantiate(m_MiniCubePrefab, transform);
            meshEditor.transform.localPosition = m_Vertices[i];
            meshEditor.name = "MeshEditor " + i;
            meshEditor.m_VertexIndex = i;
        }
    }
}