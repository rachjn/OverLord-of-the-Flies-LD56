Shader "Custom/HueShiftShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _HueShift ("Hue Shift", Range(0, 1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _HueShift;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float3 HueShift(float3 color, float hue)
            {
                float angle = hue * 6.28318530718; // 2 * PI
                float cosAngle = cos(angle);
                float sinAngle = sin(angle);
                float3 rgb = color.rgb;

                float3 shifted;
                shifted.r = dot(rgb, float3(0.299, 0.587, 0.114)) + cosAngle * (rgb.r - rgb.g) + sinAngle * (rgb.b - rgb.r);
                shifted.g = dot(rgb, float3(0.299, 0.587, 0.114)) + cosAngle * (rgb.g - rgb.b) + sinAngle * (rgb.r - rgb.g);
                shifted.b = dot(rgb, float3(0.299, 0.587, 0.114)) + cosAngle * (rgb.b - rgb.r) + sinAngle * (rgb.g - rgb.b);
                
                return shifted;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = HueShift(col.rgb, _HueShift);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
