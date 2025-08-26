Shader "Custom/Note" {
	Properties {
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
		[Space] _FogStartOffset ("Fog Start Offset", Float) = 0
		_FogScale ("Fog Scale", Float) = 1
		[Toggle(_COLOR_INSTANCING)] _ColorInstancing ("Color Instancing", Float) = 0
		_Color ("Color", Vector) = (1,1,1,1)
		_FinalColorMul ("Color Multiplier", Float) = 1
		[Space] [Toggle(_ENABLE_CUTOUT)] _EnableCutout ("Enable Cutout", Float) = 0
		_CutoutTexScale ("Cutout Texture Scale", Float) = 1
		[Space] [Toggle(_ENABLE_PLANE_CUT)] _EnablePlaneCut ("Enable Plane Cut", Float) = 0
		_CutPlaneEdgeGlowWidth ("Plane Edge Glow Width", Float) = 0.01
		[PerRendererData] _CutPlane ("Cut Plane", Vector) = (1,0,0,0)
		[Space] [Enum(UnityEngine.Rendering.CullMode)] _CullMode ("Cull Mode", Float) = 0
		[Space] [Toggle(_ENABLE_RIM_DIM)] _EnableRimDim ("Enable Rim Dim", Float) = 0
		_RimScale ("Rim Scale", Float) = 1
		_RimOffset ("Rim Offset", Float) = 1
	}
	SubShader {
		Tags { "QUEUE" = "Geometry" "RenderType" = "Opaque" }
		Pass {
			Name "FORWARD"
			Tags { "LIGHTMODE" = "FORWARDBASE" "QUEUE" = "Geometry" "RenderType" = "Opaque" }
			Cull Off
			GpuProgramID 26302
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float3 texcoord : TEXCOORD0;
				float3 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
				float4 texcoord5 : TEXCOORD5;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float4 _LightColor0;
			float4 _CustomFogColor;
			float _CustomFogColorMultiplier;
			float _CustomFogAttenuation;
			float _CustomFogOffset;
			float _Glossiness;
			float _Metallic;
			float _FogStartOffset;
			float _FogScale;
			float _FinalColorMul;
			float4 _Color;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			
			// Keywords: DIRECTIONAL
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
                o.texcoord.xyz = tmp1.www * tmp1.xyz;
                tmp0.y = tmp0.y * _ProjectionParams.x;
                tmp1.xzw = tmp0.xwy * float3(0.5, 0.5, 0.5);
                o.texcoord2.zw = tmp0.zw;
                o.texcoord2.xy = tmp1.zz + tmp1.xw;
                o.texcoord3 = v.vertex;
                o.texcoord5 = float4(0.0, 0.0, 0.0, 0.0);
                return o;
			}
			// Keywords: DIRECTIONAL
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                float4 tmp4;
                float4 tmp5;
                float4 tmp6;
                float4 tmp7;
                float4 tmp8;
                tmp0.xyz = _WorldSpaceCameraPos - inp.texcoord1.xyz;
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp0.xyz = tmp0.xyz * tmp0.www + _WorldSpaceLightPos0.xyz;
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp1.xyz = tmp0.www * tmp0.xyz;
                tmp1.w = 1.0 - _Glossiness;
                tmp2.x = dot(-tmp1.xyz, inp.texcoord.xyz);
                tmp2.x = tmp2.x + tmp2.x;
                tmp2.xyz = inp.texcoord.xyz * -tmp2.xxx + -tmp1.xyz;
                tmp2.w = unity_SpecCube0_ProbePosition.w > 0.0;
                if (tmp2.w) {
                    tmp2.w = dot(tmp2.xyz, tmp2.xyz);
                    tmp2.w = rsqrt(tmp2.w);
                    tmp3.xyz = tmp2.www * tmp2.xyz;
                    tmp4.xyz = unity_SpecCube0_BoxMax.xyz - inp.texcoord1.xyz;
                    tmp4.xyz = tmp4.xyz / tmp3.xyz;
                    tmp5.xyz = unity_SpecCube0_BoxMin.xyz - inp.texcoord1.xyz;
                    tmp5.xyz = tmp5.xyz / tmp3.xyz;
                    tmp6.xyz = tmp3.xyz > float3(0.0, 0.0, 0.0);
                    tmp4.xyz = tmp6.xyz ? tmp4.xyz : tmp5.xyz;
                    tmp2.w = min(tmp4.y, tmp4.x);
                    tmp2.w = min(tmp4.z, tmp2.w);
                    tmp4.xyz = inp.texcoord1.xyz - unity_SpecCube0_ProbePosition.xyz;
                    tmp3.xyz = tmp3.xyz * tmp2.www + tmp4.xyz;
                } else {
                    tmp3.xyz = tmp2.xyz;
                }
                tmp2.w = -tmp1.w * 0.7 + 1.7;
                tmp2.w = tmp1.w * tmp2.w;
                tmp2.w = tmp2.w * 6.0;
                tmp3 = UNITY_SAMPLE_TEXCUBE_SAMPLER(unity_SpecCube0, unity_SpecCube0, float4(tmp3.xyz, tmp2.w));
                tmp3.w = tmp3.w - 1.0;
                tmp3.w = unity_SpecCube0_HDR.w * tmp3.w + 1.0;
                tmp3.w = log(tmp3.w);
                tmp3.w = tmp3.w * unity_SpecCube0_HDR.y;
                tmp3.w = exp(tmp3.w);
                tmp3.w = tmp3.w * unity_SpecCube0_HDR.x;
                tmp4.xyz = tmp3.xyz * tmp3.www;
                tmp4.w = unity_SpecCube0_BoxMin.w < 0.99999;
                if (tmp4.w) {
                    tmp4.w = unity_SpecCube1_ProbePosition.w > 0.0;
                    if (tmp4.w) {
                        tmp4.w = dot(tmp2.xyz, tmp2.xyz);
                        tmp4.w = rsqrt(tmp4.w);
                        tmp5.xyz = tmp2.xyz * tmp4.www;
                        tmp6.xyz = unity_SpecCube1_BoxMax.xyz - inp.texcoord1.xyz;
                        tmp6.xyz = tmp6.xyz / tmp5.xyz;
                        tmp7.xyz = unity_SpecCube1_BoxMin.xyz - inp.texcoord1.xyz;
                        tmp7.xyz = tmp7.xyz / tmp5.xyz;
                        tmp8.xyz = tmp5.xyz > float3(0.0, 0.0, 0.0);
                        tmp6.xyz = tmp8.xyz ? tmp6.xyz : tmp7.xyz;
                        tmp4.w = min(tmp6.y, tmp6.x);
                        tmp4.w = min(tmp6.z, tmp4.w);
                        tmp6.xyz = inp.texcoord1.xyz - unity_SpecCube1_ProbePosition.xyz;
                        tmp2.xyz = tmp5.xyz * tmp4.www + tmp6.xyz;
                    }
                    tmp2 = UNITY_SAMPLE_TEXCUBE_SAMPLER(unity_SpecCube0, unity_SpecCube0, float4(tmp2.xyz, tmp2.w));
                    tmp2.w = tmp2.w - 1.0;
                    tmp2.w = unity_SpecCube1_HDR.w * tmp2.w + 1.0;
                    tmp2.w = log(tmp2.w);
                    tmp2.w = tmp2.w * unity_SpecCube1_HDR.y;
                    tmp2.w = exp(tmp2.w);
                    tmp2.w = tmp2.w * unity_SpecCube1_HDR.x;
                    tmp2.xyz = tmp2.xyz * tmp2.www;
                    tmp3.xyz = tmp3.www * tmp3.xyz + -tmp2.xyz;
                    tmp4.xyz = unity_SpecCube0_BoxMin.www * tmp3.xyz + tmp2.xyz;
                }
                tmp2.x = dot(inp.texcoord.xyz, inp.texcoord.xyz);
                tmp2.x = rsqrt(tmp2.x);
                tmp2.xyz = tmp2.xxx * inp.texcoord.xyz;
                tmp2.w = _Metallic * -0.04 + 0.04;
                tmp3.x = -_Metallic * 0.96 + 0.96;
                tmp0.xyz = tmp0.xyz * tmp0.www + _WorldSpaceLightPos0.xyz;
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = max(tmp0.w, 0.001);
                tmp0.w = rsqrt(tmp0.w);
                tmp0.xyz = tmp0.www * tmp0.xyz;
                tmp0.w = dot(tmp2.xyz, tmp1.xyz);
                tmp1.x = saturate(dot(tmp2.xyz, _WorldSpaceLightPos0.xyz));
                tmp1.y = saturate(dot(tmp2.xyz, tmp0.xyz));
                tmp0.x = saturate(dot(_WorldSpaceLightPos0.xyz, tmp0.xyz));
                tmp0.y = tmp1.w * tmp1.w;
                tmp0.y = max(tmp0.y, 0.002);
                tmp0.z = 1.0 - tmp0.y;
                tmp1.z = abs(tmp0.w) * tmp0.z + tmp0.y;
                tmp0.z = tmp1.x * tmp0.z + tmp0.y;
                tmp0.z = tmp0.z * abs(tmp0.w);
                tmp0.z = tmp1.x * tmp1.z + tmp0.z;
                tmp0.z = tmp0.z + 0.00001;
                tmp0.z = 0.5 / tmp0.z;
                tmp1.z = tmp0.y * tmp0.y;
                tmp1.w = tmp1.y * tmp1.z + -tmp1.y;
                tmp1.y = tmp1.w * tmp1.y + 1.0;
                tmp1.z = tmp1.z * 0.3183099;
                tmp1.y = tmp1.y * tmp1.y + 0.0000001;
                tmp1.y = tmp1.z / tmp1.y;
                tmp0.z = tmp0.z * tmp1.y;
                tmp0.z = tmp0.z * 3.141593;
                tmp0.z = tmp1.x * tmp0.z;
                tmp0.z = max(tmp0.z, 0.0);
                tmp0.y = tmp0.y * tmp0.y + 1.0;
                tmp0.y = 1.0 / tmp0.y;
                tmp1.x = dot(tmp2.xyz, tmp2.xyz);
                tmp1.x = tmp1.x != 0.0;
                tmp1.x = tmp1.x ? 1.0 : 0.0;
                tmp0.z = tmp0.z * tmp1.x;
                tmp1.x = 1.0 - tmp3.x;
                tmp1.x = saturate(tmp1.x + _Glossiness);
                tmp1.yzw = tmp0.zzz * _LightColor0.xyz;
                tmp0.x = 1.0 - tmp0.x;
                tmp0.z = tmp0.x * tmp0.x;
                tmp0.z = tmp0.z * tmp0.z;
                tmp0.x = tmp0.x * tmp0.z;
                tmp0.z = 1.0 - tmp2.w;
                tmp0.x = tmp0.z * tmp0.x + tmp2.w;
                tmp2.xyz = tmp4.xyz * tmp0.yyy;
                tmp0.y = 1.0 - abs(tmp0.w);
                tmp0.z = tmp0.y * tmp0.y;
                tmp0.z = tmp0.z * tmp0.z;
                tmp0.y = tmp0.y * tmp0.z;
                tmp0.z = tmp1.x - tmp2.w;
                tmp0.y = tmp0.y * tmp0.z + tmp2.w;
                tmp0.yzw = tmp0.yyy * tmp2.xyz;
                tmp0.xyz = tmp1.yzw * tmp0.xxx + tmp0.yzw;
                tmp1.xyz = _FinalColorMul.xxx * _Color.xyz;
                tmp0.xyz = tmp0.xyz * tmp1.xyz;
                tmp1.xyz = inp.texcoord1.xyz - _WorldSpaceCameraPos;
                tmp1.x = dot(tmp1.xyz, tmp1.xyz);
                tmp1.x = sqrt(tmp1.x);
                tmp1.x = tmp1.x - _FogStartOffset;
                tmp1.x = max(tmp1.x, 0.0);
                tmp1.x = tmp1.x * _FogScale + -_CustomFogOffset;
                tmp1.x = max(tmp1.x, 0.0);
                tmp1.x = min(tmp1.x, 9999.0);
                tmp1.x = -tmp1.x * _CustomFogAttenuation;
                tmp1.x = tmp1.x * 1.442695;
                tmp1.x = exp(tmp1.x);
                tmp1.x = 1.0 - tmp1.x;
                tmp1.x = max(tmp1.x, 0.0);
                tmp0.w = 0.0;
                tmp2 = _CustomFogColor * _CustomFogColorMultiplier.xxxx + -tmp0;
                o.sv_target = tmp1.xxxx * tmp2 + tmp0;
                return o;
			}
			ENDCG
		}
		Pass {
			Name "SHADOWCASTER"
			Tags { "LIGHTMODE" = "SHADOWCASTER" "QUEUE" = "Geometry" "RenderType" = "Opaque" }
			Cull Off
			GpuProgramID 83869
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float3 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                tmp0.x = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp0.y = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp0.z = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp0.xyz = tmp0.www * tmp0.xyz;
                tmp1 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp1 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp1;
                tmp1 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp1;
                tmp2 = unity_ObjectToWorld._m03_m13_m23_m33 * v.vertex.wwww + tmp1;
                tmp3.xyz = -tmp2.xyz * _WorldSpaceLightPos0.www + _WorldSpaceLightPos0.xyz;
                tmp0.w = dot(tmp3.xyz, tmp3.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp3.xyz = tmp0.www * tmp3.xyz;
                tmp0.w = dot(tmp0.xyz, tmp3.xyz);
                tmp0.w = -tmp0.w * tmp0.w + 1.0;
                tmp0.w = sqrt(tmp0.w);
                tmp0.w = tmp0.w * unity_LightShadowBias.z;
                tmp0.xyz = -tmp0.xyz * tmp0.www + tmp2.xyz;
                tmp0.w = unity_LightShadowBias.z != 0.0;
                tmp0.xyz = tmp0.www ? tmp0.xyz : tmp2.xyz;
                tmp3 = tmp0.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp3 = unity_MatrixVP._m00_m10_m20_m30 * tmp0.xxxx + tmp3;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp0.zzzz + tmp3;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp2.wwww + tmp0;
                tmp2.x = unity_LightShadowBias.x / tmp0.w;
                tmp2.x = min(tmp2.x, 0.0);
                tmp2.x = max(tmp2.x, -1.0);
                tmp0.z = tmp0.z + tmp2.x;
                tmp2.x = min(tmp0.w, tmp0.z);
                o.position.xyw = tmp0.xyw;
                tmp0.x = tmp2.x - tmp0.z;
                o.position.z = unity_LightShadowBias.y * tmp0.x + tmp0.z;
                o.texcoord1.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp1.xyz;
                tmp0 = tmp1 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp1 = tmp0.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp1 = unity_MatrixVP._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                tmp1.xz = tmp0.xw * float2(0.5, 0.5);
                tmp0.x = tmp0.y * _ProjectionParams.x;
                o.texcoord2.zw = tmp0.zw;
                tmp1.w = tmp0.x * 0.5;
                o.texcoord2.xy = tmp1.zz + tmp1.xw;
                o.texcoord3 = v.vertex;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                o.sv_target = float4(0.0, 0.0, 0.0, 0.0);
                return o;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}