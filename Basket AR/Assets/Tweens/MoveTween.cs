using UnityEngine;

public enum MoveType { World, Local, Anchor }

public class MoveTween : Tween
{
    private RectTransform m_Rect;
    private Transform m_Transform;

    private Vector3 m_Target;
    private Vector3 m_Start;
    private MoveType m_Type;

    protected override void OnStep(float t)
    {
        SetCurrent(Vector3.LerpUnclamped(m_Start, m_Target, t));
    }

    public void Initialise(Vector3 position, MoveType type)
    {
        SetStart(type);

        m_Target = position;
        m_Type = type;
    }

    public void Initialise(float? x = null, float? y = null, float? z = null, MoveType type = MoveType.World)
    {
        SetStart(type);

        //m_Target = m_Start.With(x, y, z);
        m_Type = type;
    }

    private void SetStart(MoveType type)
    {
        m_Rect = GetComponent<RectTransform>();
        m_Transform = transform;

        if (type == MoveType.Local)
        {
            m_Start = m_Transform.localPosition;
        }
        else if (type == MoveType.World)
        {
            m_Start = m_Transform.position;
        }
        else if (type == MoveType.Anchor)
        {
            m_Start = m_Rect.anchoredPosition3D;
        }
    }

    private void SetCurrent(Vector3 newPos)
    {
        if (m_Type == MoveType.Local)
        {
            m_Transform.localPosition = newPos;
        }
        else if (m_Type == MoveType.World)
        {
            m_Transform.position = newPos;
        }
        else if (m_Type == MoveType.Anchor)
        {
            m_Rect.anchoredPosition3D = newPos;
        }
    }
}