using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class CanvasFadeTween : FadeTween<Graphic>
{
    protected override List<Graphic> GetObjects()
    {
        return GetComponentsInChildren<Graphic>().ToList();
    }

    protected override float GetAlpha(Graphic obj)
    {
        return obj.color.a;
    }

    protected override void SetAlpha(Graphic obj, float alpha)
    {
        //obj.SetAlpha(alpha);
    }
}