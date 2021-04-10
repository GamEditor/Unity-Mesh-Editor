using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]    // because of controling gameobject after changing its shape (rotating and draging with mouse)
public class MeshInfo : MonoBehaviour
{
    private Mesh m_Mesh;                // a reference to attached mesh of MeshFilter

    private List<Vector3> m_Vertices;   // a copy of m_Mesh.vertices.ToList()

    public VertexEditor m_VertexEtitorPrefab;

    void Start()
    {
        m_Mesh = GetComponent<MeshFilter>().mesh;

        // make ready to edit mesh
        m_Vertices = GetMeshVertices();
        GenerateVerticesEditorObjects();
    }

    /// <summary>
    /// After moving a VertexEditor, it should change all vertices position
    /// </summary>
    /// <param name="vertexIndex">This array should contain indexes of referenced vertices</param>
    /// <param name="position">the local position of this Vertex Editor</param>
    public void SetVertex(List<int> vertexIndex, Vector3 position)
    {
        for (int i = 0; i < vertexIndex.Count; i++)
            m_Vertices[vertexIndex[i]] = position;

        m_Mesh.vertices = m_Vertices.ToArray();
        m_Mesh.RecalculateNormals();
    }

    private List<Vector3> GetMeshVertices()
    {
        return m_Mesh.vertices.ToList();
    }

    // needs to fix
    private void GenerateVerticesEditorObjects()
    {
        // tip: we don't need to have any reference to instantiated mini cubes
        // because the cube is independent itself

        // if mesh has no vertices, it will cause an error here. we should check it here,
        // not in anywhere else (because maybe forget to check)
        if (m_Mesh.vertices.Length < 1)
            return;

        // i need have a reference to entire generated VertexEditors till the end of this method's scope
        List<VertexEditor> vertexEditors = new List<VertexEditor>();

        // the first one is going to created always
        vertexEditors.Add(Instantiate(m_VertexEtitorPrefab, transform));
        vertexEditors[0].transform.localPosition = m_Vertices[0];
        vertexEditors[0].AddVertexIndex(0);

        for (int i = 1; i < m_Vertices.Count; i++)
        {
            int index = GetVertexPositionIndex(m_Vertices[i], ref vertexEditors);

            if (index >= 0)
            {
                vertexEditors[index].AddVertexIndex(i);
            }
            else
            {
                vertexEditors.Add(Instantiate(m_VertexEtitorPrefab, transform));

                int last = vertexEditors.Count - 1;
                vertexEditors[last].transform.localPosition = m_Vertices[i];
                vertexEditors[last].AddVertexIndex(i);
            }
        }
    }

    /// <summary>
    /// Get the index of VertexEditor in a list
    /// </summary>
    /// <param name="vertexPosition">The position of vertex</param>
    /// <param name="list">Your desired list for search</param>
    /// <returns></returns>
    private int GetVertexPositionIndex(Vector3 vertexPosition, ref List<VertexEditor> list)
    {
        for (int i = 0; i < list.Count; i++)
            if (list[i].transform.localPosition == vertexPosition)
                return i;

        return -1;  // it means the search didn't found any result
    }
}