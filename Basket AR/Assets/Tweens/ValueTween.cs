using System;
using UnityEngine;

public class ValueTween : Tween
{
    private Action<float> m_Handler;
    private float m_From;
    private float m_To;

    protected override void OnStep(float t)
    {
        if (m_Handler != null)
        {
            m_Handler(Mathf.LerpUnclamped(m_From, m_To, t));
        }
        else
        {
            Debug.LogError("Handler is somehow null, on " + name, this);
        }
    }

    public void Initialise(float from, float to, Action<float> handler)
    {
        m_Handler = handler;
        m_From = from;
        m_To = to;

        if (m_From == m_To)
        {
            End();
        }
    }
}