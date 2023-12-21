// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33330,y:32719,varname:node_9361,prsc:2|custl-9598-OUT,alpha-764-OUT;n:type:ShaderForge.SFN_DepthBlend,id:7666,x:31906,y:32599,varname:node_7666,prsc:2|DIST-7651-OUT;n:type:ShaderForge.SFN_OneMinus,id:8321,x:32449,y:32804,varname:node_8321,prsc:2|IN-3707-OUT;n:type:ShaderForge.SFN_Multiply,id:9598,x:32982,y:33008,varname:node_9598,prsc:2|A-8321-OUT,B-7261-RGB;n:type:ShaderForge.SFN_Color,id:7261,x:32973,y:33196,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_7261,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Power,id:3707,x:32151,y:32805,varname:node_3707,prsc:2|VAL-7666-OUT,EXP-3099-OUT;n:type:ShaderForge.SFN_Slider,id:3099,x:31700,y:32839,ptovrint:False,ptlb:Power,ptin:_Power,varname:node_3099,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:1,max:10;n:type:ShaderForge.SFN_Clamp01,id:3634,x:32756,y:32840,varname:node_3634,prsc:2|IN-8321-OUT;n:type:ShaderForge.SFN_Slider,id:7651,x:31544,y:32658,ptovrint:False,ptlb:Depth,ptin:_Depth,varname:node_7651,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:764,x:33033,y:32833,varname:node_764,prsc:2|A-3634-OUT,B-2755-OUT;n:type:ShaderForge.SFN_Slider,id:2755,x:32578,y:33073,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:node_2755,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;proporder:7261-3099-7651-2755;pass:END;sub:END;*/

Shader "UdonUI/Grid" {
    Properties {
        _MainColor ("MainColor", Color) = (0.5,0.5,0.5,1)
        _Power ("Power", Range(1, 10)) = 1
        _Depth ("Depth", Range(0, 1)) = 0
        _Alpha ("Alpha", Range(0, 1)) = 1
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
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _MainColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _Power)
                UNITY_DEFINE_INSTANCED_PROP( float, _Depth)
                UNITY_DEFINE_INSTANCED_PROP( float, _Alpha)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 projPos : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
                float _Depth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Depth );
                float _Power_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Power );
                float node_8321 = (1.0 - pow(saturate((sceneZ-partZ)/_Depth_var),_Power_var));
                float4 _MainColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MainColor );
                float3 finalColor = (node_8321*_MainColor_var.rgb);
                float _Alpha_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Alpha );
                return fixed4(finalColor,(saturate(node_8321)*_Alpha_var));
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
