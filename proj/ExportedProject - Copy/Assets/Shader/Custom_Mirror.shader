Shader "Custom/Mirror" {
	Properties {
		_NormalTex ("Normal Texture", 2D) = "white" {}
		_BumpIntensity ("Bump Intensity", Float) = 0.1
		[Space] [Toggle(_ENABLE_DIRT_TEX)] _EnableDirtTex ("Enable Dirt", Float) = 0
		_DirtTex ("Dirt Texture", 2D) = "white" {}
		[Space] _TintColor ("Tint Color", Color) = (1,1,1,1)
		[Space] _FogStartOffset ("Fog Start Offset", Float) = 0
		_FogScale ("Fog Scale", Float) = 1
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		Pass {
			Tags { "LIGHTMODE" = "FORWARDBASE" "RenderType" = "Opaque" }
			GpuProgramID 17889
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 texcoord1 : TEXCOORD1;
				float2 texcoord2 : TEXCOORD2;
				float3 texcoord3 : TEXCOORD3;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _NormalTex_ST;
			// $Globals ConstantBuffers for Fragment Shader
			float4 _CustomFogColor;
			float _CustomFogColorMultiplier;
			float _CustomFogAttenuation;
			float _CustomFogOffset;
			float _BumpIntensity;
			float4 _TintColor;
			float _FogStartOffset;
			float _FogScale;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _NormalTex;
			sampler2D _ReflectionTex;
			
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
                o.texcoord3.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp0 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp0 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp0;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp0;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp0;
                o.position = tmp0;
                tmp0.y = tmp0.y * _ProjectionParams.x;
                tmp1.xzw = tmp0.xwy * float3(0.5, 0.5, 0.5);
                o.texcoord1.zw = tmp0.zw;
                o.texcoord1.xy = tmp1.zz + tmp1.xw;
                o.texcoord2.xy = v.texcoord.xy * _NormalTex_ST.xy + _NormalTex_ST.zw;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                tmp0.xyz = inp.texcoord3.xyz - _WorldSpaceCameraPos;
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
                tmp1 = tex2D(_NormalTex, inp.texcoord2.xy);
                tmp1.x = tmp1.w * tmp1.x;
                tmp0.yz = tmp1.xy * float2(2.0, 2.0) + float2(-1.0, -1.0);
                tmp1.xy = inp.texcoord1.xy / inp.texcoord1.ww;
                tmp0.yz = tmp0.yz * _BumpIntensity.xx + tmp1.xy;
                tmp1 = tex2D(_ReflectionTex, tmp0.yz);
                tmp1 = tmp1 * _TintColor;
                tmp0.yzw = _CustomFogColor.xyz * _CustomFogColorMultiplier.xxx + -tmp1.xyz;
                o.sv_target.xyz = tmp0.xxx * tmp0.yzw + tmp1.xyz;
                o.sv_target.w = tmp1.w;
                return o;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}