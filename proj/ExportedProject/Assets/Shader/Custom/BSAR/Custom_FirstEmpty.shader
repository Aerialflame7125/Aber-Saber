Shader "Custom/FirstEmpty" {
	Properties {
	}
	SubShader {
		Tags { "QUEUE" = "Geometry-1999" "RenderType" = "Opaque" }
		Pass {
			Tags { "QUEUE" = "Geometry-1999" "RenderType" = "Opaque" }
			GpuProgramID 19304
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
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
                o.position = float4(0.0, 0.0, 0.0, 1.0);
                return o;
			}
			// Keywords: 
			fout frag(v2f inp)
			{
                fout o;
                if (NaN) {
                    discard;
                }
                o.sv_target = float4(0.0, 0.0, 0.0, 0.0);
                return o;
			}
			ENDCG
		}
	}
}