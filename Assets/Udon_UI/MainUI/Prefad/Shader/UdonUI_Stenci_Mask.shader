Shader "UdonUI/Stencil/Mask"
{
	Properties
	{
		[HideInInspector] __dirty("", Int) = 1
	}

		SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		ZWrite Off
		Stencil
		{
			Ref 129
			Comp Always
			Pass Replace
		}
		ColorMask 0
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			fixed filler;
		};

		void surf(Input input , inout SurfaceOutputStandard output)
		{
			output.Emission = float4(1,1,0,0).rgb;
			output.Alpha = 1;
		}

		ENDCG
	}
		Fallback "Diffuse"
}
