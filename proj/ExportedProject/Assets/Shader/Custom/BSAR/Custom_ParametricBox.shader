Shader "Custom/ParametricBox" {
	Properties {
		_Color ("Color", Vector) = (1,0,0,0)
		_SizeParams ("Width, Length and Center", Vector) = (0.2,2,0.5,0)
		_FogStartOffset ("Fog Start Offset", Float) = 1
		_FogScale ("Fog Scale", Float) = 1
		_Fuckery ("Fuckery", Vector) = (0,0,0,0)
	}
	SubShader {
		Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend One One, One One
			ZWrite Off
			GpuProgramID 56141
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float3 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float _CustomFogAttenuation;
			float _CustomFogOffset;
			float _FogStartOffset;
			float _FogScale;
			float4 _Fuckery;
			// Custom ConstantBuffers for Vertex Shader
			CBUFFER_START(MyProperties)
				float3 _SizeParams;
			CBUFFER_END
			// Custom ConstantBuffers for Fragment Shader
			CBUFFER_START(MyProperties)
				float4 _Color;
			CBUFFER_END
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                tmp0.x = v.vertex.y - _SizeParams + _Fuckery;
                tmp0.x = tmp0.x / _SizeParams.yy + _Fuckery.yy;
                tmp0 = tmp0.xxxx * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp1.xy = v.vertex.xz * _SizeParams.x + _Fuckery.x;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * tmp1.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * tmp1.yyyy + tmp0;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                o.texcoord1.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp0 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp0 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp0;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp0;
                o.position = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp0;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                tmp0.xyz = inp.texcoord1.xyz - _WorldSpaceCameraPos;
                tmp0.x = dot(tmp0.xyz, tmp0.xyz);
                tmp0.x = sqrt(tmp0.x);
                tmp0.x = tmp0.x - _FogStartOffset;
                tmp0.x = max(tmp0.x, 0.0);
                tmp0.x = tmp0.x * _FogScale + -_CustomFogOffset;
                tmp0.x = max(tmp0.x, 0.0);
                tmp0.x = min(tmp0.x, 9999.0);
                tmp0.x = -tmp0.x * _CustomFogAttenuation;
                tmp0.x = tmp0.x * 1.442695;
                tmp0.x = exp(tmp0.x);
                tmp0.x = 1.0 - tmp0.x;
                tmp0.x = max(tmp0.x, 0.0);
                tmp0.x = 1.0 - tmp0.x;
                tmp0.x = tmp0.x * _Color.w;
                o.sv_target.xyz = tmp0.xxx * _Color.xyz;
                o.sv_target.w = tmp0.x;
                return o;
			}
			ENDCG
		}
	}
}