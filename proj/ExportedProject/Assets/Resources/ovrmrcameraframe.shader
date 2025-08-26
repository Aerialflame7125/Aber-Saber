Shader "Oculus/OVRMRCameraFrame" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_Visible ("Visible", Range(0, 1)) = 1
		_ChromaAlphaCutoff ("ChromaAlphaCutoff", Range(0, 1)) = 0.01
		_ChromaToleranceA ("ChromaToleranceA", Range(0, 50)) = 20
		_ChromaToleranceB ("ChromaToleranceB", Range(0, 50)) = 15
		_ChromaShadows ("ChromaShadows", Range(0, 1)) = 0.02
	}
	SubShader {
		LOD 100
		Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			LOD 100
			Tags { "IGNOREPROJECTOR" = "true" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			Cull Off
			Fog {
				Mode 0
			}
			GpuProgramID 34540
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
			float _Visible;
			// $Globals ConstantBuffers for Fragment Shader
			float4 _ChromaKeyColor;
			float _ChromaKeySimilarity;
			float _ChromaKeySmoothRange;
			float _ChromaKeySpillRange;
			float4 _TextureDimension;
			float4 _Color;
			float4 _FlipParams;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MaskTex;
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
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp0.wwww + tmp1;
                o.position = tmp0 * _Visible.xxxx;
                tmp0.xy = v.texcoord.xy * float2(1.0, -1.0) + float2(0.0, 1.0);
                o.texcoord.xy = tmp0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
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
                float4 tmp4;
                tmp0.xy = _FlipParams.xy > float2(0.0, 0.0);
                tmp0.zw = float2(1.0, 1.0) - inp.texcoord.xy;
                tmp0.xy = tmp0.xy ? tmp0.zw : inp.texcoord.xy;
                tmp0.z = 1.0 - tmp0.y;
                tmp1 = tex2D(_MaskTex, tmp0.xz);
                tmp0.z = tmp1.x == 0.0;
                if (tmp0.z) {
                    discard;
                }
                tmp0.z = _ChromaKeyColor.y * 0.33609;
                tmp0.z = _ChromaKeyColor.x * -0.09991 + -tmp0.z;
                tmp1.x = _ChromaKeyColor.z * 0.436 + tmp0.z;
                tmp0.z = _ChromaKeyColor.y * 0.55861;
                tmp0.z = _ChromaKeyColor.x * 0.615 + -tmp0.z;
                tmp1.y = -_ChromaKeyColor.z * 0.05639 + tmp0.z;
                tmp0.zw = float2(0.0, 0.0);
                while (true) {
                    tmp1.z = i >= 3;
                    if (tmp1.z) {
                        break;
                    }
                    tmp1.z = floor(i);
                    tmp2.x = tmp1.z - 1.0;
                    tmp1.z = tmp0.z;
                    tmp1.w = 0.0;
                    for (int j = tmp1.w; j < 3; j += 1) {
                        tmp2.z = floor(j);
                        tmp2.y = tmp2.z - 1.0;
                        tmp2.yz = tmp2.xy * _TextureDimension.zw + tmp0.xy;
                        tmp3 = tex2D(_MainTex, tmp2.yz);
                        tmp3 = tmp3.xyzx * _Color;
                        tmp2.y = dot(tmp3.xyz, float3(0.2126, 0.7152, 0.0722));
                        tmp2.z = tmp3.y * 0.33609;
                        tmp2.z = tmp3.x * -0.09991 + -tmp2.z;
                        tmp4.x = tmp3.z * 0.436 + tmp2.z;
                        tmp2.z = tmp3.y * 0.55861;
                        tmp2.z = tmp3.w * 0.615 + -tmp2.z;
                        tmp4.y = -tmp3.z * 0.05639 + tmp2.z;
                        tmp2.zw = tmp4.xy - tmp1.xy;
                        tmp2.z = dot(tmp2.xy, tmp2.xy);
                        tmp2.z = sqrt(tmp2.z);
                        tmp2.y = saturate(tmp2.y - 0.9);
                        tmp2.y = tmp2.y + tmp2.z;
                        tmp1.z = tmp1.z + tmp2.y;
                    }
                    tmp0.z = tmp1.z;
                    i = i + 1;
                }
                tmp1 = tex2D(_MainTex, tmp0.xy);
                tmp0.xyw = tmp1.xyz * _Color.xyz;
                tmp0.z = tmp0.z * 0.1111111 + -_ChromaKeySimilarity;
                tmp2.xy = float2(1.0, 1.0) / float2(_ChromaKeySmoothRange.x, _ChromaKeySpillRange.x);
                tmp2.xy = saturate(tmp0.zz * tmp2.xy);
                tmp2.zw = tmp2.xy * float2(-2.0, -2.0) + float2(3.0, 3.0);
                tmp2.xy = tmp2.xy * tmp2.xy;
                tmp2.xy = tmp2.xy * tmp2.zw;
                o.sv_target.w = tmp2.x * tmp2.x;
                tmp0.z = tmp2.y * tmp2.y;
                tmp0.z = tmp2.y * tmp0.z;
                tmp0.x = dot(tmp0.xyz, float3(0.2126, 0.7152, 0.0722));
                tmp1.xyz = tmp1.xyz * _Color.xyz + -tmp0.xxx;
                o.sv_target.xyz = tmp0.zzz * tmp1.xyz + tmp0.xxx;
                return o;
			}
			ENDCG
		}
	}
}