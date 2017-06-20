Shader "Custom/Glowing"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Color("Color", Color) = (0.8455882,0.2051795,0.2051795,1)
		_Frequency("Frequency", Range( 0 , 100)) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			fixed filler;
		};

		uniform float _Frequency;
		uniform float4 _Color;

		inline fixed4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return fixed4 ( s.Emission, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float mulTime2 = _Time.y * _Frequency;
			o.Emission = ( ( ( sin( mulTime2 ) + 1.0 ) / 2.0 ) * _Color ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}