using System;
using UnityEngine;

public abstract class Tween : MonoBehaviour
{
    public static T Create<T>(GameObject go, float duration, EaseType ease, Action<float> onTick, Action callback) where T : Tween
    {
        var tween = go.AddComponent<T>();
        if (go.activeInHierarchy == true)
        {
            tween.m_EndTime = Time.time + duration;
            tween.m_StartTime = Time.time;
            tween.m_Callback = callback;
            tween.m_OnTick = onTick;
            tween.m_EaseType = ease;
        }
        else
        {
            callback?.Invoke();
            tween.Stop();
        }
        return tween;
    }

    private Action<float> m_OnTick;
    private Action m_Callback;

    private EaseType m_EaseType;
    private float m_StartTime;
    private float m_EndTime;
    private bool m_Disabled;

    private void OnDisable()
    {
        if (m_Disabled == false)
        {
            End();
        }
    }

    private void Update()
    {
        if (IsDone() == false)
        {
            Step(ComputeT());
        }
        else
        {
            End();
        }
    }

    public void Stop()
    {
        if (this != null)
        {
            this.enabled = false;
            //this.DestroyObject();
            Destroy(this);
        }
    }

    public void Cancel()
    {
        m_Callback = null;
        Stop();
    }

    protected void Step(float t)
    {
        OnStep(t);
        m_OnTick?.Invoke(t);
    }

    protected abstract void OnStep(float t);

    protected virtual bool IsDone()
    {
        return Time.time >= m_EndTime;
    }

    protected void End()
    {
        Step(1f);
        m_Callback?.Invoke();
        m_Callback = null;
        m_Disabled = true;
        Stop();
    }

    private float ComputeT()
    {
        return m_EaseType.Ease(Mathf.InverseLerp(m_StartTime, m_EndTime, Time.time));
    }
}