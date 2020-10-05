﻿Shader "Windsmoon/HighQualityInteractiveGrass/Grass"
{
    Properties
    {
        _GrassColor("Texture", 2D) = "white" {}
        _GrassMask("Texture", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags { "Queue" = "AlphaTest"  "RenderType" = "TransparentCutout" }

        Pass
        {
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _GrassColor;
            sampler2D _GrassMask;
            float4 _GrassColor_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _GrassColor);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_GrassColor, i.uv);
                fixed mask = tex2D(_GrassMask, i.uv).r;
                clip(mask - 0.0001f);
                return col;
            }
            
            ENDCG
        }
    }
}