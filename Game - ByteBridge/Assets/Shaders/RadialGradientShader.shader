Shader "Custom/RadialGradientShader"
{
    Properties
    {
        _Center ("Center", Vector) = (0.5, 0.5, 0, 0)
        _Color1 ("Center Color", Color) = (1, 1, 1, 1)
        _Color2 ("Outer Color", Color) = (0.6, 0.6, 0.6, 1)
        _Radius ("Radius", Float) = 1
    }
    
    SubShader
    {
        Tags { "Queue" = "Background" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _Center;
            float4 _Color1;
            float4 _Color2;
            float _Radius;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xy;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float dist = length(i.uv - _Center.xy) / _Radius;
                float4 col = lerp(_Color1, _Color2, dist);
                return col;
            }
            ENDCG
        }
    }
}
