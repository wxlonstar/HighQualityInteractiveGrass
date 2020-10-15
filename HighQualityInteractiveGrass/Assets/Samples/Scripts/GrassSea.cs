﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSea : MonoBehaviour
{
    #region fields
    [SerializeField]
    private Texture2D windNoise;
    [SerializeField]
    private Vector3 uniformWindDirection;
    [SerializeField, Range(0, 1)]
    private float uniformWindStrength;
    [SerializeField]    
    private Rect worldRect;
    [SerializeField, Range(0, 1)]
    private float windSpeed = 0.1f;
    private Vector2 uvOffset = new Vector2(0, 0);
    #endregion

    #region unity methods
    private void Update()
    {
        Vector2 windDirectionXZ = new Vector2(uniformWindDirection.x, uniformWindDirection.z).normalized;
        uvOffset += windDirectionXZ * windSpeed * Time.deltaTime;
        uvOffset = new Vector2(uvOffset.x - Mathf.Floor(uvOffset.x), uvOffset.y - Mathf.Floor(uvOffset.y));
        Vector3 windDirection = new Vector4(uniformWindDirection.x, 0, uniformWindDirection.z).normalized;
        Shader.SetGlobalTexture("_WindNoise", windNoise);
        Shader.SetGlobalVector("_UniformWindEffect", new Vector4(windDirection.x, windDirection.y, windDirection.z, uniformWindStrength));
        Shader.SetGlobalVector("_worldRect", new Vector4(worldRect.x, worldRect.y, worldRect.width, worldRect.height));
        Shader.SetGlobalVector("_uvOffset", uvOffset);
    }
    #endregion
}