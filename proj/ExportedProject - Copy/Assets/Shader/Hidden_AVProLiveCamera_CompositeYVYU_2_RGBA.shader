Shader "Hidden/AVProLiveCamera/CompositeYVYU_2_RGBA" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TextureWidth ("Texure Width", Float) = 256
	}
	SubShader {
		Pass {
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 34038
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 sv_position : SV_Position0;
				float3 texcoord : TEXCOORD0;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			float _TextureWidth;
			float4 _MainTex_ST2;
			// $Globals ConstantBuffers for Fragment Shader
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			
			// Keywords: SWAP_RED_BLUE_ON AVPRO_GAMMACORRECTION HORIZONTAL_FLIP_ON
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
                o.sv_position = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                tmp0.x = v.vertex.x * _TextureWidth;
                o.texcoord.z = tmp0.x * 0.5;
                o.texcoord.xy = v.texcoord.xy * _MainTex_ST2.xy + _MainTex_ST2.zw;
                return o;
			}
			// Keywords: SWAP_RED_BLUE_ON AVPRO_GAMMACORRECTION HORIZONTAL_FLIP_ON
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                tmp0.x = frac(inp.texcoord.z);
                tmp0.x = tmp0.x > 0.5;
                tmp1 = tex2D(_MainTex, inp.texcoord.xy);
                tmp0.x = tmp0.x ? tmp1.x : tmp1.z;
                tmp1 = tmp1.ywyw - float4(0.5, 0.5, 0.5, 0.5);
                tmp0.y = -tmp1.y * 0.344 + tmp0.x;
                tmp0.xz = saturate(tmp1.xw * float2(1.402, 1.772) + tmp0.xx);
                tmp0.y = saturate(-tmp1.z * 0.714 + tmp0.y);
                tmp1.y = log(tmp0.y);
                tmp1.xz = log(tmp0.xz);
                tmp0.xyz = tmp1.xyz * float3(2.2, 2.2, 2.2);
                o.sv_target.xyz = exp(tmp0.xyz);
                o.sv_target.w = 1.0;
                return o;
			}
			ENDCG
		}
	}
}