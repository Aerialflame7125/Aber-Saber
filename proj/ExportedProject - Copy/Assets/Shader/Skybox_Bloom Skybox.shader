Shader "Skybox/Bloom Skybox" {
	Properties {
	}
	SubShader {
		Tags { "QUEUE" = "Background" "RenderType" = "Background" }
		Pass {
			Tags { "QUEUE" = "Background" "RenderType" = "Background" }
			ZWrite Off
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 45595
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 texcoord : TEXCOORD0;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float4 _CustomFogColor;
			float _CustomFogColorMultiplier;
			float2 _GlobalBlueNoiseParams;
			float _GlobalRandomValue;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
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
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                o.position = tmp0;
                tmp0.y = tmp0.y * _ProjectionParams.x;
                tmp1.xzw = tmp0.xwy * float3(0.5, 0.5, 0.5);
                o.texcoord.zw = tmp0.zw;
                o.texcoord.xy = tmp1.zz + tmp1.xw;
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                tmp0.xy = inp.texcoord.xy / inp.texcoord.ww;
                tmp0.xy = tmp0.xy * _GlobalBlueNoiseParams + _GlobalRandomValue.xx;
                tmp0 = tex2D(_GlobalBlueNoiseTex, tmp0.xy);
                tmp0.x = tmp0.w - 0.5;
                tmp0.x = tmp0.x * 0.0039063;
                o.sv_target.xyz = _CustomFogColor.xyz * _CustomFogColorMultiplier.xxx + tmp0.xxx;
                o.sv_target.w = 0.0;
                return o;
			}
			ENDCG
		}
	}
}