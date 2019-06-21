using UnityEngine;
using UnityEngine.EventSystems;

public class CompassController : MonoBehaviour, IPointerClickHandler
{
    private Transform m_MainCamera;

    private void Start()
    {
        m_MainCamera = Camera.main.transform;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        iTween.RotateTo(m_MainCamera.gameObject, Vector3.zero, 0.2f);
        //m_MainCamera.eulerAngles = new Vector3(0, 0, 0);
        Debug.Log("compass is clicked");
    }
}