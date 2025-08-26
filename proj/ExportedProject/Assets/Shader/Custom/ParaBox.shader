// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Urki/ParaBox"
{
	Properties
	{
		_Color("Color", Color) = (0,0,0,0)
		_SizeParams("Width, Length, & Center", Vector) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			half filler;
		};

		uniform float4 _SizeParams;
		uniform float4 _Color;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 _Vector0 = float2(0,1);
			float temp_output_26_0 = ( _SizeParams.x - 1.0 );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float4 transform35 = mul(unity_ObjectToWorld,float4( ( float3( ( _SizeParams.y * -_SizeParams.z * _Vector0 ) ,  0.0 ) + ( ( float3( ( float2( 1,0 ) * temp_output_26_0 ) ,  0.0 ) + ( float3(0,0,1) * temp_output_26_0 ) ) * ase_vertex3Pos ) + ( ase_vertex3Pos * _SizeParams.y * float3( _Vector0 ,  0.0 ) ) ) , 0.0 ));
			v.vertex.xyz += transform35.xyz;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Emission = _Color.rgb;
			o.Alpha = _Color.a;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18935
263;26;1273;715;913.5;104;1;True;True
Node;AmplifyShaderEditor.Vector4Node;2;-682,139.5;Inherit;False;Property;_SizeParams;Width, Length, & Center;1;0;Create;False;0;0;0;False;0;False;0,0,0,0;0.5,10,0.5,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;25;-581,-140;Inherit;False;Constant;_Blue;Blue;2;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;26;-423,184.5;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;15;-564,4.5;Inherit;False;Constant;_Red;Red;2;0;Create;True;0;0;0;False;0;False;1,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-254,91.5;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-255,184.5;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode;37;-631,318;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NegateNode;34;-405,286;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;29;-580,-271.5;Inherit;False;Constant;_Vector0;Green;2;0;Create;True;0;0;0;False;0;False;0,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-75,143.5;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;66,121.5;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-279,289;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;4,-13.5;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT2;0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;300,98.5;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;35;433,82;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;1;365,-142.5;Inherit;False;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.408737,0.5643623,0.6176471,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;621,-197;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Urki/ParaBox;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;26;0;2;1
WireConnection;13;0;15;0
WireConnection;13;1;26;0
WireConnection;14;0;25;0
WireConnection;14;1;26;0
WireConnection;34;0;2;3
WireConnection;12;0;13;0
WireConnection;12;1;14;0
WireConnection;4;0;12;0
WireConnection;4;1;37;0
WireConnection;31;0;2;2
WireConnection;31;1;34;0
WireConnection;31;2;29;0
WireConnection;28;0;37;0
WireConnection;28;1;2;2
WireConnection;28;2;29;0
WireConnection;8;0;31;0
WireConnection;8;1;4;0
WireConnection;8;2;28;0
WireConnection;35;0;8;0
WireConnection;0;2;1;0
WireConnection;0;9;1;4
WireConnection;0;11;35;0
ASEEND*/
//CHKSM=FA7190CD7945F8A2D220D8871B68C01B54C31913