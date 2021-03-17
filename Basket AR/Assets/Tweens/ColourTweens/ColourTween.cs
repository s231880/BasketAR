using System.Collections.Generic;
using UnityEngine;

public abstract class ColourTween<T> : Tween where T : UnityEngine.Object
{
    private List<T> m_Objects;
    private List<Color> m_Start;
    private Color m_Target;

    private void Awake()
    {
        m_Objects = GetObjects();
        //m_Start = m_Objects.Extract(o => GetColour(o));
    }

    protected abstract List<T> GetObjects();

    protected abstract void SetColour(T obj, Color alpha);

    protected abstract Color GetColour(T obj);

    protected override void OnStep(float t)
    {
        for (int i = 0; i < m_Objects.Count; ++i)
        {
            var colour = Color.Lerp(m_Start[i], m_Target, t);
            SetColour(m_Objects[i], colour);
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

    public void Initialise(Color colour, Color? start = null)
    {
        m_Target = colour;

        if (start != null)
        {
            for (int i = 0; i < m_Start.Count; ++i)
            {
                m_Start[i] = start.Value;
            }
        }

        // check if colours already match
        var coloursMatch = true;
        for (int i = 0; i < m_Start.Count; ++i)
        {
            if (m_Start[i] != m_Target)
            {
                coloursMatch = false;
            }
        }

        if (coloursMatch == true)
        {
            End();
        }
    }
}