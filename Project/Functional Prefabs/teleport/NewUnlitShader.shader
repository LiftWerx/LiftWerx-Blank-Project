Shader "Unlit/VerticalGradient"
{
    Properties
    {
        _TopColour("Top Gradient Colour: ", Color) = (1,1,1,0)
        _BottomColour("Bottom Gradient Colour: ", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha // Enable blending for transparency

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
                float4 colour : COLOR; // Changed TEXCOORD0 to COLOR for fragment shader output
                float4 vertex : SV_POSITION;
            };

            fixed4 _TopColour;
            fixed4 _BottomColour;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // Interpolate the colour including alpha channel
                o.colour = lerp(_BottomColour, _TopColour, v.uv.y);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return i.colour;
            }
            ENDCG
        }
    }
}
