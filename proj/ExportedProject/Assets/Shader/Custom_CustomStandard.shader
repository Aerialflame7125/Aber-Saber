Shader "Custom/CustomStandard" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
		[Space] _FogStartOffset ("Fog Start Offset", Float) = 0
		_FogScale ("Fog Scale", Float) = 1
	}
	SubShader {
		Tags { "QUEUE" = "Geometry" "RenderType" = "Opaque" }
		Pass {
			Name "FORWARD"
			Tags { "LIGHTMODE" = "FORWARDBASE" "QUEUE" = "Geometry" "RenderType" = "Opaque" }
			GpuProgramID 15089
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
			float2 _GlobalBlueNoiseParams;
			float _GlobalRandomValue;
			float _Glossiness;
			float _Metallic;
			float4 _Color;
			float _FogStartOffset;
			float _FogScale;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _GlobalBlueNoiseTex;
			
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
                float4 tmp9;
                tmp0.xyz = _WorldSpaceCameraPos - inp.texcoord1.xyz;
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp1.xyz = tmp0.www * tmp0.xyz;
                tmp1.w = unity_ProbeVolumeParams.x == 1.0;
                if (tmp1.w) {
                    tmp1.w = unity_ProbeVolumeParams.y == 1.0;
                    tmp2.xyz = inp.texcoord1.yyy * unity_ProbeVolumeWorldToObject._m01_m11_m21;
                    tmp2.xyz = unity_ProbeVolumeWorldToObject._m00_m10_m20 * inp.texcoord1.xxx + tmp2.xyz;
                    tmp2.xyz = unity_ProbeVolumeWorldToObject._m02_m12_m22 * inp.texcoord1.zzz + tmp2.xyz;
                    tmp2.xyz = tmp2.xyz + unity_ProbeVolumeWorldToObject._m03_m13_m23;
                    tmp2.xyz = tmp1.www ? tmp2.xyz : inp.texcoord1.xyz;
                    tmp2.xyz = tmp2.xyz - unity_ProbeVolumeMin;
                    tmp2.yzw = tmp2.xyz * unity_ProbeVolumeSizeInv;
                    tmp1.w = tmp2.y * 0.25 + 0.75;
                    tmp2.y = unity_ProbeVolumeParams.z * 0.5 + 0.75;
                    tmp2.x = max(tmp1.w, tmp2.y);
                    tmp2 = UNITY_SAMPLE_TEX3D_SAMPLER(unity_ProbeVolumeSH, unity_ProbeVolumeSH, tmp2.xzw);
                } else {
                    tmp2 = float4(1.0, 1.0, 1.0, 1.0);
                }
                tmp1.w = saturate(dot(tmp2, unity_OcclusionMaskSelector));
                tmp2.x = 1.0 - _Glossiness;
                tmp2.y = dot(-tmp1.xyz, inp.texcoord.xyz);
                tmp2.y = tmp2.y + tmp2.y;
                tmp2.yzw = inp.texcoord.xyz * -tmp2.yyy + -tmp1.xyz;
                tmp3.xyz = tmp1.www * _LightColor0.xyz;
                tmp1.w = unity_SpecCube0_ProbePosition.w > 0.0;
                if (tmp1.w) {
                    tmp1.w = dot(tmp2.xyz, tmp2.xyz);
                    tmp1.w = rsqrt(tmp1.w);
                    tmp4.xyz = tmp1.www * tmp2.yzw;
                    tmp5.xyz = unity_SpecCube0_BoxMax.xyz - inp.texcoord1.xyz;
                    tmp5.xyz = tmp5.xyz / tmp4.xyz;
                    tmp6.xyz = unity_SpecCube0_BoxMin.xyz - inp.texcoord1.xyz;
                    tmp6.xyz = tmp6.xyz / tmp4.xyz;
                    tmp7.xyz = tmp4.xyz > float3(0.0, 0.0, 0.0);
                    tmp5.xyz = tmp7.xyz ? tmp5.xyz : tmp6.xyz;
                    tmp1.w = min(tmp5.y, tmp5.x);
                    tmp1.w = min(tmp5.z, tmp1.w);
                    tmp5.xyz = inp.texcoord1.xyz - unity_SpecCube0_ProbePosition.xyz;
                    tmp4.xyz = tmp4.xyz * tmp1.www + tmp5.xyz;
                } else {
                    tmp4.xyz = tmp2.yzw;
                }
                tmp1.w = -tmp2.x * 0.7 + 1.7;
                tmp1.w = tmp1.w * tmp2.x;
                tmp1.w = tmp1.w * 6.0;
                tmp4 = UNITY_SAMPLE_TEXCUBE_SAMPLER(unity_SpecCube0, unity_SpecCube0, float4(tmp4.xyz, tmp1.w));
                tmp3.w = tmp4.w - 1.0;
                tmp3.w = unity_SpecCube0_HDR.w * tmp3.w + 1.0;
                tmp3.w = log(tmp3.w);
                tmp3.w = tmp3.w * unity_SpecCube0_HDR.y;
                tmp3.w = exp(tmp3.w);
                tmp3.w = tmp3.w * unity_SpecCube0_HDR.x;
                tmp5.xyz = tmp4.xyz * tmp3.www;
                tmp4.w = unity_SpecCube0_BoxMin.w < 0.99999;
                if (tmp4.w) {
                    tmp4.w = unity_SpecCube1_ProbePosition.w > 0.0;
                    if (tmp4.w) {
                        tmp4.w = dot(tmp2.xyz, tmp2.xyz);
                        tmp4.w = rsqrt(tmp4.w);
                        tmp6.xyz = tmp2.yzw * tmp4.www;
                        tmp7.xyz = unity_SpecCube1_BoxMax.xyz - inp.texcoord1.xyz;
                        tmp7.xyz = tmp7.xyz / tmp6.xyz;
                        tmp8.xyz = unity_SpecCube1_BoxMin.xyz - inp.texcoord1.xyz;
                        tmp8.xyz = tmp8.xyz / tmp6.xyz;
                        tmp9.xyz = tmp6.xyz > float3(0.0, 0.0, 0.0);
                        tmp7.xyz = tmp9.xyz ? tmp7.xyz : tmp8.xyz;
                        tmp4.w = min(tmp7.y, tmp7.x);
                        tmp4.w = min(tmp7.z, tmp4.w);
                        tmp7.xyz = inp.texcoord1.xyz - unity_SpecCube1_ProbePosition.xyz;
                        tmp2.yzw = tmp6.xyz * tmp4.www + tmp7.xyz;
                    }
                    tmp6 = UNITY_SAMPLE_TEXCUBE_SAMPLER(unity_SpecCube0, unity_SpecCube0, float4(tmp2.yzw, tmp1.w));
                    tmp1.w = tmp6.w - 1.0;
                    tmp1.w = unity_SpecCube1_HDR.w * tmp1.w + 1.0;
                    tmp1.w = log(tmp1.w);
                    tmp1.w = tmp1.w * unity_SpecCube1_HDR.y;
                    tmp1.w = exp(tmp1.w);
                    tmp1.w = tmp1.w * unity_SpecCube1_HDR.x;
                    tmp2.yzw = tmp6.xyz * tmp1.www;
                    tmp4.xyz = tmp3.www * tmp4.xyz + -tmp2.yzw;
                    tmp5.xyz = unity_SpecCube0_BoxMin.www * tmp4.xyz + tmp2.yzw;
                }
                tmp1.w = dot(inp.texcoord.xyz, inp.texcoord.xyz);
                tmp1.w = rsqrt(tmp1.w);
                tmp2.yzw = tmp1.www * inp.texcoord.xyz;
                tmp4.xyz = _Color.xyz - float3(0.04, 0.04, 0.04);
                tmp4.xyz = _Metallic.xxx * tmp4.xyz + float3(0.04, 0.04, 0.04);
                tmp1.w = -_Metallic * 0.96 + 0.96;
                tmp6.xyz = tmp1.www * _Color.xyz;
                tmp0.xyz = tmp0.xyz * tmp0.www + _WorldSpaceLightPos0.xyz;
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = max(tmp0.w, 0.001);
                tmp0.w = rsqrt(tmp0.w);
                tmp0.xyz = tmp0.www * tmp0.xyz;
                tmp0.w = dot(tmp2.xyz, tmp1.xyz);
                tmp1.x = saturate(dot(tmp2.xyz, _WorldSpaceLightPos0.xyz));
                tmp1.y = saturate(dot(tmp2.xyz, tmp0.xyz));
                tmp0.x = saturate(dot(_WorldSpaceLightPos0.xyz, tmp0.xyz));
                tmp0.y = tmp0.x * tmp0.x;
                tmp0.y = dot(tmp0.xy, tmp2.xy);
                tmp0.y = tmp0.y - 0.5;
                tmp0.z = 1.0 - tmp1.x;
                tmp1.z = tmp0.z * tmp0.z;
                tmp1.z = tmp1.z * tmp1.z;
                tmp0.z = tmp0.z * tmp1.z;
                tmp0.z = tmp0.y * tmp0.z + 1.0;
                tmp1.z = 1.0 - abs(tmp0.w);
                tmp2.y = tmp1.z * tmp1.z;
                tmp2.y = tmp2.y * tmp2.y;
                tmp1.z = tmp1.z * tmp2.y;
                tmp0.y = tmp0.y * tmp1.z + 1.0;
                tmp0.y = tmp0.y * tmp0.z;
                tmp0.z = tmp2.x * tmp2.x;
                tmp0.z = max(tmp0.z, 0.002);
                tmp2.x = 1.0 - tmp0.z;
                tmp2.y = abs(tmp0.w) * tmp2.x + tmp0.z;
                tmp2.x = tmp1.x * tmp2.x + tmp0.z;
                tmp0.w = abs(tmp0.w) * tmp2.x;
                tmp0.w = tmp1.x * tmp2.y + tmp0.w;
                tmp0.w = tmp0.w + 0.00001;
                tmp0.w = 0.5 / tmp0.w;
                tmp2.x = tmp0.z * tmp0.z;
                tmp2.y = tmp1.y * tmp2.x + -tmp1.y;
                tmp1.y = tmp2.y * tmp1.y + 1.0;
                tmp2.x = tmp2.x * 0.3183099;
                tmp1.y = tmp1.y * tmp1.y + 0.0000001;
                tmp1.y = tmp2.x / tmp1.y;
                tmp0.w = tmp0.w * tmp1.y;
                tmp0.w = tmp0.w * 3.141593;
                tmp0.yw = tmp1.xx * tmp0.yw;
                tmp0.w = max(tmp0.w, 0.0);
                tmp0.z = tmp0.z * tmp0.z + 1.0;
                tmp0.z = 1.0 / tmp0.z;
                tmp1.x = dot(tmp4.xyz, tmp4.xyz);
                tmp1.x = tmp1.x != 0.0;
                tmp1.x = tmp1.x ? 1.0 : 0.0;
                tmp0.w = tmp0.w * tmp1.x;
                tmp1.x = 1.0 - tmp1.w;
                tmp1.x = saturate(tmp1.x + _Glossiness);
                tmp2.xyz = tmp0.yyy * tmp3.xyz;
                tmp3.xyz = tmp3.xyz * tmp0.www;
                tmp0.x = 1.0 - tmp0.x;
                tmp0.y = tmp0.x * tmp0.x;
                tmp0.y = tmp0.y * tmp0.y;
                tmp0.x = tmp0.x * tmp0.y;
                tmp7.xyz = float3(1.0, 1.0, 1.0) - tmp4.xyz;
                tmp0.xyw = tmp7.xyz * tmp0.xxx + tmp4.xyz;
                tmp0.xyw = tmp0.xyw * tmp3.xyz;
                tmp0.xyw = tmp6.xyz * tmp2.xyz + tmp0.xyw;
                tmp2.xyz = tmp5.xyz * tmp0.zzz;
                tmp1.xyw = tmp1.xxx - tmp4.xyz;
                tmp1.xyz = tmp1.zzz * tmp1.xyw + tmp4.xyz;
                tmp0.xyz = tmp2.xyz * tmp1.xyz + tmp0.xyw;
                tmp1.xy = inp.texcoord2.xy / inp.texcoord2.ww;
                tmp2.xyz = inp.texcoord1.xyz - _WorldSpaceCameraPos;
                tmp1.z = dot(tmp2.xyz, tmp2.xyz);
                tmp1.z = sqrt(tmp1.z);
                tmp1.w = dot(inp.texcoord1.xyz, inp.texcoord1.xyz);
                tmp1.w = rsqrt(tmp1.w);
                tmp2.xyz = tmp1.www * inp.texcoord1.xyz;
                tmp1.w = dot(tmp2.xyz, inp.texcoord.xyz);
                tmp1.w = tmp1.w * 0.1 + 1.0;
                tmp1.z = tmp1.z * tmp1.w + -_FogStartOffset;
                tmp1.z = max(tmp1.z, 0.0);
                tmp1.z = tmp1.z * _FogScale + -_CustomFogOffset;
                tmp1.z = max(tmp1.z, 0.0);
                tmp1.z = min(tmp1.z, 9999.0);
                tmp1.z = -tmp1.z * _CustomFogAttenuation;
                tmp1.z = tmp1.z * 1.442695;
                tmp1.z = exp(tmp1.z);
                tmp1.z = 1.0 - tmp1.z;
                tmp1.z = max(tmp1.z, 0.0);
                tmp0.w = _Color.w;
                tmp2 = _CustomFogColor * _CustomFogColorMultiplier.xxxx + -tmp0;
                tmp0 = tmp1.zzzz * tmp2 + tmp0;
                tmp1.xy = tmp1.xy * _GlobalBlueNoiseParams + _GlobalRandomValue.xx;
                tmp1 = tex2D(_GlobalBlueNoiseTex, tmp1.xy);
                tmp1.x = tmp1.w - 0.5;
                o.sv_target.xyz = tmp1.xxx * float3(0.0039063, 0.0039063, 0.0039063) + tmp0.xyz;
                o.sv_target.w = tmp0.w;
                return o;
			}
			ENDCG
		}
		Pass {
			Name "DEFERRED"
			Tags { "LIGHTMODE" = "DEFERRED" "QUEUE" = "Geometry" "RenderType" = "Opaque" }
			GpuProgramID 118517
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
				float4 texcoord4 : TEXCOORD4;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
				float4 sv_target1 : SV_Target1;
				float4 sv_target2 : SV_Target2;
				float4 sv_target3 : SV_Target3;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float _Glossiness;
			float _Metallic;
			float4 _Color;
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
                o.texcoord4 = float4(0.0, 0.0, 0.0, 0.0);
                return o;
			}
			// Keywords: 
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
                tmp0.xyz = _WorldSpaceCameraPos - inp.texcoord1.xyz;
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp0.xyz = tmp0.www * tmp0.xyz;
                tmp0.w = 1.0 - _Glossiness;
                tmp1.x = dot(-tmp0.xyz, inp.texcoord.xyz);
                tmp1.x = tmp1.x + tmp1.x;
                tmp1.xyz = inp.texcoord.xyz * -tmp1.xxx + -tmp0.xyz;
                tmp1.w = unity_SpecCube0_ProbePosition.w > 0.0;
                if (tmp1.w) {
                    tmp1.w = dot(tmp1.xyz, tmp1.xyz);
                    tmp1.w = rsqrt(tmp1.w);
                    tmp2.xyz = tmp1.www * tmp1.xyz;
                    tmp3.xyz = unity_SpecCube0_BoxMax.xyz - inp.texcoord1.xyz;
                    tmp3.xyz = tmp3.xyz / tmp2.xyz;
                    tmp4.xyz = unity_SpecCube0_BoxMin.xyz - inp.texcoord1.xyz;
                    tmp4.xyz = tmp4.xyz / tmp2.xyz;
                    tmp5.xyz = tmp2.xyz > float3(0.0, 0.0, 0.0);
                    tmp3.xyz = tmp5.xyz ? tmp3.xyz : tmp4.xyz;
                    tmp1.w = min(tmp3.y, tmp3.x);
                    tmp1.w = min(tmp3.z, tmp1.w);
                    tmp3.xyz = inp.texcoord1.xyz - unity_SpecCube0_ProbePosition.xyz;
                    tmp2.xyz = tmp2.xyz * tmp1.www + tmp3.xyz;
                } else {
                    tmp2.xyz = tmp1.xyz;
                }
                tmp1.w = -tmp0.w * 0.7 + 1.7;
                tmp1.w = tmp0.w * tmp1.w;
                tmp1.w = tmp1.w * 6.0;
                tmp2 = UNITY_SAMPLE_TEXCUBE_SAMPLER(unity_SpecCube0, unity_SpecCube0, float4(tmp2.xyz, tmp1.w));
                tmp2.w = tmp2.w - 1.0;
                tmp2.w = unity_SpecCube0_HDR.w * tmp2.w + 1.0;
                tmp2.w = log(tmp2.w);
                tmp2.w = tmp2.w * unity_SpecCube0_HDR.y;
                tmp2.w = exp(tmp2.w);
                tmp2.w = tmp2.w * unity_SpecCube0_HDR.x;
                tmp3.xyz = tmp2.xyz * tmp2.www;
                tmp3.w = unity_SpecCube0_BoxMin.w < 0.99999;
                if (tmp3.w) {
                    tmp3.w = unity_SpecCube1_ProbePosition.w > 0.0;
                    if (tmp3.w) {
                        tmp3.w = dot(tmp1.xyz, tmp1.xyz);
                        tmp3.w = rsqrt(tmp3.w);
                        tmp4.xyz = tmp1.xyz * tmp3.www;
                        tmp5.xyz = unity_SpecCube1_BoxMax.xyz - inp.texcoord1.xyz;
                        tmp5.xyz = tmp5.xyz / tmp4.xyz;
                        tmp6.xyz = unity_SpecCube1_BoxMin.xyz - inp.texcoord1.xyz;
                        tmp6.xyz = tmp6.xyz / tmp4.xyz;
                        tmp7.xyz = tmp4.xyz > float3(0.0, 0.0, 0.0);
                        tmp5.xyz = tmp7.xyz ? tmp5.xyz : tmp6.xyz;
                        tmp3.w = min(tmp5.y, tmp5.x);
                        tmp3.w = min(tmp5.z, tmp3.w);
                        tmp5.xyz = inp.texcoord1.xyz - unity_SpecCube1_ProbePosition.xyz;
                        tmp1.xyz = tmp4.xyz * tmp3.www + tmp5.xyz;
                    }
                    tmp1 = UNITY_SAMPLE_TEXCUBE_SAMPLER(unity_SpecCube0, unity_SpecCube0, float4(tmp1.xyz, tmp1.w));
                    tmp1.w = tmp1.w - 1.0;
                    tmp1.w = unity_SpecCube1_HDR.w * tmp1.w + 1.0;
                    tmp1.w = log(tmp1.w);
                    tmp1.w = tmp1.w * unity_SpecCube1_HDR.y;
                    tmp1.w = exp(tmp1.w);
                    tmp1.w = tmp1.w * unity_SpecCube1_HDR.x;
                    tmp1.xyz = tmp1.xyz * tmp1.www;
                    tmp2.xyz = tmp2.www * tmp2.xyz + -tmp1.xyz;
                    tmp3.xyz = unity_SpecCube0_BoxMin.www * tmp2.xyz + tmp1.xyz;
                }
                tmp1.xyz = _Color.xyz - float3(0.04, 0.04, 0.04);
                tmp1.xyz = _Metallic.xxx * tmp1.xyz + float3(0.04, 0.04, 0.04);
                tmp1.w = -_Metallic * 0.96 + 0.96;
                o.sv_target.xyz = tmp1.www * _Color.xyz;
                tmp0.x = dot(inp.texcoord.xyz, tmp0.xyz);
                tmp0.y = tmp0.w * tmp0.w;
                tmp0.y = max(tmp0.y, 0.002);
                tmp0.y = tmp0.y * tmp0.y + 1.0;
                tmp0.y = 1.0 / tmp0.y;
                tmp0.z = _Glossiness - tmp1.w;
                tmp0.z = saturate(tmp0.z + 1.0);
                tmp2.xyz = tmp3.xyz * tmp0.yyy;
                tmp0.x = 1.0 - abs(tmp0.x);
                tmp0.y = tmp0.x * tmp0.x;
                tmp0.y = tmp0.y * tmp0.y;
                tmp0.x = tmp0.x * tmp0.y;
                tmp0.yzw = tmp0.zzz - tmp1.xyz;
                tmp0.xyz = tmp0.xxx * tmp0.yzw + tmp1.xyz;
                tmp0.xyz = tmp0.xyz * tmp2.xyz;
                o.sv_target3.xyz = exp(-tmp0.xyz);
                o.sv_target.w = 1.0;
                o.sv_target1.w = _Glossiness;
                o.sv_target1.xyz = tmp1.xyz;
                o.sv_target2.xyz = inp.texcoord.xyz * float3(0.5, 0.5, 0.5) + float3(0.5, 0.5, 0.5);
                o.sv_target2.w = 1.0;
                o.sv_target3.w = 1.0;
                return o;
			}
			ENDCG
		}
		Pass {
			Name "META"
			Tags { "LIGHTMODE" = "META" "QUEUE" = "Geometry" "RenderType" = "Opaque" }
			Cull Off
			GpuProgramID 185564
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
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float4 _Color;
			float unity_OneOverOutputBoost;
			float unity_MaxOutputValue;
			// Custom ConstantBuffers for Vertex Shader
			CBUFFER_START(UnityMetaPass)
				bool4 unity_MetaVertexControl;
			CBUFFER_END
			// Custom ConstantBuffers for Fragment Shader
			CBUFFER_START(UnityMetaPass)
				bool4 unity_MetaFragmentControl;
			CBUFFER_END
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                tmp0.x = v.vertex.z > 0.0;
                tmp0.z = tmp0.x ? 0.0001 : 0.0;
                tmp0.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                tmp0.xyz = unity_MetaVertexControl.xxx ? tmp0.xyz : v.vertex.xyz;
                tmp0.w = tmp0.z > 0.0;
                tmp1.z = tmp0.w ? 0.0001 : 0.0;
                tmp1.xy = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                tmp0.xyz = unity_MetaVertexControl.yyy ? tmp1.xyz : tmp0.xyz;
                tmp1 = tmp0.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                o.position = tmp0 + unity_MatrixVP._m03_m13_m23_m33;
                tmp0.x = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp0.y = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp0.z = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = rsqrt(tmp0.w);
                o.texcoord.xyz = tmp0.www * tmp0.xyz;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                o.texcoord1.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp0 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp1 = tmp0.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp1 = unity_MatrixVP._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                tmp1.xz = tmp0.xw * float2(0.5, 0.5);
                tmp0.x = tmp0.y * _ProjectionParams.x;
                o.texcoord2.zw = tmp0.zw;
                tmp1.w = tmp0.x * 0.5;
                o.texcoord2.xy = tmp1.zz + tmp1.xw;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                tmp0.x = saturate(unity_OneOverOutputBoost);
                tmp0.yzw = log(_Color.xyz);
                tmp0.xyz = tmp0.yzw * tmp0.xxx;
                tmp0.xyz = exp(tmp0.xyz);
                tmp0.xyz = min(tmp0.xyz, unity_MaxOutputValue.xxx);
                tmp0.w = 1.0;
                tmp0 = unity_MetaFragmentControl ? tmp0 : float4(0.0, 0.0, 0.0, 0.0);
                o.sv_target = unity_MetaFragmentControl ? float4(0.0, 0.0, 0.0, 1.0) : tmp0;
                return o;
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}