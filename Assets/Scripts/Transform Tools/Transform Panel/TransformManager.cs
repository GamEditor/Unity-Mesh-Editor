using UnityEngine;
using UnityEngine.UI;

public class TransformManager : MonoBehaviour
{
    public static TransformManager Instance;

    #region UI Transform Tool
    public enum Tool { Hand, Move, Scale, Rotate }
    private Tool m_CurrentTool; // it will be set due to the transform buttons in the scene
    #endregion

    #region Scene References
    [SerializeField] private TransformButton[] m_TransformToolButtons = new TransformButton[0];
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        Instance = this;

        ManageTransformButtons();
    }
    #endregion

    public void ChangeTransformTool(Tool tool)
    {
        m_CurrentTool = tool;

        ManageTransformButtons();
    }

    public Tool GetCurrentTransformTool()
    {
        return m_CurrentTool;
    }

    private void ManageTransformButtons()
    {
        for (int i = 0; i < m_TransformToolButtons.Length; i++)
            if (m_TransformToolButtons[i].m_Tool == m_CurrentTool)
                m_TransformToolButtons[i].GetComponent<Button>().interactable = false;
            else
                m_TransformToolButtons[i].GetComponent<Button>().interactable = true;
    }
}