Shader "Custom/ScreenDisplacement" {
	Properties {
		_MainTex ("Displacement Texture", 2D) = "white" {}
		_DisplacementStrength ("Displacement Strength", Float) = 0.01
		[Space] [Enum(UnityEngine.Rendering.CullMode)] _CullMode ("CullMode", Float) = 0
		[Space] _TintColor ("Tint Color", Vector) = (1,1,1,1)
		_AddColor ("Add Color", Vector) = (0,0,0,0)
		[Space] [Toggle(_FADE_Z)] _FadeZ ("Fade Z", Float) = 0
		_FadeZThreshold ("Fade Z Threshold", Float) = -1
		_FadeZWidth ("Fade Z Width", Float) = 3
		[Space] [Toggle(_ENABLE_FOG)] _EnableFog ("Enable Fog", Float) = 0
		_FogStartOffset ("Fog Start Offset", Float) = 0
		_FogScale ("Fog Scale", Float) = 1
		[Space] [Toggle(_ZWRITE)] _ZWrite ("Z Write", Float) = 0
		[Space] [Toggle(_CLIP_LOW_ALPHA)] _ClipLowAlpha ("Clip Low Alpha", Float) = 1
		[Space] [Toggle(_SOFT_EDGES)] _SoftEdges ("Soft Edges", Float) = 1
		_SoftFactor ("Soft Factor", Range(0, 5)) = 0
		[Space] [Toggle(_VIEW_ANGLE_AFFECTS_DISTORTION)] _ViewAngleAffectsDistortion ("View Angle Affects Distortion", Float) = 1
		[Space] [Toggle(_ENABLE_CUTOUT)] _EnableCutout ("Enable Cutout", Float) = 0
		_CutoutTexScale ("Cutout Texture Scale", Float) = 1
		[PerRendererData] _CutoutTexOffset ("Cutout Texture Offset", Vector) = (0,0,0,0)
		[PerRendererData] _Cutout ("Cutout", Range(0, 1)) = 0
	}
	SubShader {
		Tags { "DisableBatching" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent+1" "RenderType" = "Transparent" }
		GrabPass {
			"_GrabTexture1"
		}
		Pass {
			Tags { "DisableBatching" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent+1" "RenderType" = "Transparent" }
			ZWrite Off
			Cull Off
			GpuProgramID 22041
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature ENABLE_QUEST_OPTIMIZATIONS

			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float2 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_ST;
			// $Globals ConstantBuffers for Fragment Shader
			float4 _GrabTexture1_TexelSize;
			float4 _BloomPrePassTexture_TexelSize;
			float _DisplacementStrength;
			float4 _TintColor;
			float4 _AddColor;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			uniform sampler2D _BloomPrePassTexture;
			sampler2D _GrabTexture1;
			
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
                o.texcoord3 = unity_ObjectToWorld._m03_m13_m23_m33 * v.vertex.wwww + tmp0;
                tmp0 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp0 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp0;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp0;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp0;
                o.position = tmp0;
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                o.texcoord1 = v.color;
                tmp1.xyz = tmp0.xwy * float3(0.5, 0.5, -0.5);
                o.texcoord2.zw = tmp0.zw;
                o.texcoord2.xy = tmp1.yy + tmp1.xz;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
				#ifdef ENABLE_QUEST_OPTIMIZATIONS
				{
					fout o;
                	float4 tmp0;
                	float4 tmp1;
                	float4 tmp2;
                	tmp0.xyz = inp.texcoord3.xyz - _WorldSpaceCameraPos;
                	tmp0.x = dot(tmp0.xyz, tmp0.xyz);
                	tmp0.x = sqrt(tmp0.x);
                	tmp0.x = tmp0.x + 1.0;
                	tmp0.x = 1.0 / tmp0.x;
                	tmp1 = tex2D(_MainTex, inp.texcoord.xy);
                	tmp0.yz = tmp1.xy - float2(0.5, 0.5);
                	tmp0.w = tmp1.w * inp.texcoord1.w;
                	tmp0.w = tmp0.w * 1.01 + -0.01;
					tmp0.yz = tmp0.yz * _BloomPrePassTexture_TexelSize.xy;
                	tmp0.yz = tmp0.yz * _DisplacementStrength.xx;
                	tmp1.xy = inp.texcoord2.xy / inp.texcoord2.ww;
                	tmp0.xy = tmp0.yz * tmp0.xx + tmp1.xy;
                	tmp1 = tex2D(_BloomPrePassTexture, tmp1.xy);
                	tmp2 = tex2D(_BloomPrePassTexture, tmp0.xy);
                	tmp2 = tmp2 * _TintColor + _AddColor;
                	tmp2 = tmp2 - tmp1;
                	o.sv_target = tmp0.wwww * tmp2 + tmp1;
                	return o;
				}
				#endif
				{
                	fout o;
                	float4 tmp0;
                	float4 tmp1;
                	float4 tmp2;
                	tmp0.xyz = inp.texcoord3.xyz - _WorldSpaceCameraPos;
                	tmp0.x = dot(tmp0.xyz, tmp0.xyz);
                	tmp0.x = sqrt(tmp0.x);
                	tmp0.x = tmp0.x + 1.0;
                	tmp0.x = 1.0 / tmp0.x;
                	tmp1 = tex2D(_MainTex, inp.texcoord.xy);
                	tmp0.yz = tmp1.xy - float2(0.5, 0.5);
                	tmp0.w = tmp1.w * inp.texcoord1.w;
                	tmp0.w = tmp0.w * 1.01 + -0.01;
                	tmp0.yz = tmp0.yz * _GrabTexture1_TexelSize.xy;
                	tmp0.yz = tmp0.yz * _DisplacementStrength.xx;
                	tmp1.xy = inp.texcoord2.xy / inp.texcoord2.ww;
                	tmp0.xy = tmp0.yz * tmp0.xx + tmp1.xy;
                	tmp1 = tex2D(_GrabTexture1, tmp1.xy);
                	tmp2 = tex2D(_GrabTexture1, tmp0.xy);
					tmp2 = tmp2 * _TintColor + _AddColor;
                	tmp2 = tmp2 - tmp1;
                	o.sv_target = tmp0.wwww * tmp2 + tmp1;
                	return o;
				}
			}
			ENDCG
		}
	}
}