using System.Collections.Generic;
using UnityEngine;

public class MaterialFadeTween : FadeTween<Material>
{
    protected override List<Material> GetObjects()
    {
        // return GetComponentsInChildren<Renderer>().ToList().Extract(r => r.material);
        return null;
    }

    protected override float GetAlpha(Material obj)
    {
        return obj.color.a;
    }

    protected override void SetAlpha(Material obj, float alpha)
    {
        //obj.SetAlpha(alpha);
    }
}