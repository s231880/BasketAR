using System.Collections.Generic;
using UnityEngine;

public class SpriteColourTween : ColourTween<SpriteRenderer>
{
    //protected override List<SpriteRenderer> GetObjects()
    //{
    //    //return GetComponentsInChildren<SpriteRenderer>().ToList();
    //}

    protected override List<SpriteRenderer> GetObjects()
    {
        //return GetComponentsInChildren<SpriteRenderer>().ToList();
        return null;
    }

    protected override Color GetColour(SpriteRenderer obj)
    {
        return obj.color;
    }

    protected override void SetColour(SpriteRenderer obj, Color colour)
    {
        obj.color = colour;
    }
}