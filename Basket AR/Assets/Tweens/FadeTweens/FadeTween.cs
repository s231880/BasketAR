using System.Collections.Generic;
using UnityEngine;

public abstract class FadeTween<T> : Tween where T : Object
{
    private List<float> m_Start;
    private List<T> m_Objects;
    private float m_Target;

    private void Awake()
    {
        m_Objects = GetObjects();

        m_Start = new List<float>(m_Objects.Count);
        m_Objects.ForEach(obj => m_Start.Add(GetAlpha(obj)));
    }

    protected abstract List<T> GetObjects();

    protected abstract void SetAlpha(T obj, float alpha);

    protected abstract float GetAlpha(T obj);

    protected override void OnStep(float t)
    {
        for (int i = 0; i < m_Objects.Count; ++i)
        {
            float alpha = Mathf.Lerp(m_Start[i], m_Target, t);
            SetAlpha(m_Objects[i], alpha);
        }
    }

    public void Exclude(params string[] names)
    {
        for (int i = 0; i < names.Length; ++i)
        {
            int index = m_Objects.FindIndex(obj => obj.name == names[i]);
            if (index >= 0)
            {
                m_Objects.RemoveAt(index);
                m_Start.RemoveAt(index);
            }
            else
            {
                Debug.LogWarning("Could not find " + names[i], gameObject);
            }
        }
    }

    public void Initialise(float alpha, float? start = null)
    {
        if (m_Objects == null)
        {
            Awake();
        }

        m_Target = alpha;

        if (start != null)
        {
            for (int i = 0; i < m_Start.Count; ++i)
            {
                m_Start[i] = start.Value;
            }
        }
        Step(0f);
    }
}