using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DragIt : MonoBehaviour
{
    /* for mouse drag */
    private Vector3 m_Distance;
    private float m_PosX;
    private float m_PosY;
    
    protected void OnMouseDown()
    {
        m_Distance = Camera.main.WorldToScreenPoint(transform.position);
        m_PosX = Input.mousePosition.x - m_Distance.x;
        m_PosY = Input.mousePosition.y - m_Distance.y;
    }

    protected void OnMouseDrag()
    {
        // when drag is happend, replace the mesh vertex due to the m_VertexIndex

        Vector3 curPos = new Vector3(Input.mousePosition.x - m_PosX, Input.mousePosition.y - m_PosY, m_Distance.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);

        transform.position = worldPos;
    }
}