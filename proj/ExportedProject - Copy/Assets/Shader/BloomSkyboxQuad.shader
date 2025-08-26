Shader "Custom/Base/BloomSkyboxQuad" {
	Properties {
	}
	SubShader {
		Tags { "QUEUE" = "Geometry" "RenderType" = "Opaque" }
		Pass {
			Tags { "QUEUE" = "Geometry" "RenderType" = "Opaque" }
			ZWrite Off
			Cull Off
			GpuProgramID 16159
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
                o.position.xy = v.vertex.xy;
                o.position.zw = float2(0.0, 1.0);
                tmp0.x = v.vertex.y * _ProjectionParams.x;
                tmp0.z = tmp0.x * 0.5;
                tmp0.x = v.vertex.x * 0.5;
                o.texcoord.xy = tmp0.xz + float2(0.5, 0.5);
                o.texcoord.zw = float2(0.0, 1.0);
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