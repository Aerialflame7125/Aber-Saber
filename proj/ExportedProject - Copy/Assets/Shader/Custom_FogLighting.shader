Shader "Custom/FogLighting" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		[Space] _FogLightingStrength ("Fog Lighting Strength", Float) = 0.1
		[Space] _FogStartOffset ("Fog Start Offset", Float) = 0
		_FogScale ("Fog Scale", Float) = 1
	}
	SubShader {
		Tags { "QUEUE" = "Geometry" "RenderType" = "Opaque" }
		Pass {
			Tags { "QUEUE" = "Geometry" "RenderType" = "Opaque" }
			GpuProgramID 9843
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
			// $Globals ConstantBuffers for Fragment Shader
			float4 _CustomFogColor;
			float _CustomFogColorMultiplier;
			float _CustomFogAttenuation;
			float _CustomFogOffset;
			float2 _GlobalBlueNoiseParams;
			float _GlobalRandomValue;
			float4 _Color;
			float _FogLightingStrength;
			float _FogStartOffset;
			float _FogScale;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _GlobalBlueNoiseTex;
			
			// Keywords: 
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
                tmp1.x = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp1.y = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp1.z = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp1.w = dot(tmp1.xyz, tmp1.xyz);
                tmp1.w = rsqrt(tmp1.w);
                o.texcoord2.xyz = tmp1.www * tmp1.xyz;
                tmp0.y = tmp0.y * _ProjectionParams.x;
                tmp1.xzw = tmp0.xwy * float3(0.5, 0.5, 0.5);
                o.texcoord3.zw = tmp0.zw;
                o.texcoord3.xy = tmp1.zz + tmp1.xw;
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
                tmp0 = tmp0.xxxx * tmp2 + tmp1;
                tmp1.xy = inp.texcoord3.xy / inp.texcoord3.ww;
                tmp1.xy = tmp1.xy * _GlobalBlueNoiseParams + _GlobalRandomValue.xx;
                tmp1 = tex2D(_GlobalBlueNoiseTex, tmp1.xy);
                tmp1.x = tmp1.w - 0.5;
                o.sv_target.xyz = tmp1.xxx * float3(0.0039063, 0.0039063, 0.0039063) + tmp0.xyz;
                o.sv_target.w = tmp0.w;
                return o;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}