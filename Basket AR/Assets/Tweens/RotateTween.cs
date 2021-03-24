using UnityEngine;

public class RotateTween : Tween
{
    private Transform m_Transform;
    private Quaternion m_Target;
    private Quaternion m_Start;
    private bool m_Local;

    protected override void OnStep(float t)
    {
        if (m_Local == true)
        {
            m_Transform.localRotation = Quaternion.SlerpUnclamped(m_Start, m_Target, t);
        }
        else
        {
            m_Transform.rotation = Quaternion.SlerpUnclamped(m_Start, m_Target, t);
        }
    }

    public void Initialise(Quaternion rotation, bool local)
    {
        m_Transform = transform;
        m_Local = local;
        m_Target = rotation;
        m_Start = local ? m_Transform.localRotation : m_Transform.rotation;

        if (m_Start == m_Target)
        {
            End();
        }
    }

    public void Initialise(Vector3 angles, bool local)
    {
        Initialise(Quaternion.Euler(angles.x, angles.y, angles.z), local);
    }

    public void Initialise(float x, float y, float z, bool local)
    {
        Initialise(Quaternion.Euler(x, y, z), local);
    }
}