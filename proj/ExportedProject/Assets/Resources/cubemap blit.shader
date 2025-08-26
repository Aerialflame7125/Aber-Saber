Shader "Oculus/Cubemap Blit" {
	Properties {
		_MainTex ("Base (RGB) Trans (A)", Cube) = "white" {}
		_face ("Face", Float) = 0
		_linearToSrgb ("Perform linear-to-gamma conversion", Float) = 0
		_premultiply ("Cubemap Blit", Float) = 1
	}
	SubShader {
		Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			ZWrite Off
			GpuProgramID 46655
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float3 texcoord : TEXCOORD0;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			int _face;
			// $Globals ConstantBuffers for Fragment Shader
			int _linearToSrgb;
			int _premultiply;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			samplerCUBE _MainTex;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                cb0[0].xyz = float3(1.0, -1.0, 1.0);
                cb0[1].xyz = float3(-1.0, -1.0, -1.0);
                cb0[2].xyz = float3(-1.0, 1.0, 1.0);
                _face = float3(-1.0, -1.0, -1.0);
                cb0[4].xyz = float3(-1.0, -1.0, 1.0);
                cb0[5].xyz = float3(1.0, -1.0, -1.0);
                unity_ObjectToWorld._m00_m20 = float2(0.0, -1.0);
                unity_ObjectToWorld._m01_m21 = float2(0.0, 1.0);
                unity_ObjectToWorld._m02_m22 = float2(1.0, 0.0);
                unity_ObjectToWorld._m03_m23 = float2(1.0, 0.0);
                cb1[4].xz = float2(1.0, 0.0);
                cb1[5].xz = float2(-1.0, 0.0);
                cb2[0].yz = float2(1.0, 0.0);
                cb2[1].yz = float2(1.0, 0.0);
                cb2[2].yz = float2(0.0, -1.0);
                cb2[3].yz = float2(0.0, 1.0);
                cb2[4].yz = float2(1.0, 0.0);
                cb2[5].yz = float2(1.0, 0.0);
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp0 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp1 = tmp0.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp1 = unity_MatrixVP._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                o.position = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                tmp0.x = _face;
                tmp0.yz = unity_ObjectToWorld._m00_m20;
                tmp0.w = v.texcoord.x * 2.0;
                tmp1.xz = tmp0.yz * tmp0.ww;
                tmp0.yzw = cb0[0].xyz;
                tmp2.xy = cb2[0].yz;
                tmp1.y = 0.0;
                tmp0.xyz = tmp1.xyz + tmp0.yzw;
                tmp1.xyz = v.texcoord.yyy * float3(-1.0, -2.0, -2.0) + float3(1.0, 2.0, 2.0);
                tmp2.z = 0.0;
                o.texcoord.xyz = tmp1.xyz * tmp2.zxy + tmp0.xyz;
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
                tmp0 = texCUBE(_MainTex, inp.texcoord.xyz);
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