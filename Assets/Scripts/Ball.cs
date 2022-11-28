using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : ActiveItem
{
    [SerializeField] private BallSettings _ballSettings;
    [SerializeField] private Renderer _renderer;


    public override void SetLevel(int level)
    {
        base.SetLevel(level);

        _renderer.material = _ballSettings.BallMaterials[level];

        Projection.Setup(_ballSettings.BallProjectionMaterials[level],LevelText.text,Radius);
    }
}
