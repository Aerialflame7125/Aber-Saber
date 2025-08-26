Shader "Custom/Glowing" {
	Properties {
		_Color ("Color", Color) = (1,0,0,0)
		_FogStartOffset ("Fog Start Offset", Float) = 0
		_FogScale ("Fog Scale", Float) = 1
		[Space] [KeywordEnum(None, Normal, Z)] _CUTOUT ("Cutout Mode", Float) = 0
		[Space] _Cutout ("Cutout", Range(0, 1)) = 0
		[Space] _CutoutTexScale ("Cutout Texture Scale", Float) = 1
		_CutoutTexOffset ("Cutout Texture Offset", Vector) = (0,0,0,0)
		[HideInInspector] _CutoutTex ("Cutout Texture", 3D) = "white" {}
		[Space] [Toggle(_NOISE_DITHERING)] _NoiseDithering ("Noise Dithering", Float) = 0
	}
	SubShader {
		Tags { "QUEUE" = "Geometry" "RenderType" = "Opaque" }
		Pass {
			Tags { "QUEUE" = "Geometry" "RenderType" = "Opaque" }
			GpuProgramID 17375
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float3 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float4 _CustomFogColor;
			float _CustomFogColorMultiplier;
			float _CustomFogAttenuation;
			float _CustomFogOffset;
			float4 _Color;
			float _FogStartOffset;
			float _FogScale;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			
			// Keywords: _CUTOUT_NONE
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                o.texcoord1.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp0 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp0 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp0;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp0;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp0;
                o.position = tmp0;
                tmp0.y = tmp0.y * _ProjectionParams.x;
                tmp1.xzw = tmp0.xwy * float3(0.5, 0.5, 0.5);
                o.texcoord2.zw = tmp0.zw;
                o.texcoord2.xy = tmp1.zz + tmp1.xw;
                return o;
			}
			// Keywords: _CUTOUT_NONE
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                tmp0.xyz = inp.texcoord1.xyz - _WorldSpaceCameraPos;
                tmp0.x = dot(tmp0.xyz, tmp0.xyz);
                tmp0.x = sqrt(tmp0.x);
                tmp0.x = -_Color.w * _FogStartOffset + tmp0.x;
                tmp0.x = max(tmp0.x, 0.0);
                tmp0.y = _FogScale - 1.0;
                tmp0.y = _Color.w * tmp0.y + 1.0;
                tmp0.x = tmp0.x * tmp0.y + -_CustomFogOffset;
                tmp0.x = max(tmp0.x, 0.0);
                tmp0.x = min(tmp0.x, 9999.0);
                tmp0.x = -tmp0.x * _CustomFogAttenuation;
                tmp0.x = tmp0.x * 1.442695;
                tmp0.x = exp(tmp0.x);
                tmp0.x = 1.0 - tmp0.x;
                tmp0.x = max(tmp0.x, 0.0);
                tmp1 = _CustomFogColor * _CustomFogColorMultiplier.xxxx + -_Color;
                o.sv_target = tmp0.xxxx * tmp1 + _Color;
                return o;
			}
			ENDCG
		}
	}
}