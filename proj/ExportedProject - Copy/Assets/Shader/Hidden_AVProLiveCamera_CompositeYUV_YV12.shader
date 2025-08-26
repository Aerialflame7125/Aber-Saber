Shader "Hidden/AVProLiveCamera/CompositeYUV_YV12" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MainU ("Base (RGB)", 2D) = "white" {}
		_MainV ("Base (RGB)", 2D) = "white" {}
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
			GpuProgramID 28184
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
			float4 _MainU_ST;
			// $Globals ConstantBuffers for Fragment Shader
			float _TextureWidth;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			sampler2D _MainU;
			sampler2D _MainV;
			
			// Keywords: SWAP_RED_BLUE_ON
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
                o.texcoord1.xy = v.texcoord1.xy * _MainU_ST.xy + _MainU_ST.zw;
                return o;
			}
			// Keywords: SWAP_RED_BLUE_ON
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                float4 tmp4;
                float4 tmp5;
                tmp0.x = inp.texcoord.x * _TextureWidth;
                tmp0.y = tmp0.x * 0.5;
                tmp0.xy = frac(tmp0.xy);
                tmp0.xzw = tmp0.xxx >= float3(0.25, 0.5, 0.75);
                tmp1.xyz = tmp0.yyy >= float3(0.25, 0.5, 0.75);
                tmp2 = tex2D(_MainU, inp.texcoord1.xy);
                tmp3.xz = tmp2.yy;
                tmp4 = tex2D(_MainV, inp.texcoord1.xy);
                tmp3.yw = tmp4.yy;
                tmp5.xz = tmp2.xx;
                tmp5.yw = tmp4.xx;
                tmp3 = tmp1.xxxx ? tmp3 : tmp5;
                tmp5.xz = tmp2.zz;
                tmp2.xz = tmp2.ww;
                tmp5.yw = tmp4.zz;
                tmp2.yw = tmp4.ww;
                tmp3 = tmp1.yyyy ? tmp5 : tmp3;
                tmp1 = tmp1.zzzz ? tmp2 : tmp3;
                tmp1 = tmp1 - float4(0.5, 0.5, 0.5, 0.5);
                tmp2 = tex2D(_MainTex, inp.texcoord.xy);
                tmp0.x = tmp0.x ? tmp2.y : tmp2.x;
                tmp0.x = tmp0.z ? tmp2.z : tmp0.x;
                tmp0.x = tmp0.w ? tmp2.w : tmp0.x;
                tmp0.y = -tmp1.y * 0.344 + tmp0.x;
                o.sv_target.xz = saturate(tmp1.xw * float2(1.402, 1.772) + tmp0.xx);
                o.sv_target.y = saturate(-tmp1.z * 0.714 + tmp0.y);
                o.sv_target.w = 1.0;
                return o;
			}
			ENDCG
		}
	}
}