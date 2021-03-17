using UnityEngine;

public class ScaleTween : Tween
{
    private Transform m_Transform;
    private Vector3 m_Target;
    private Vector3 m_Start;

    protected override void OnStep(float t)
    {
        m_Transform.localScale = Vector3.LerpUnclamped(m_Start, m_Target, t);
    }

    public void Initialise(Vector3 scale)
    {
        m_Target = scale;
        m_Transform = transform;
        m_Start = m_Transform.localScale;
    }

    public void Initialise(float scale)
    {
        Initialise(new Vector3(scale, scale, scale));
    }
}