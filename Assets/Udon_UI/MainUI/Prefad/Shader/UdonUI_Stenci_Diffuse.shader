Shader "UdonUI/Stencil/Diffuse"
{
	Properties
	{
		[HideInInspector] __dirty("", Int) = 1
		_Color("_Color", Color) = (1,1,1,1)
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		[HideInInspector] _texcoord("", 2D) = "white" {}
	}

		SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+1" }
		Cull Back
		Stencil
		{
			Ref 129
			Comp Equal
			Pass Keep
			Fail Keep
		}
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float4 _Color;

		void surf(Input i , inout SurfaceOutputStandard o)
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackNormal(tex2D(_Normal, uv_Normal));
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = (tex2D(_Albedo, uv_Albedo) * _Color).xyz;
			o.Alpha = 1;
		}

		ENDCG
	}
		Fallback "Diffuse"
}
