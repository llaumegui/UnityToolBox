Shader "Custom/Sprites/InlineShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _OutlineColor("Inline Color",Color) = (1,1,1,1)
        _OutlineSize("Inline Size",Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }

            Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color:COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            fixed4 _Color;
            fixed4 _OutlineColor;
            float _OutlineSize;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= i.color;

                fixed leftPixel = tex2D(_MainTex, i.uv + float2(-_MainTex_TexelSize.x * _OutlineSize, 0)).a;
                fixed rightPixel = tex2D(_MainTex, i.uv + float2(_MainTex_TexelSize.x* _OutlineSize, 0)).a;
                fixed botPixel = tex2D(_MainTex, i.uv + float2(0,-_MainTex_TexelSize.y * _OutlineSize)).a;
                fixed upPixel = tex2D(_MainTex, i.uv + float2(0,_MainTex_TexelSize.y * _OutlineSize)).a;

                fixed outline = (1-leftPixel*upPixel*rightPixel*botPixel)* col.a;

                return lerp(col, _OutlineColor, outline);
            }
            ENDCG
        }
    }
}
