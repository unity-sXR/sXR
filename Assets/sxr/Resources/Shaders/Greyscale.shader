Shader "Greyscale" {
    Properties{ _MainTex ("Texture", 2D) = "white" {} }
    
    SubShader{
        Pass{
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag#pragma fragmentoption ARB_precision_hint_fastest
	    	
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

            float4 frag (v2f i) : SV_Target{
                float3 colors = tex2D(_MainTex, i.uv).rgb;
                float lumValue = colors.r*.3 + colors.g*.59 + colors.b*.11;
                return float4(lumValue, lumValue, lumValue, lumValue);}
            
            ENDHLSL
        } 
    }
}
