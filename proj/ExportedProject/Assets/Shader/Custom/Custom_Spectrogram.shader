Shader "Custom/Spectrogram" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_PeakOffset ("Peak Offset", Vector) = (0,10,0,1)
		[Space] _FogLightingStrength ("Fog Lighting Strength", Float) = 0.1
		[Space] _FogStartOffset ("Fog Start Offset", Float) = 0
		_FogScale ("Fog Scale", Float) = 1
		[PerRendererData] _DataTex ("Data Texture", 2D) = "white" {}
	}
	SubShader {
		Tags { "DisableBatching" = "true" "QUEUE" = "Geometry" "RenderType" = "Opaque" }
		Pass {
			Tags { "DisableBatching" = "true" "QUEUE" = "Geometry" "RenderType" = "Opaque" }
			GpuProgramID 20907
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float3 texcoord1 : TEXCOORD1;
				float3 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float _StereoCameraEyeOffset;
			float3 _PeakOffset;
			// $Globals ConstantBuffers for Fragment Shader
			float4 _CustomFogColor;
			float _CustomFogColorMultiplier;
			float _CustomFogAttenuation;
			float _CustomFogOffset;
			float4 _Color;
			float _FogLightingStrength;
			float _FogStartOffset;
			float _FogScale;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			sampler2D _DataTex;
			// Texture params for Fragment Shader
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                tmp0.x = v.texcoord.x;
                tmp0.y = 0.0;
                tmp0 = tex2Dlod(_DataTex, float4(tmp0.xy, 0, 0.0));
                tmp0.x = 1.0 - tmp0.x;
                tmp0.xyz = tmp0.xxx * _PeakOffset;
                tmp0.xyz = -tmp0.xyz * v.texcoord.yyy + v.vertex.xyz;
                tmp1 = tmp0.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp1 = unity_ObjectToWorld._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                o.texcoord1.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp0 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp0 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp0;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp0;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp0;
                o.position = tmp0;
                tmp1.x = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp1.y = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp1.z = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp1.w = dot(tmp1.xyz, tmp1.xyz);
                tmp1.w = rsqrt(tmp1.w);
                o.texcoord2.xyz = tmp1.www * tmp1.xyz;
                tmp0.y = tmp0.y * _ProjectionParams.x;
                tmp1.xzw = tmp0.xwy * float3(0.5, 0.5, 0.5);
                tmp0.xy = tmp1.zz + tmp1.xw;
                tmp1.x = floor(unity_StereoEyeIndex);
                tmp1.y = _StereoCameraEyeOffset + _StereoCameraEyeOffset;
                tmp1.x = tmp1.x * tmp1.y + -_StereoCameraEyeOffset;
                o.texcoord3.x = tmp0.w * tmp1.x + tmp0.x;
                o.texcoord3.yzw = tmp0.yzw;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
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
                tmp0.y = dot(inp.texcoord1.xyz, inp.texcoord1.xyz);
                tmp0.y = rsqrt(tmp0.y);
                tmp0.yzw = tmp0.yyy * inp.texcoord1.xyz;
                tmp0.y = dot(tmp0.xyz, inp.texcoord2.xyz);
                tmp0.y = 1.0 - abs(tmp0.y);
                tmp0.y = tmp0.y * _FogLightingStrength;
                tmp1 = _CustomFogColor * _CustomFogColorMultiplier.xxxx + -_Color;
                tmp1 = tmp0.yyyy * tmp1 + _Color;
                tmp2 = _CustomFogColor * _CustomFogColorMultiplier.xxxx + -tmp1;
                o.sv_target = tmp0.xxxx * tmp2 + tmp1;
                return o;
			}
			ENDCG
		}
		Pass {
			Tags { "DisableBatching" = "true" "LIGHTMODE" = "SHADOWCASTER" "QUEUE" = "Geometry" "RenderType" = "Opaque" }
			GpuProgramID 83623
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float3 texcoord1 : TEXCOORD1;
				float3 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float _StereoCameraEyeOffset;
			float3 _PeakOffset;
			// $Globals ConstantBuffers for Fragment Shader
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			sampler2D _DataTex;
			// Texture params for Fragment Shader
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                tmp0.x = v.texcoord.x;
                tmp0.y = 0.0;
                tmp0 = tex2Dlod(_DataTex, float4(tmp0.xy, 0, 0.0));
                tmp0.x = 1.0 - tmp0.x;
                tmp0.xyz = tmp0.xxx * _PeakOffset;
                tmp0.xyz = -tmp0.xyz * v.texcoord.yyy + v.vertex.xyz;
                tmp1 = tmp0.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp1 = unity_ObjectToWorld._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                o.texcoord1.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp0 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp0 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp0;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp0;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp0;
                o.position = tmp0;
                tmp1.x = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp1.y = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp1.z = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp1.w = dot(tmp1.xyz, tmp1.xyz);
                tmp1.w = rsqrt(tmp1.w);
                o.texcoord2.xyz = tmp1.www * tmp1.xyz;
                tmp0.y = tmp0.y * _ProjectionParams.x;
                tmp1.xzw = tmp0.xwy * float3(0.5, 0.5, 0.5);
                tmp0.xy = tmp1.zz + tmp1.xw;
                tmp1.x = floor(unity_StereoEyeIndex);
                tmp1.y = _StereoCameraEyeOffset + _StereoCameraEyeOffset;
                tmp1.x = tmp1.x * tmp1.y + -_StereoCameraEyeOffset;
                o.texcoord3.x = tmp0.w * tmp1.x + tmp0.x;
                o.texcoord3.yzw = tmp0.yzw;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                o.sv_target = float4(0.0, 0.0, 0.0, 1.0);
                return o;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}