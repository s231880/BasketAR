using System.Collections.Generic;
using UnityEngine;

public class MaterialColourTween : ColourTween<Material>
{
    protected override List<Material> GetObjects()
    {
        //return GetComponentsInChildren<Renderer>().ToList().Extract(r => r.material);
        return null;
    }

    protected override Color GetColour(Material obj)
    {
        return obj.color;
    }

    protected override void SetColour(Material obj, Color colour)
    {
        obj.color = colour;
    }
}