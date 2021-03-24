using System;
using System.Linq;
using UnityEngine;

public static class TweenExtensions
{
    private const EaseType DefaultEase = EaseType.Linear;

    public static T Create<T>(this GameObject go, float duration, EaseType ease, Action<float> onTick, Action callback) where T : Tween => Tween.Create<T>(go, duration, ease, onTick, callback);

    public static T Create<T>(this GameObject go, float duration, Action<float> onTick, Action callback) where T : Tween => go.Create<T>(duration, DefaultEase, callback);

    public static T Create<T>(this GameObject go, float duration, EaseType ease, Action<float> onTick) where T : Tween => go.Create<T>(duration, ease, onTick, null);

    public static T Create<T>(this GameObject go, float duration, EaseType ease, Action callback) where T : Tween => go.Create<T>(duration, ease, null, callback);

    public static T Create<T>(this GameObject go, float duration, Action<float> onTick) where T : Tween => go.Create<T>(duration, DefaultEase, onTick, null);

    public static T Create<T>(this GameObject go, float duration, Action callback) where T : Tween => go.Create<T>(duration, DefaultEase, callback);

    public static T Create<T>(this GameObject go, float duration, EaseType ease) where T : Tween => go.Create<T>(duration, ease, null, null);

    public static T Create<T>(this GameObject go, float duration) where T : Tween => go.Create<T>(duration, DefaultEase);

    public static void StopTweens<T>(this GameObject go) where T : Tween => go.GetComponents<T>().ToList().ForEach(t => t.Stop());

    public static void StopAllTweens(this GameObject go) => go.StopTweens<Tween>();

    public static void CancelTweens<T>(this GameObject go) where T : Tween => go.GetComponents<T>().ToList().ForEach(t => t.Cancel());

    public static void CancelAllTweens(this GameObject go) => go.CancelTweens<Tween>();

    public static T Create<T>(this Component component, float duration, EaseType ease, Action<float> onTick, Action callback) where T : Tween => component.gameObject.Create<T>(duration, ease, onTick, callback);

    public static T Create<T>(this Component component, float duration, Action<float> onTick, Action callback) where T : Tween => component.gameObject.Create<T>(duration, DefaultEase, callback);

    public static T Create<T>(this Component component, float duration, EaseType ease, Action<float> onTick) where T : Tween => component.gameObject.Create<T>(duration, ease, onTick, null);

    public static T Create<T>(this Component component, float duration, EaseType ease, Action callback) where T : Tween => component.gameObject.Create<T>(duration, ease, null, callback);

    public static T Create<T>(this Component component, float duration, Action<float> onTick) where T : Tween => component.gameObject.Create<T>(duration, DefaultEase, onTick, null);

    public static T Create<T>(this Component component, float duration, Action callback) where T : Tween => component.gameObject.Create<T>(duration, DefaultEase, callback);

    public static T Create<T>(this Component component, float duration, EaseType ease) where T : Tween => component.gameObject.Create<T>(duration, ease, null, null);

    public static T Create<T>(this Component component, float duration) where T : Tween => component.gameObject.Create<T>(duration, DefaultEase);

    public static void StopTweens<T>(this Component component) where T : Tween => component.gameObject.StopTweens<T>();

    public static void StopAllTweens(this Component component) => component.gameObject.StopAllTweens();

    public static void CancelAllTweens<T>(this Component component) where T : Tween => component.gameObject.CancelTweens<T>();

    public static void CancelAllTweens(this Component component) => component.gameObject.CancelAllTweens();
}