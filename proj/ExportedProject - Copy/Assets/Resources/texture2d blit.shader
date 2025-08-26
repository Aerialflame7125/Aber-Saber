Shader "Oculus/Texture2D Blit" {
	Properties {
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_linearToSrgb ("Perform linear-to-gamma conversion", Float) = 0
		_premultiply ("Pre-multiply alpha", Float) = 1
	}
	SubShader {
		Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			ZWrite Off
			GpuProgramID 28022
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float2 texcoord : TEXCOORD0;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float4 _MainTex_ST;
			// $Globals ConstantBuffers for Fragment Shader
			int _linearToSrgb;
			int _premultiply;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			
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
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
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
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp1.xyz = sqrt(tmp0.xyz);
                tmp2.xyz = sqrt(tmp1.xyz);
                tmp3.xyz = tmp2.xyz * float3(0.6841221, 0.6841221, 0.6841221);
                tmp2.xyz = sqrt(tmp2.xyz);
                tmp1.xyz = tmp1.xyz * float3(0.6620027, 0.6620027, 0.6620027) + tmp3.xyz;
                tmp1.xyz = -tmp2.xyz * float3(0.3235836, 0.3235836, 0.3235836) + tmp1.xyz;
                tmp1.xyz = -tmp0.xyz * float3(0.0225412, 0.0225412, 0.0225412) + tmp1.xyz;
                tmp0.xyz = _linearToSrgb.xxx ? tmp1.xyz : tmp0.xyz;
                tmp1.xyz = tmp0.www * tmp0.xyz;
                o.sv_target.xyz = _premultiply.xxx ? tmp1.xyz : tmp0.xyz;
                o.sv_target.w = tmp0.w;
                return o;
			}
			ENDCG
		}
	}
}