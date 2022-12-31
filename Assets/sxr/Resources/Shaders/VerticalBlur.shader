﻿Shader "VerticalBlur"{
    Properties{_MainTex ("Texture", 2D) = "white" {}}
    
    SubShader{
	    Pass{
	    	HLSLPROGRAM
	    	
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
	    	
	    	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			struct appdata{ 
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;};

            struct v2f { 
                float4 vertex : SV_POSITION; 
                float2 uv : TEXCOORD0; };

            v2f vert (appdata v){ 
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex); 
                o.uv = v.uv; 
                return o; }
	    	
	    	sampler2D _MainTex;
            float2 _MainTex_TexelSize;
	    	int	kernelSize;

			float4 frag(v2f i) : SV_Target{
				kernelSize = 21;
				
				float3 col;

				const int max = ((kernelSize - 1) / 2);
				const int min = -max;

				for (int y = min; y <= max; ++y)
					col += tex2D(_MainTex, i.uv + float2(0.0, _MainTex_TexelSize.y * y));

				return float4(col/kernelSize, 1.0);}
	    	
	    	ENDHLSL
		}
    }
}