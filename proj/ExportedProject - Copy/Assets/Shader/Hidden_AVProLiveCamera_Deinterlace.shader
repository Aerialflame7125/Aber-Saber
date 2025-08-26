Shader "Hidden/AVProLiveCamera/Deinterlace" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Pass {
			ZTest Always
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 49404
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
			// $Globals ConstantBuffers for Fragment Shader
			float4 _MainTex_TexelSize;
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
                o.texcoord.xy = v.texcoord.xy;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                tmp0 = tex2D(_MainTex, inp.texcoord.xy);
                tmp1.x = 0.0;
                tmp1.y = _MainTex_TexelSize.y;
                tmp1.zw = inp.texcoord.xy - tmp1.xy;
                tmp1.xy = tmp1.xy + inp.texcoord.xy;
                tmp2 = tex2D(_MainTex, tmp1.xy);
                tmp1 = tex2D(_MainTex, tmp1.zw);
                tmp0 = tmp0 * float4(2.0, 2.0, 2.0, 2.0) + tmp1;
                tmp0 = tmp2 + tmp0;
                o.sv_target = tmp0 * float4(0.25, 0.25, 0.25, 0.25);
                return o;
			}
			ENDCG
		}
	}
}