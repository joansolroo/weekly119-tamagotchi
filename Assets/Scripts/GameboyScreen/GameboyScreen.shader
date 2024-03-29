﻿Shader "Postprocessing/NewImageEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Width("Width", Int) = 32
		_Height("Height", Int) = 16
		_Padding("Padding", Float) = 0.1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			fixed4 _MainTex_TexelSize;
			int _Width;
			int _Height;
			float _Padding;
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
				if((i.uv.x* _Width)%1< _Padding)
				{
					col.rgb = 0;
				}else if ((i.uv.y* _Height) % 1 < _Padding)
				{
					col.rgb = 0;
				}
                return col;
            }
            ENDCG
        }
    }
}
