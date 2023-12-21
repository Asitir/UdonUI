// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:34240,y:32790,varname:node_9361,prsc:2|custl-5163-OUT,alpha-6652-OUT;n:type:ShaderForge.SFN_TexCoord,id:5443,x:32415,y:33004,varname:node_5443,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Distance,id:9551,x:32634,y:33082,varname:node_9551,prsc:2|A-5443-UVOUT,B-4541-OUT;n:type:ShaderForge.SFN_Vector2,id:4541,x:32415,y:33138,varname:node_4541,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Step,id:7271,x:32860,y:33146,varname:node_7271,prsc:2|A-9551-OUT,B-5891-OUT;n:type:ShaderForge.SFN_Slider,id:7746,x:32054,y:33209,ptovrint:False,ptlb:OuterLine,ptin:_OuterLine,varname:node_7746,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4153697,max:1;n:type:ShaderForge.SFN_Step,id:2404,x:32873,y:33305,varname:node_2404,prsc:2|A-9551-OUT,B-6901-OUT;n:type:ShaderForge.SFN_Slider,id:5798,x:32057,y:33437,ptovrint:False,ptlb:LineWide,ptin:_LineWide,varname:node_5798,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.03478261,max:1;n:type:ShaderForge.SFN_Subtract,id:6901,x:32700,y:33377,varname:node_6901,prsc:2|A-5891-OUT,B-1164-OUT;n:type:ShaderForge.SFN_Subtract,id:5792,x:33031,y:33186,varname:node_5792,prsc:2|A-7271-OUT,B-2404-OUT;n:type:ShaderForge.SFN_Slider,id:7842,x:32576,y:32906,ptovrint:False,ptlb:MainSphere,ptin:_MainSphere,varname:node_7842,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3305298,max:1;n:type:ShaderForge.SFN_Step,id:5925,x:33226,y:32855,varname:node_5925,prsc:2|A-9551-OUT,B-6026-OUT;n:type:ShaderForge.SFN_Multiply,id:290,x:33415,y:32747,varname:node_290,prsc:2|A-168-RGB,B-5925-OUT;n:type:ShaderForge.SFN_Tex2d,id:168,x:33417,y:32218,ptovrint:False,ptlb:Main_Text,ptin:_Main_Text,varname:node_168,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9d0aabe93d504a24d8ba884aeb873fb2,ntxv:0,isnm:False|UVIN-8405-OUT;n:type:ShaderForge.SFN_Add,id:5163,x:33693,y:33004,varname:node_5163,prsc:2|A-290-OUT,B-7919-OUT;n:type:ShaderForge.SFN_Rotator,id:5441,x:32473,y:32367,varname:node_5441,prsc:2|UVIN-9111-UVOUT,ANG-7066-OUT;n:type:ShaderForge.SFN_TexCoord,id:9111,x:32177,y:32327,varname:node_9111,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:3337,x:31666,y:32593,ptovrint:False,ptlb:Rotate,ptin:_Rotate,varname:node_3337,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0,max:2;n:type:ShaderForge.SFN_Multiply,id:7066,x:32219,y:32481,varname:node_7066,prsc:2|A-2748-OUT,B-1659-OUT;n:type:ShaderForge.SFN_Pi,id:2748,x:31959,y:32436,varname:node_2748,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6590,x:32695,y:32393,varname:node_6590,prsc:2|A-5441-UVOUT,B-3692-OUT;n:type:ShaderForge.SFN_Append,id:3692,x:32659,y:32557,varname:node_3692,prsc:2|A-1781-OUT,B-1781-OUT;n:type:ShaderForge.SFN_Slider,id:7476,x:32127,y:32645,ptovrint:False,ptlb:Size,ptin:_Size,varname:node_7476,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.409416,max:3;n:type:ShaderForge.SFN_Vector2,id:8413,x:32838,y:32555,varname:node_8413,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Multiply,id:392,x:33022,y:32527,varname:node_392,prsc:2|A-1781-OUT,B-8413-OUT;n:type:ShaderForge.SFN_Subtract,id:5656,x:33022,y:32393,varname:node_5656,prsc:2|A-6590-OUT,B-392-OUT;n:type:ShaderForge.SFN_Add,id:8405,x:33245,y:32393,varname:node_8405,prsc:2|A-5656-OUT,B-8413-OUT;n:type:ShaderForge.SFN_Add,id:6652,x:33998,y:33061,varname:node_6652,prsc:2|A-6217-OUT,B-1691-OUT;n:type:ShaderForge.SFN_Multiply,id:1691,x:33777,y:33171,varname:node_1691,prsc:2|A-5792-OUT,B-5590-OUT;n:type:ShaderForge.SFN_Slider,id:3712,x:33712,y:33529,ptovrint:False,ptlb:Aphle,ptin:_Aphle,varname:node_3712,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5790803,max:1;n:type:ShaderForge.SFN_Multiply,id:6217,x:33762,y:32734,varname:node_6217,prsc:2|A-168-A,B-5925-OUT;n:type:ShaderForge.SFN_Multiply,id:3380,x:33288,y:33102,varname:node_3380,prsc:2|A-5792-OUT,B-7096-OUT;n:type:ShaderForge.SFN_Color,id:383,x:33132,y:33454,ptovrint:False,ptlb:LineColor,ptin:_LineColor,varname:node_383,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:9143,x:31666,y:32679,ptovrint:False,ptlb:Rotate_End,ptin:_Rotate_End,varname:node_9143,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0,max:2;n:type:ShaderForge.SFN_Lerp,id:1659,x:32042,y:32645,varname:node_1659,prsc:2|A-3337-OUT,B-9143-OUT,T-9234-OUT;n:type:ShaderForge.SFN_Slider,id:9234,x:31666,y:32770,ptovrint:False,ptlb:Anim,ptin:_Anim,varname:node_9234,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:1781,x:32474,y:32653,varname:node_1781,prsc:2|A-7476-OUT,B-4351-OUT,T-9234-OUT;n:type:ShaderForge.SFN_Slider,id:4351,x:32181,y:32811,ptovrint:False,ptlb:Size_End,ptin:_Size_End,varname:node_4351,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:3;n:type:ShaderForge.SFN_Lerp,id:6026,x:32988,y:32810,varname:node_6026,prsc:2|A-7842-OUT,B-4183-OUT,T-9234-OUT;n:type:ShaderForge.SFN_Slider,id:4183,x:32610,y:32813,ptovrint:False,ptlb:MainSphere_End,ptin:_MainSphere_End,varname:node_4183,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:5891,x:32461,y:33249,varname:node_5891,prsc:2|A-7746-OUT,B-7333-OUT,T-9234-OUT;n:type:ShaderForge.SFN_Lerp,id:1164,x:32423,y:33451,varname:node_1164,prsc:2|A-5798-OUT,B-2396-OUT,T-9234-OUT;n:type:ShaderForge.SFN_Slider,id:7333,x:32017,y:33316,ptovrint:False,ptlb:OuterLine_End,ptin:_OuterLine_End,varname:node_7333,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:2396,x:32057,y:33514,ptovrint:False,ptlb:LineWide_End,ptin:_LineWide_End,varname:node_2396,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:7096,x:33206,y:33265,varname:node_7096,prsc:2|A-383-RGB,B-4256-RGB,T-9234-OUT;n:type:ShaderForge.SFN_Lerp,id:5590,x:33881,y:33316,varname:node_5590,prsc:2|A-3712-OUT,B-3688-OUT,T-9234-OUT;n:type:ShaderForge.SFN_Color,id:4256,x:33132,y:33608,ptovrint:False,ptlb:LineColor_End,ptin:_LineColor_End,varname:node_4256,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.6965935,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:3688,x:33698,y:33646,ptovrint:False,ptlb:Aphle_End,ptin:_Aphle_End,varname:node_3688,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:7919,x:33511,y:33102,varname:node_7919,prsc:2|A-3380-OUT,B-9807-OUT;n:type:ShaderForge.SFN_Lerp,id:9807,x:33511,y:33306,varname:node_9807,prsc:2|A-3163-OUT,B-7144-RGB,T-2450-OUT;n:type:ShaderForge.SFN_Color,id:7144,x:33511,y:33471,ptovrint:False,ptlb:DownColor,ptin:_DownColor,varname:node_7144,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.06603771,c3:0.06603771,c4:1;n:type:ShaderForge.SFN_Vector3,id:3163,x:33328,y:33293,varname:node_3163,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Slider,id:2450,x:33302,y:33689,ptovrint:False,ptlb:AnimDown,ptin:_AnimDown,varname:node_2450,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:9234-168-3337-7476-7842-7746-5798-383-3712-9143-4351-4183-7333-2396-4256-3688-7144-2450;pass:END;sub:END;*/

Shader "UdonUI/ButtonAnim/Sphere_A" {
    Properties {
        _Anim ("Anim", Range(0, 1)) = 0
        _Main_Text ("Main_Text", 2D) = "white" {}
        _Rotate ("Rotate", Range(-2, 2)) = 0
        _Size ("Size", Range(0, 3)) = 1.409416
        _MainSphere ("MainSphere", Range(0, 1)) = 0.3305298
        _OuterLine ("OuterLine", Range(0, 1)) = 0.4153697
        _LineWide ("LineWide", Range(0, 1)) = 0.03478261
        [HDR]_LineColor ("LineColor", Color) = (1,1,1,1)
        _Aphle ("Aphle", Range(0, 1)) = 0.5790803
        _Rotate_End ("Rotate_End", Range(-2, 2)) = 0
        _Size_End ("Size_End", Range(0, 3)) = 0
        _MainSphere_End ("MainSphere_End", Range(0, 1)) = 0
        _OuterLine_End ("OuterLine_End", Range(0, 1)) = 0
        _LineWide_End ("LineWide_End", Range(0, 1)) = 0
        [HDR]_LineColor_End ("LineColor_End", Color) = (0.5,0.6965935,1,1)
        _Aphle_End ("Aphle_End", Range(0, 1)) = 1
        [HDR]_DownColor ("DownColor", Color) = (1,0.06603771,0.06603771,1)
        _AnimDown ("AnimDown", Range(0, 1)) = 0
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
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _Main_Text; uniform float4 _Main_Text_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _OuterLine)
                UNITY_DEFINE_INSTANCED_PROP( float, _LineWide)
                UNITY_DEFINE_INSTANCED_PROP( float, _MainSphere)
                UNITY_DEFINE_INSTANCED_PROP( float, _Rotate)
                UNITY_DEFINE_INSTANCED_PROP( float, _Size)
                UNITY_DEFINE_INSTANCED_PROP( float, _Aphle)
                UNITY_DEFINE_INSTANCED_PROP( float4, _LineColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _Rotate_End)
                UNITY_DEFINE_INSTANCED_PROP( float, _Anim)
                UNITY_DEFINE_INSTANCED_PROP( float, _Size_End)
                UNITY_DEFINE_INSTANCED_PROP( float, _MainSphere_End)
                UNITY_DEFINE_INSTANCED_PROP( float, _OuterLine_End)
                UNITY_DEFINE_INSTANCED_PROP( float, _LineWide_End)
                UNITY_DEFINE_INSTANCED_PROP( float4, _LineColor_End)
                UNITY_DEFINE_INSTANCED_PROP( float, _Aphle_End)
                UNITY_DEFINE_INSTANCED_PROP( float4, _DownColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _AnimDown)
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
                float _Rotate_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Rotate );
                float _Rotate_End_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Rotate_End );
                float _Anim_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Anim );
                float node_5441_ang = (3.141592654*lerp(_Rotate_var,_Rotate_End_var,_Anim_var));
                float node_5441_spd = 1.0;
                float node_5441_cos = cos(node_5441_spd*node_5441_ang);
                float node_5441_sin = sin(node_5441_spd*node_5441_ang);
                float2 node_5441_piv = float2(0.5,0.5);
                float2 node_5441 = (mul(i.uv0-node_5441_piv,float2x2( node_5441_cos, -node_5441_sin, node_5441_sin, node_5441_cos))+node_5441_piv);
                float _Size_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Size );
                float _Size_End_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Size_End );
                float node_1781 = lerp(_Size_var,_Size_End_var,_Anim_var);
                float2 node_8413 = float2(0.5,0.5);
                float2 node_8405 = (((node_5441*float2(node_1781,node_1781))-(node_1781*node_8413))+node_8413);
                float4 _Main_Text_var = tex2D(_Main_Text,TRANSFORM_TEX(node_8405, _Main_Text));
                float node_9551 = distance(i.uv0,float2(0.5,0.5));
                float _MainSphere_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MainSphere );
                float _MainSphere_End_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MainSphere_End );
                float node_5925 = step(node_9551,lerp(_MainSphere_var,_MainSphere_End_var,_Anim_var));
                float _OuterLine_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OuterLine );
                float _OuterLine_End_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OuterLine_End );
                float node_5891 = lerp(_OuterLine_var,_OuterLine_End_var,_Anim_var);
                float _LineWide_var = UNITY_ACCESS_INSTANCED_PROP( Props, _LineWide );
                float _LineWide_End_var = UNITY_ACCESS_INSTANCED_PROP( Props, _LineWide_End );
                float node_5792 = (step(node_9551,node_5891)-step(node_9551,(node_5891-lerp(_LineWide_var,_LineWide_End_var,_Anim_var))));
                float4 _LineColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _LineColor );
                float4 _LineColor_End_var = UNITY_ACCESS_INSTANCED_PROP( Props, _LineColor_End );
                float4 _DownColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _DownColor );
                float _AnimDown_var = UNITY_ACCESS_INSTANCED_PROP( Props, _AnimDown );
                float3 finalColor = ((_Main_Text_var.rgb*node_5925)+((node_5792*lerp(_LineColor_var.rgb,_LineColor_End_var.rgb,_Anim_var))+lerp(float3(0,0,0),_DownColor_var.rgb,_AnimDown_var)));
                float _Aphle_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Aphle );
                float _Aphle_End_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Aphle_End );
                fixed4 finalRGBA = fixed4(finalColor,((_Main_Text_var.a*node_5925)+(node_5792*lerp(_Aphle_var,_Aphle_End_var,_Anim_var))));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
