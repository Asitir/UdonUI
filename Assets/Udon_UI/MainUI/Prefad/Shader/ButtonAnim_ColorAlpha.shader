// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33518,y:32850,varname:node_9361,prsc:2|custl-623-OUT,alpha-8595-OUT;n:type:ShaderForge.SFN_Slider,id:4683,x:31840,y:33433,ptovrint:False,ptlb:Anim,ptin:_Anim,varname:node_4683,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:4208,x:32307,y:32584,varname:node_4208,prsc:2|A-2468-RGB,B-7824-RGB,T-4683-OUT;n:type:ShaderForge.SFN_Color,id:2468,x:31971,y:32441,ptovrint:False,ptlb:Start_Color,ptin:_Start_Color,varname:node_2468,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:7824,x:31970,y:32714,ptovrint:False,ptlb:End_Color,ptin:_End_Color,varname:node_7824,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.4811321,c3:0.4811321,c4:1;n:type:ShaderForge.SFN_TexCoord,id:7277,x:31622,y:32830,varname:node_7277,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Distance,id:653,x:32002,y:32937,varname:node_653,prsc:2|A-7277-UVOUT,B-5765-OUT;n:type:ShaderForge.SFN_Append,id:1866,x:31722,y:33074,varname:node_1866,prsc:2|A-490-OUT,B-869-OUT;n:type:ShaderForge.SFN_ValueProperty,id:490,x:31498,y:33048,ptovrint:False,ptlb:Start_X_Pos,ptin:_Start_X_Pos,varname:node_490,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:869,x:31498,y:33126,ptovrint:False,ptlb:Start_Y_Pos,ptin:_Start_Y_Pos,varname:node_869,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Lerp,id:5765,x:31976,y:33158,varname:node_5765,prsc:2|A-1866-OUT,B-9019-OUT,T-4683-OUT;n:type:ShaderForge.SFN_Append,id:9019,x:31722,y:33233,varname:node_9019,prsc:2|A-1374-OUT,B-6077-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1374,x:31498,y:33209,ptovrint:False,ptlb:End_X_Pos,ptin:_End_X_Pos,varname:node_1374,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:6077,x:31498,y:33289,ptovrint:False,ptlb:End_Y_Pos,ptin:_End_Y_Pos,varname:node_6077,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Step,id:7843,x:32457,y:33008,varname:node_7843,prsc:2|A-653-OUT,B-3893-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4023,x:32191,y:33160,ptovrint:False,ptlb:Start_SphereRange,ptin:_Start_SphereRange,varname:node_4023,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:5076,x:32191,y:33223,ptovrint:False,ptlb:End_SphereRange,ptin:_End_SphereRange,varname:node_5076,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Lerp,id:3893,x:32407,y:33160,varname:node_3893,prsc:2|A-4023-OUT,B-5076-OUT,T-4683-OUT;n:type:ShaderForge.SFN_OneMinus,id:7339,x:32722,y:33006,varname:node_7339,prsc:2|IN-7843-OUT;n:type:ShaderForge.SFN_Multiply,id:5104,x:32681,y:32821,varname:node_5104,prsc:2|A-4208-OUT,B-7339-OUT;n:type:ShaderForge.SFN_Color,id:3367,x:32614,y:33289,ptovrint:False,ptlb:SphereColor,ptin:_SphereColor,varname:node_3367,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.122489,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:3766,x:32806,y:33173,varname:node_3766,prsc:2|A-7843-OUT,B-3367-RGB;n:type:ShaderForge.SFN_Add,id:9483,x:32980,y:32897,varname:node_9483,prsc:2|A-5104-OUT,B-3766-OUT;n:type:ShaderForge.SFN_Slider,id:2659,x:32550,y:33837,ptovrint:False,ptlb:AnimDown,ptin:_AnimDown,varname:node_2659,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Color,id:2378,x:32707,y:33598,ptovrint:False,ptlb:DownColor,ptin:_DownColor,varname:node_2378,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:6764,x:33065,y:33542,varname:node_6764,prsc:2|A-3124-OUT,B-2378-RGB,T-2659-OUT;n:type:ShaderForge.SFN_Add,id:623,x:33281,y:33001,varname:node_623,prsc:2|A-9483-OUT,B-6764-OUT;n:type:ShaderForge.SFN_Vector3,id:3124,x:32842,y:33479,varname:node_3124,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Lerp,id:5794,x:33034,y:33236,varname:node_5794,prsc:2|A-2468-A,B-7824-A,T-4683-OUT;n:type:ShaderForge.SFN_Add,id:8825,x:33209,y:33200,varname:node_8825,prsc:2|A-7843-OUT,B-5794-OUT;n:type:ShaderForge.SFN_Clamp01,id:8595,x:33356,y:33167,varname:node_8595,prsc:2|IN-8825-OUT;proporder:4683-2468-7824-3367-490-869-4023-1374-6077-5076-2659-2378;pass:END;sub:END;*/

Shader "UdonUI/ButtonAnim/ColorAlpha" {
    Properties {
        _Anim ("Anim", Range(0, 1)) = 0
        [HDR]_Start_Color ("Start_Color", Color) = (0.5,0.5,0.5,1)
        [HDR]_End_Color ("End_Color", Color) = (1,0.4811321,0.4811321,1)
        [HDR]_SphereColor ("SphereColor", Color) = (0,0.122489,1,1)
        _Start_X_Pos ("Start_X_Pos", Float ) = 0
        _Start_Y_Pos ("Start_Y_Pos", Float ) = 0.5
        _Start_SphereRange ("Start_SphereRange", Float ) = 0
        _End_X_Pos ("End_X_Pos", Float ) = 0
        _End_Y_Pos ("End_Y_Pos", Float ) = 0
        _End_SphereRange ("End_SphereRange", Float ) = 1
        _AnimDown ("AnimDown", Range(0, 1)) = 0
        [HDR]_DownColor ("DownColor", Color) = (1,1,1,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Anim)
                UNITY_DEFINE_INSTANCED_PROP( float4, _Start_Color)
                UNITY_DEFINE_INSTANCED_PROP( float4, _End_Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _Start_X_Pos)
                UNITY_DEFINE_INSTANCED_PROP( float, _Start_Y_Pos)
                UNITY_DEFINE_INSTANCED_PROP( float, _End_X_Pos)
                UNITY_DEFINE_INSTANCED_PROP( float, _End_Y_Pos)
                UNITY_DEFINE_INSTANCED_PROP( float, _Start_SphereRange)
                UNITY_DEFINE_INSTANCED_PROP( float, _End_SphereRange)
                UNITY_DEFINE_INSTANCED_PROP( float4, _SphereColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _AnimDown)
                UNITY_DEFINE_INSTANCED_PROP( float4, _DownColor)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
////// Lighting:
                float4 _Start_Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Start_Color );
                float4 _End_Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _End_Color );
                float _Anim_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Anim );
                float _Start_X_Pos_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Start_X_Pos );
                float _Start_Y_Pos_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Start_Y_Pos );
                float _End_X_Pos_var = UNITY_ACCESS_INSTANCED_PROP( Props, _End_X_Pos );
                float _End_Y_Pos_var = UNITY_ACCESS_INSTANCED_PROP( Props, _End_Y_Pos );
                float _Start_SphereRange_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Start_SphereRange );
                float _End_SphereRange_var = UNITY_ACCESS_INSTANCED_PROP( Props, _End_SphereRange );
                float node_7843 = step(distance(i.uv0,lerp(float2(_Start_X_Pos_var,_Start_Y_Pos_var),float2(_End_X_Pos_var,_End_Y_Pos_var),_Anim_var)),lerp(_Start_SphereRange_var,_End_SphereRange_var,_Anim_var));
                float4 _SphereColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _SphereColor );
                float4 _DownColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _DownColor );
                float _AnimDown_var = UNITY_ACCESS_INSTANCED_PROP( Props, _AnimDown );
                float3 finalColor = (((lerp(_Start_Color_var.rgb,_End_Color_var.rgb,_Anim_var)*(1.0 - node_7843))+(node_7843*_SphereColor_var.rgb))+lerp(float3(0,0,0),_DownColor_var.rgb,_AnimDown_var));
                fixed4 finalRGBA = fixed4(finalColor,saturate((node_7843+lerp(_Start_Color_var.a,_End_Color_var.a,_Anim_var))));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
