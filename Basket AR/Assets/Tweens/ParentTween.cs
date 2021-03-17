using UnityEngine;

public class ParentTween : Tween
{
    private Quaternion m_StartRotation;
    private Vector3 m_StartPosition;

    private Quaternion m_EndRotation;
    private Vector3 m_EndPosition;

    private Transform m_Transform;

    protected override void OnStep(float t)
    {
        m_Transform.localRotation = Quaternion.SlerpUnclamped(m_StartRotation, m_EndRotation, t);
        m_Transform.localPosition = Vector3.LerpUnclamped(m_StartPosition, m_EndPosition, t);
    }

    public void Initialise()
    {
        Initialise(Quaternion.identity);
    }

    public void Initialise(Quaternion offset)
    {
        m_Transform = transform;
        m_StartRotation = m_Transform.localRotation;
        m_StartPosition = m_Transform.localPosition;
        m_EndPosition = Vector3.zero;
        m_EndRotation = offset;
    }
}