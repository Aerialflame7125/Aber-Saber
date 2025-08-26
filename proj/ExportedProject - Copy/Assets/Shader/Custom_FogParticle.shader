Shader "Custom/FogParticle" {
	Properties {
		_MainTex ("Main Texture", 2D) = "white" {}
		_SoftFactor ("Soft Factor", Range(0, 5)) = 0
		_BrightnessBoost ("Brightness Boost", Range(0, 10)) = 1.1
	}
	SubShader {
		Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha One, Zero One
			ZWrite Off
			Cull Off
			GpuProgramID 19204
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float4 color : COLOR0;
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
			float4 _CustomFogColor;
			float _CustomFogColorMultiplier;
			float2 _GlobalBlueNoiseParams;
			float _GlobalRandomValue;
			float _SoftFactor;
			float _BrightnessBoost;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _CameraDepthTexture;
			sampler2D _GlobalBlueNoiseTex;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp0.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp0.xyz = tmp0.xyz - _WorldSpaceCameraPos;
                tmp2 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp2 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp2;
                tmp2 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp2;
                tmp2 = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp2;
                o.sv_position = tmp2;
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = sqrt(tmp0.w);
                tmp0.xyz = tmp0.xyz / tmp0.www;
                tmp3.xyz = v.normal.yyy * unity_ObjectToWorld._m01_m11_m21;
                tmp3.xyz = unity_ObjectToWorld._m00_m10_m20 * v.normal.xxx + tmp3.xyz;
                tmp3.xyz = unity_ObjectToWorld._m02_m12_m22 * v.normal.zzz + tmp3.xyz;
                tmp0.w = dot(tmp3.xyz, tmp3.xyz);
                tmp0.w = rsqrt(tmp0.w);
                tmp3.xyz = tmp0.www * tmp3.xyz;
                tmp0.x = dot(tmp3.xyz, tmp0.xyz);
                tmp0.x = abs(tmp0.x) * abs(tmp0.x);
                tmp0.x = tmp0.x * 1.5;
                tmp0.x = min(tmp0.x, 1.0);
                o.color.w = tmp0.x * v.color.w;
                o.color.xyz = v.color.xyz;
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
                tmp0.x = tmp1.y * unity_MatrixV._m21;
                tmp0.x = unity_MatrixV._m20 * tmp1.x + tmp0.x;
                tmp0.x = unity_MatrixV._m22 * tmp1.z + tmp0.x;
                tmp0.x = unity_MatrixV._m23 * tmp1.w + tmp0.x;
                o.texcoord1.z = -tmp0.x;
                tmp0.x = tmp2.y * _ProjectionParams.x;
                tmp0.w = tmp0.x * 0.5;
                tmp0.xz = tmp2.xw * float2(0.5, 0.5);
                tmp2.xy = tmp0.zz + tmp0.xw;
                o.texcoord1.xyw = tmp2.xyw;
                o.texcoord2 = tmp2;
                o.texcoord3 = v.normal;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                tmp0.xy = inp.texcoord2.xy / inp.texcoord2.ww;
                tmp0.xy = tmp0.xy + inp.texcoord3.xy;
                tmp0.xy = tmp0.xy * _GlobalBlueNoiseParams + _GlobalRandomValue.xx;
                tmp0 = tex2D(_GlobalBlueNoiseTex, tmp0.xy);
                tmp0.x = tmp0.w - 0.5;
                tmp0.yz = inp.texcoord1.xy / inp.texcoord1.ww;
                tmp1 = tex2D(_CameraDepthTexture, tmp0.yz);
                tmp0.y = _ZBufferParams.z * tmp1.x + _ZBufferParams.w;
                tmp0.y = 1.0 / tmp0.y;
                tmp0.y = tmp0.y - inp.texcoord1.z;
                tmp0.y = saturate(tmp0.y * _SoftFactor);
                tmp1 = tex2D(_MainTex, inp.texcoord.xy);
                tmp1 = tmp1.xyzx * inp.color;
                tmp0.y = tmp0.y * tmp1.w;
                tmp2.w = tmp0.y * _BrightnessBoost;
                tmp0.yzw = _CustomFogColor.xyz * _CustomFogColorMultiplier.xxx;
                tmp2.xyz = tmp0.yzw * tmp1.xyz;
                o.sv_target = tmp0.xxxx * float4(0.0039063, 0.0039063, 0.0039063, 0.0039063) + tmp2;
                return o;
			}
			ENDCG
		}
	}
}