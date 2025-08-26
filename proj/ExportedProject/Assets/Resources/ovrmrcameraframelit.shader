Shader "Oculus/OVRMRCameraFrameLit" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_DepthTex ("Depth (cm)", 2D) = "black" {}
		_InconfidenceTex ("Inconfidence (0-100)", 2D) = "black" {}
		_Visible ("Visible", Range(0, 1)) = 1
	}
	SubShader {
		LOD 200
		Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Name "FORWARD"
			LOD 200
			Tags { "LIGHTMODE" = "FORWARDBASE" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			ZWrite Off
			GpuProgramID 18302
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
				float4 texcoord7 : TEXCOORD7;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float4 _LightColor0;
			float4 _ChromaKeyColor;
			float _ChromaKeySimilarity;
			float _ChromaKeySmoothRange;
			float _ChromaKeySpillRange;
			float4 _TextureDimension;
			float4 _TextureWorldSize;
			float _SmoothFactor;
			float _DepthVariationClamp;
			float _CullingDistance;
			float4 _Color;
			float _Visible;
			float4 _FlipParams;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MaskTex;
			sampler2D _MainTex;
			sampler2D _DepthTex;
			
			// Keywords: DIRECTIONAL
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                float4 tmp4;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                tmp0.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp2 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp2 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp2;
                tmp2 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp2;
                tmp1 = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp2;
                o.position = tmp1;
                o.texcoord.w = tmp0.x;
                tmp2.y = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp2.z = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp2.x = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp0.x = dot(tmp2.xyz, tmp2.xyz);
                tmp0.x = rsqrt(tmp0.x);
                tmp2.xyz = tmp0.xxx * tmp2.xyz;
                tmp3.xyz = v.tangent.yyy * unity_ObjectToWorld._m11_m21_m01;
                tmp3.xyz = unity_ObjectToWorld._m10_m20_m00 * v.tangent.xxx + tmp3.xyz;
                tmp3.xyz = unity_ObjectToWorld._m12_m22_m02 * v.tangent.zzz + tmp3.xyz;
                tmp0.x = dot(tmp3.xyz, tmp3.xyz);
                tmp0.x = rsqrt(tmp0.x);
                tmp3.xyz = tmp0.xxx * tmp3.xyz;
                tmp4.xyz = tmp2.xyz * tmp3.xyz;
                tmp4.xyz = tmp2.zxy * tmp3.yzx + -tmp4.xyz;
                tmp0.x = v.tangent.w * unity_WorldTransformParams.w;
                tmp4.xyz = tmp0.xxx * tmp4.xyz;
                o.texcoord.y = tmp4.x;
                o.texcoord.x = tmp3.z;
                o.texcoord.z = tmp2.y;
                o.texcoord1.x = tmp3.x;
                o.texcoord2.x = tmp3.y;
                o.texcoord1.z = tmp2.z;
                o.texcoord2.z = tmp2.x;
                o.texcoord1.w = tmp0.y;
                o.texcoord2.w = tmp0.z;
                o.texcoord1.y = tmp4.y;
                o.texcoord2.y = tmp4.z;
                tmp0.x = tmp1.y * _ProjectionParams.x;
                tmp0.w = tmp0.x * 0.5;
                tmp0.xz = tmp1.xw * float2(0.5, 0.5);
                o.texcoord3.zw = tmp1.zw;
                o.texcoord3.xy = tmp0.zz + tmp0.xw;
                o.texcoord7 = float4(0.0, 0.0, 0.0, 0.0);
                return o;
			}
			// Keywords: DIRECTIONAL
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                float4 tmp4;
                float4 tmp5;
                tmp0.xy = inp.texcoord3.yx / inp.texcoord3.ww;
                tmp0.zw = _FlipParams.xy > float2(0.0, 0.0);
                tmp1.xy = float2(1.0, 1.0) - tmp0.xy;
                tmp0.x = tmp0.z ? tmp1.y : tmp0.y;
                tmp0.z = 1.0 - tmp1.x;
                tmp0.y = tmp0.w ? tmp0.z : tmp1.x;
                tmp1 = tex2D(_MaskTex, tmp0.xy);
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
                tmp1 = tex2D(_DepthTex, tmp0.xy);
                i = tmp1.x * 0.01;
                i = _CullingDistance < i;
                if (i) {
                    discard;
                }
                tmp1 = tex2D(_MainTex, tmp0.xy);
                tmp2.xyz = tmp1.xyz * _Color.xyz;
                tmp0.z = tmp0.z * 0.1111111 + -_ChromaKeySimilarity;
                tmp3.xy = float2(1.0, 1.0) / float2(_ChromaKeySmoothRange.x, _ChromaKeySpillRange.x);
                tmp0.zw = saturate(tmp0.zz * tmp3.xy);
                tmp3.xy = tmp0.zw * float2(-2.0, -2.0) + float2(3.0, 3.0);
                tmp0.zw = tmp0.zw * tmp0.zw;
                tmp0.zw = tmp0.zw * tmp3.xy;
                tmp3.xy = tmp0.zw * tmp0.zw;
                tmp0.z = i * tmp3.y;
                i = dot(tmp2.xyz, float3(0.2126, 0.7152, 0.0722));
                tmp1.xyz = tmp1.xyz * _Color.xyz + -tmp0.www;
                tmp1.xyz = tmp0.zzz * tmp1.xyz + tmp0.www;
                tmp2.xw = _TextureDimension.zw;
                tmp2.yz = float2(0.0, 0.0);
                tmp4 = tmp0.xyxy + tmp2;
                tmp5 = tex2D(_DepthTex, tmp4.xy);
                tmp0 = tmp0.xyxy - tmp2;
                tmp2 = tex2D(_DepthTex, tmp0.xy);
                tmp0.x = tmp2.x * 0.01;
                tmp0.x = tmp5.x * 0.01 + -tmp0.x;
                tmp2 = tex2D(_DepthTex, tmp4.zw);
                tmp4 = tex2D(_DepthTex, tmp0.zw);
                tmp0.y = tmp4.x * 0.01;
                tmp0.y = tmp2.x * 0.01 + -tmp0.y;
                tmp0.x = max(tmp0.x, -_DepthVariationClamp);
                tmp2.y = min(tmp0.x, _DepthVariationClamp);
                tmp0.x = max(tmp0.y, -_DepthVariationClamp);
                tmp2.x = min(tmp0.x, _DepthVariationClamp);
                tmp0.xy = _TextureDimension.zw * _TextureWorldSize.xy;
                tmp4.z = dot(tmp0.xy, float2(_SmoothFactor.x, _DepthVariationClamp.x));
                tmp2.z = dot(tmp0.xy, float2(_SmoothFactor.x, _DepthVariationClamp.x));
                tmp4.xy = float2(0.0, 0.0);
                tmp2.w = tmp4.z;
                tmp0.xy = tmp2.zx * tmp2.yw;
                tmp0.z = 0.0;
                tmp0.xyz = tmp4.xyz * tmp2.xyz + -tmp0.xyz;
                i = dot(tmp0.xyz, tmp0.xyz);
                i = rsqrt(i);
                tmp0.xyz = tmp0.www * tmp0.xyz;
                tmp2.w = tmp3.x * _Visible;
                i = unity_ProbeVolumeParams.x == 1.0;
                if (i) {
                    i = unity_ProbeVolumeParams.y == 1.0;
                    tmp3.xyz = inp.texcoord1.www * unity_ProbeVolumeWorldToObject._m01_m11_m21;
                    tmp3.xyz = unity_ProbeVolumeWorldToObject._m00_m10_m20 * inp.texcoord.www + tmp3.xyz;
                    tmp3.xyz = unity_ProbeVolumeWorldToObject._m02_m12_m22 * inp.texcoord2.www + tmp3.xyz;
                    tmp3.xyz = tmp3.xyz + unity_ProbeVolumeWorldToObject._m03_m13_m23;
                    tmp4.y = inp.texcoord.w;
                    tmp4.z = inp.texcoord1.w;
                    tmp4.w = inp.texcoord2.w;
                    tmp3.xyz = tmp0.www ? tmp3.xyz : tmp4.yzw;
                    tmp3.xyz = tmp3.xyz - unity_ProbeVolumeMin;
                    tmp3.yzw = tmp3.xyz * unity_ProbeVolumeSizeInv;
                    i = tmp3.y * 0.25 + 0.75;
                    tmp1.w = unity_ProbeVolumeParams.z * 0.5 + 0.75;
                    tmp3.x = max(i, tmp1.w);
                    tmp3 = UNITY_SAMPLE_TEX3D_SAMPLER(unity_ProbeVolumeSH, unity_ProbeVolumeSH, tmp3.xzw);
                } else {
                    tmp3 = float4(1.0, 1.0, 1.0, 1.0);
                }
                i = saturate(dot(tmp3, unity_OcclusionMaskSelector));
                tmp3.x = dot(inp.texcoord.xyz, tmp0.xyz);
                tmp3.y = dot(inp.texcoord1.xyz, tmp0.xyz);
                tmp3.z = dot(inp.texcoord2.xyz, tmp0.xyz);
                tmp0.x = dot(tmp3.xyz, tmp3.xyz);
                tmp0.x = rsqrt(tmp0.x);
                tmp0.xyz = tmp0.xxx * tmp3.xyz;
                tmp3.xyz = tmp0.www * _LightColor0.xyz;
                tmp0.x = dot(tmp0.xyz, _WorldSpaceLightPos0.xyz);
                tmp0.x = max(tmp0.x, 0.0);
                tmp0.yzw = tmp1.xyz * tmp3.xyz;
                tmp2.xyz = tmp0.xxx * tmp0.yzw;
                o.sv_target = tmp2;
                return o;
			}
			ENDCG
		}
		Pass {
			Name "FORWARD"
			LOD 200
			Tags { "LIGHTMODE" = "FORWARDADD" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha One, SrcAlpha One
			ColorMask RGB
			ZWrite Off
			GpuProgramID 91498
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float3 texcoord : TEXCOORD0;
				float3 texcoord1 : TEXCOORD1;
				float3 texcoord2 : TEXCOORD2;
				float3 texcoord3 : TEXCOORD3;
				float4 texcoord4 : TEXCOORD4;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float4x4 unity_WorldToLight;
			float4 _LightColor0;
			float4 _ChromaKeyColor;
			float _ChromaKeySimilarity;
			float _ChromaKeySmoothRange;
			float _ChromaKeySpillRange;
			float4 _TextureDimension;
			float4 _TextureWorldSize;
			float _SmoothFactor;
			float _DepthVariationClamp;
			float _CullingDistance;
			float4 _Color;
			float _Visible;
			float4 _FlipParams;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MaskTex;
			sampler2D _MainTex;
			sampler2D _DepthTex;
			sampler2D _LightTexture0;
			
			// Keywords: POINT
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                o.texcoord3.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp0 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp0 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp0;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp0;
                tmp0 = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp0;
                o.position = tmp0;
                tmp1.y = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp1.z = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp1.x = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp1.w = dot(tmp1.xyz, tmp1.xyz);
                tmp1.w = rsqrt(tmp1.w);
                tmp1.xyz = tmp1.www * tmp1.xyz;
                tmp2.xyz = v.tangent.yyy * unity_ObjectToWorld._m11_m21_m01;
                tmp2.xyz = unity_ObjectToWorld._m10_m20_m00 * v.tangent.xxx + tmp2.xyz;
                tmp2.xyz = unity_ObjectToWorld._m12_m22_m02 * v.tangent.zzz + tmp2.xyz;
                tmp1.w = dot(tmp2.xyz, tmp2.xyz);
                tmp1.w = rsqrt(tmp1.w);
                tmp2.xyz = tmp1.www * tmp2.xyz;
                tmp3.xyz = tmp1.xyz * tmp2.xyz;
                tmp3.xyz = tmp1.zxy * tmp2.yzx + -tmp3.xyz;
                tmp1.w = v.tangent.w * unity_WorldTransformParams.w;
                tmp3.xyz = tmp1.www * tmp3.xyz;
                o.texcoord.y = tmp3.x;
                o.texcoord.x = tmp2.z;
                o.texcoord.z = tmp1.y;
                o.texcoord1.x = tmp2.x;
                o.texcoord2.x = tmp2.y;
                o.texcoord1.z = tmp1.z;
                o.texcoord2.z = tmp1.x;
                o.texcoord1.y = tmp3.y;
                o.texcoord2.y = tmp3.z;
                tmp0.y = tmp0.y * _ProjectionParams.x;
                tmp1.xzw = tmp0.xwy * float3(0.5, 0.5, 0.5);
                o.texcoord4.zw = tmp0.zw;
                o.texcoord4.xy = tmp1.zz + tmp1.xw;
                return o;
			}
			// Keywords: POINT
			fout frag(v2f inp)
			{
                fout o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                float4 tmp4;
                float4 tmp5;
                tmp0.xy = inp.texcoord4.yx / inp.texcoord4.ww;
                tmp0.zw = _FlipParams.xy > float2(0.0, 0.0);
                tmp1.xy = float2(1.0, 1.0) - tmp0.xy;
                tmp0.x = tmp0.z ? tmp1.y : tmp0.y;
                tmp0.z = 1.0 - tmp1.x;
                tmp0.y = tmp0.w ? tmp0.z : tmp1.x;
                tmp1 = tex2D(_MaskTex, tmp0.xy);
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
                tmp1 = tex2D(_DepthTex, tmp0.xy);
                i = tmp1.x * 0.01;
                i = _CullingDistance < i;
                if (i) {
                    discard;
                }
                tmp1 = tex2D(_MainTex, tmp0.xy);
                tmp2.xyz = tmp1.xyz * _Color.xyz;
                tmp0.z = tmp0.z * 0.1111111 + -_ChromaKeySimilarity;
                tmp3.xy = float2(1.0, 1.0) / float2(_ChromaKeySmoothRange.x, _ChromaKeySpillRange.x);
                tmp0.zw = saturate(tmp0.zz * tmp3.xy);
                tmp3.xy = tmp0.zw * float2(-2.0, -2.0) + float2(3.0, 3.0);
                tmp0.zw = tmp0.zw * tmp0.zw;
                tmp0.zw = tmp0.zw * tmp3.xy;
                tmp3.xy = tmp0.zw * tmp0.zw;
                tmp0.z = i * tmp3.y;
                i = dot(tmp2.xyz, float3(0.2126, 0.7152, 0.0722));
                tmp1.xyz = tmp1.xyz * _Color.xyz + -tmp0.www;
                tmp1.xyz = tmp0.zzz * tmp1.xyz + tmp0.www;
                tmp2.xw = _TextureDimension.zw;
                tmp2.yz = float2(0.0, 0.0);
                tmp4 = tmp0.xyxy + tmp2;
                tmp5 = tex2D(_DepthTex, tmp4.xy);
                tmp0 = tmp0.xyxy - tmp2;
                tmp2 = tex2D(_DepthTex, tmp0.xy);
                tmp0.x = tmp2.x * 0.01;
                tmp0.x = tmp5.x * 0.01 + -tmp0.x;
                tmp2 = tex2D(_DepthTex, tmp4.zw);
                tmp4 = tex2D(_DepthTex, tmp0.zw);
                tmp0.y = tmp4.x * 0.01;
                tmp0.y = tmp2.x * 0.01 + -tmp0.y;
                tmp0.x = max(tmp0.x, -_DepthVariationClamp);
                tmp2.y = min(tmp0.x, _DepthVariationClamp);
                tmp0.x = max(tmp0.y, -_DepthVariationClamp);
                tmp2.x = min(tmp0.x, _DepthVariationClamp);
                tmp0.xy = _TextureDimension.zw * _TextureWorldSize.xy;
                tmp4.z = dot(tmp0.xy, float2(_SmoothFactor.x, _DepthVariationClamp.x));
                tmp2.z = dot(tmp0.xy, float2(_SmoothFactor.x, _DepthVariationClamp.x));
                tmp4.xy = float2(0.0, 0.0);
                tmp2.w = tmp4.z;
                tmp0.xy = tmp2.zx * tmp2.yw;
                tmp0.z = 0.0;
                tmp0.xyz = tmp4.xyz * tmp2.xyz + -tmp0.xyz;
                i = dot(tmp0.xyz, tmp0.xyz);
                i = rsqrt(i);
                tmp0.xyz = tmp0.www * tmp0.xyz;
                tmp2.w = tmp3.x * _Visible;
                tmp3.xyz = _WorldSpaceLightPos0.xyz - inp.texcoord3.xyz;
                i = dot(tmp3.xyz, tmp3.xyz);
                i = rsqrt(i);
                tmp3.xyz = tmp0.www * tmp3.xyz;
                tmp4.xyz = inp.texcoord3.yyy * unity_WorldToLight._m01_m11_m21;
                tmp4.xyz = unity_WorldToLight._m00_m10_m20 * inp.texcoord3.xxx + tmp4.xyz;
                tmp4.xyz = unity_WorldToLight._m02_m12_m22 * inp.texcoord3.zzz + tmp4.xyz;
                tmp4.xyz = tmp4.xyz + unity_WorldToLight._m03_m13_m23;
                i = unity_ProbeVolumeParams.x == 1.0;
                if (i) {
                    i = unity_ProbeVolumeParams.y == 1.0;
                    tmp5.xyz = inp.texcoord3.yyy * unity_ProbeVolumeWorldToObject._m01_m11_m21;
                    tmp5.xyz = unity_ProbeVolumeWorldToObject._m00_m10_m20 * inp.texcoord3.xxx + tmp5.xyz;
                    tmp5.xyz = unity_ProbeVolumeWorldToObject._m02_m12_m22 * inp.texcoord3.zzz + tmp5.xyz;
                    tmp5.xyz = tmp5.xyz + unity_ProbeVolumeWorldToObject._m03_m13_m23;
                    tmp5.xyz = tmp0.www ? tmp5.xyz : inp.texcoord3.xyz;
                    tmp5.xyz = tmp5.xyz - unity_ProbeVolumeMin;
                    tmp5.yzw = tmp5.xyz * unity_ProbeVolumeSizeInv;
                    i = tmp5.y * 0.25 + 0.75;
                    tmp1.w = unity_ProbeVolumeParams.z * 0.5 + 0.75;
                    tmp5.x = max(i, tmp1.w);
                    tmp5 = UNITY_SAMPLE_TEX3D_SAMPLER(unity_ProbeVolumeSH, unity_ProbeVolumeSH, tmp5.xzw);
                } else {
                    tmp5 = float4(1.0, 1.0, 1.0, 1.0);
                }
                i = saturate(dot(tmp5, unity_OcclusionMaskSelector));
                tmp1.w = dot(tmp4.xyz, tmp4.xyz);
                tmp4 = tex2D(_LightTexture0, tmp1.ww);
                i = i * tmp4.x;
                tmp4.x = dot(inp.texcoord.xyz, tmp0.xyz);
                tmp4.y = dot(inp.texcoord1.xyz, tmp0.xyz);
                tmp4.z = dot(inp.texcoord2.xyz, tmp0.xyz);
                tmp0.x = dot(tmp4.xyz, tmp4.xyz);
                tmp0.x = rsqrt(tmp0.x);
                tmp0.xyz = tmp0.xxx * tmp4.xyz;
                tmp4.xyz = tmp0.www * _LightColor0.xyz;
                tmp0.x = dot(tmp0.xyz, tmp3.xyz);
                tmp0.x = max(tmp0.x, 0.0);
                tmp0.yzw = tmp1.xyz * tmp4.xyz;
                tmp2.xyz = tmp0.xxx * tmp0.yzw;
                o.sv_target = tmp2;
                return o;
			}
			ENDCG
		}
		Pass {
			Name "META"
			LOD 200
			Tags { "LIGHTMODE" = "META" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			ColorMask RGB
			ZWrite Off
			Cull Off
			GpuProgramID 173320
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION0;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 texcoord3 : TEXCOORD3;
			};
			struct fout
			{
				float4 sv_target : SV_Target0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float4 _ChromaKeyColor;
			float _ChromaKeySimilarity;
			float _ChromaKeySpillRange;
			float4 _TextureDimension;
			float _CullingDistance;
			float4 _Color;
			float4 _FlipParams;
			float unity_OneOverOutputBoost;
			float unity_MaxOutputValue;
			// Custom ConstantBuffers for Vertex Shader
			CBUFFER_START(UnityMetaPass)
				bool4 unity_MetaVertexControl;
			CBUFFER_END
			// Custom ConstantBuffers for Fragment Shader
			CBUFFER_START(UnityMetaPass)
				bool4 unity_MetaFragmentControl;
			CBUFFER_END
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MaskTex;
			sampler2D _MainTex;
			sampler2D _DepthTex;
			
			// Keywords: 
			v2f vert(appdata_full v)
			{
                v2f o;
                float4 tmp0;
                float4 tmp1;
                float4 tmp2;
                float4 tmp3;
                float4 tmp4;
                tmp0.x = v.vertex.z > 0.0;
                tmp0.z = tmp0.x ? 0.0001 : 0.0;
                tmp0.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                tmp0.xyz = unity_MetaVertexControl.xxx ? tmp0.xyz : v.vertex.xyz;
                tmp0.w = tmp0.z > 0.0;
                tmp1.z = tmp0.w ? 0.0001 : 0.0;
                tmp1.xy = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                tmp0.xyz = unity_MetaVertexControl.yyy ? tmp1.xyz : tmp0.xyz;
                tmp1 = tmp0.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp1 = unity_MatrixVP._m00_m10_m20_m30 * tmp0.xxxx + tmp1;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp0.zzzz + tmp1;
                tmp0 = tmp0 + unity_MatrixVP._m03_m13_m23_m33;
                o.position = tmp0;
                tmp1.y = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp1.z = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp1.x = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp1.w = dot(tmp1.xyz, tmp1.xyz);
                tmp1.w = rsqrt(tmp1.w);
                tmp1.xyz = tmp1.www * tmp1.xyz;
                tmp2.xyz = v.tangent.yyy * unity_ObjectToWorld._m11_m21_m01;
                tmp2.xyz = unity_ObjectToWorld._m10_m20_m00 * v.tangent.xxx + tmp2.xyz;
                tmp2.xyz = unity_ObjectToWorld._m12_m22_m02 * v.tangent.zzz + tmp2.xyz;
                tmp1.w = dot(tmp2.xyz, tmp2.xyz);
                tmp1.w = rsqrt(tmp1.w);
                tmp2.xyz = tmp1.www * tmp2.xyz;
                tmp3.xyz = tmp1.xyz * tmp2.xyz;
                tmp3.xyz = tmp1.zxy * tmp2.yzx + -tmp3.xyz;
                tmp1.w = v.tangent.w * unity_WorldTransformParams.w;
                tmp3.xyz = tmp1.www * tmp3.xyz;
                o.texcoord.y = tmp3.x;
                tmp4.xyz = v.vertex.yyy * unity_ObjectToWorld._m01_m11_m21;
                tmp4.xyz = unity_ObjectToWorld._m00_m10_m20 * v.vertex.xxx + tmp4.xyz;
                tmp4.xyz = unity_ObjectToWorld._m02_m12_m22 * v.vertex.zzz + tmp4.xyz;
                tmp4.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp4.xyz;
                o.texcoord.w = tmp4.x;
                o.texcoord.x = tmp2.z;
                o.texcoord.z = tmp1.y;
                o.texcoord1.x = tmp2.x;
                o.texcoord2.x = tmp2.y;
                o.texcoord1.z = tmp1.z;
                o.texcoord2.z = tmp1.x;
                o.texcoord1.w = tmp4.y;
                o.texcoord2.w = tmp4.z;
                o.texcoord1.y = tmp3.y;
                o.texcoord2.y = tmp3.z;
                tmp0.y = tmp0.y * _ProjectionParams.x;
                tmp1.xzw = tmp0.xwy * float3(0.5, 0.5, 0.5);
                o.texcoord3.zw = tmp0.zw;
                o.texcoord3.xy = tmp1.zz + tmp1.xw;
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
                tmp0.xy = inp.texcoord3.yx / inp.texcoord3.ww;
                tmp0.zw = _FlipParams.xy > float2(0.0, 0.0);
                tmp1.xy = float2(1.0, 1.0) - tmp0.xy;
                tmp0.x = tmp0.z ? tmp1.y : tmp0.y;
                tmp0.z = 1.0 - tmp1.x;
                tmp0.y = tmp0.w ? tmp0.z : tmp1.x;
                tmp1 = tex2D(_MaskTex, tmp0.xy);
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
                tmp1 = tex2D(_DepthTex, tmp0.xy);
                i = tmp1.x * 0.01;
                i = _CullingDistance < i;
                if (i) {
                    discard;
                }
                tmp1 = tex2D(_MainTex, tmp0.xy);
                tmp0.xyw = tmp1.xyz * _Color.xyz;
                tmp0.z = tmp0.z * 0.1111111 + -_ChromaKeySimilarity;
                tmp1.w = 1.0 / _ChromaKeySpillRange;
                tmp0.z = saturate(tmp0.z * tmp1.w);
                tmp1.w = tmp0.z * -2.0 + 3.0;
                tmp0.z = tmp0.z * tmp0.z;
                tmp0.z = tmp0.z * tmp1.w;
                tmp1.w = tmp0.z * tmp0.z;
                tmp0.z = tmp0.z * tmp1.w;
                tmp0.x = dot(tmp0.xyz, float3(0.2126, 0.7152, 0.0722));
                tmp1.xyz = tmp1.xyz * _Color.xyz + -tmp0.xxx;
                tmp0.xyz = tmp0.zzz * tmp1.xyz + tmp0.xxx;
                i = saturate(unity_OneOverOutputBoost);
                tmp0.xyz = log(tmp0.xyz);
                tmp0.xyz = tmp0.xyz * tmp0.www;
                tmp0.xyz = exp(tmp0.xyz);
                tmp0.xyz = min(tmp0.xyz, unity_MaxOutputValue.xxx);
                tmp0.w = 1.0;
                tmp0 = unity_MetaFragmentControl ? tmp0 : float4(0.0, 0.0, 0.0, 0.0);
                o.sv_target = unity_MetaFragmentControl ? float4(0.0, 0.0, 0.0, 1.0) : tmp0;
                return o;
			}
			ENDCG
		}
	}
	Fallback "Alpha-Diffuse"
}