using UnityEngine;

public class OrientTween : Tween
{
    private Quaternion m_StartRotation;
    private Vector3 m_StartPosition;

    private Quaternion m_EndRotation;
    private Vector3 m_EndPosition;

    private Transform m_Transform;

    protected override void OnStep(float t)
    {
        m_Transform.rotation = Quaternion.SlerpUnclamped(m_StartRotation, m_EndRotation, t);
        m_Transform.position = Vector3.LerpUnclamped(m_StartPosition, m_EndPosition, t);
    }

    public void Initialise(Vector3 position, Quaternion rotation)
    {
        m_Transform = transform;
        m_StartRotation = m_Transform.rotation;
        m_StartPosition = m_Transform.position;
        m_EndRotation = rotation;
        m_EndPosition = position;

        if (m_StartPosition == m_EndPosition && m_StartRotation == m_EndRotation)
        {
            End();
        }
    }
}