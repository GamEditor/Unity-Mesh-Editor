using UnityEngine;
using UnityEngine.EventSystems;

public class TransformButton : MonoBehaviour, IPointerClickHandler
{
    public TransformManager.Tool m_Tool = TransformManager.Tool.Hand;

    public void OnPointerClick(PointerEventData eventData)
    {
        TransformManager.Instance.ChangeTransformTool(m_Tool);
    }
}