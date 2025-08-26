Shader "Hidden/MainEffect" {
	Properties {
		[HideInInspector] _MainTex ("Main Texture", 2D) = "white" {}
		[HideInInspector] _NoiseTex ("Noise Texture", 2D) = "white" {}
	}
	SubShader {
		Pass {
			Name "MAIN"
			ZTest Always
			ZWrite Off
			Cull Off
			GpuProgramID 9272
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float2 texcoord : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_ST;
			float2 _MainTex_TexelSize;
			// $Globals ConstantBuffers for Fragment Shader
			float2 _GlobalBlueNoiseParams;
			float _GlobalRandomValue;
			float _BaseColorBoost;
			float _BaseColorBoostThreshold;
			float _BloomIntensity;
			float _FadeAmount;
			float _k1256;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _BloomTex;
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
                tmp0 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp1 = tmp0.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp1 = unity_MatrixVP._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                o.position = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                tmp0.x = _MainTex_TexelSize.y < 0.0;
                tmp0.yzw = v.texcoord.xyx * _MainTex_ST.xyx + _MainTex_ST.zwz;
                tmp1.x = 1.0 - tmp0.z;
                o.texcoord1.y = tmp0.x ? tmp1.x : tmp0.z;
                o.texcoord1 = tmp0.yzw;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                tmp0.x = tmp0.w + tmp1.w;
                tmp0.x = tmp0.x * 0.5;
                tmp0.x = tmp0.x * _BaseColorBoost + -_BaseColorBoostThreshold;
                tmp0.yz = inp.texcoord.xy * _GlobalBlueNoiseParams + _GlobalRandomValue.xx;
                tmp1 = tex2D(_GlobalBlueNoiseTex, tmp0.yz);
                tmp0.y = tmp1.w - 0.5;
                tmp0.x = tmp0.y * 0.0039063 + tmp0.x;
                tmp0.z = _k1256;
                tmp0.x = saturate(tmp0.x - tmp0.z);
                tmp1.xyz = saturate(tmp0.yyy * float3(0.0039063, 0.0039063, 0.0039063) + -tmp0.zzz);
                tmp2 = tex2D(_MainTex, inp.texcoord1.xy);
                tmp0 = tmp0.xxxx + tmp2;
                tmp2 = tex2D(_MainTex, inp.texcoord1.xy);
                tmp1.w = saturate(0.0);
                tmp1 = tmp2 * _BloomIntensity.xxxx + tmp1;
                tmp0 = tmp0 + tmp1;
                tmp0.xyz = saturate(tmp0.xyz);
                o.sv_target.w = tmp1;
                o.sv_target.xyz = (tmp1 + tmp2) * _FadeAmount.xxx;
                return o;
			}
			ENDCG
		}
	}
}