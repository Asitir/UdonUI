// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|custl-2470-RGB,clip-5678-OUT;n:type:ShaderForge.SFN_Tex2d,id:2470,x:32550,y:32763,ptovrint:False,ptlb:MainText,ptin:_MainText,varname:node_2470,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:37f2601622a180946af899626dd1df67,ntxv:0,isnm:False|UVIN-120-OUT;n:type:ShaderForge.SFN_TexCoord,id:510,x:32660,y:33041,varname:node_510,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Distance,id:8219,x:32911,y:33148,varname:node_8219,prsc:2|A-510-UVOUT,B-3535-OUT;n:type:ShaderForge.SFN_Vector2,id:3535,x:32712,y:33271,varname:node_3535,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Slider,id:5290,x:32831,y:33380,ptovrint:False,ptlb:node_5290,ptin:_node_5290,varname:node_5290,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4304348,max:1;n:type:ShaderForge.SFN_Step,id:5678,x:33101,y:33207,varname:node_5678,prsc:2|A-8219-OUT,B-5290-OUT;n:type:ShaderForge.SFN_TexCoord,id:2448,x:31922,y:32627,varname:node_2448,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2dAsset,id:4562,x:31813,y:33135,ptovrint:False,ptlb:node_4562,ptin:_node_4562,varname:node_4562,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9394,x:32164,y:32767,varname:node_9394,prsc:2|A-2448-UVOUT,B-7493-OUT;n:type:ShaderForge.SFN_Slider,id:7493,x:31812,y:32876,ptovrint:False,ptlb:node_7493,ptin:_node_7493,varname:node_7493,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Add,id:120,x:32344,y:32821,varname:node_120,prsc:2|A-9394-OUT,B-7539-OUT;n:type:ShaderForge.SFN_Append,id:7539,x:32191,y:33043,varname:node_7539,prsc:2|A-873-OUT,B-107-OUT;n:type:ShaderForge.SFN_Slider,id:873,x:31920,y:33171,ptovrint:False,ptlb:node_873,ptin:_node_873,varname:node_873,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:107,x:31917,y:33254,ptovrint:False,ptlb:node_107,ptin:_node_107,varname:node_107,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:5290-2470-7493-873-107;pass:END;sub:END;*/

Shader "UdonUI/ButtonAnim/Textrue" {
    Properties {
        _node_5290 ("node_5290", Range(0, 1)) = 0.4304348
        _MainText ("MainText", 2D) = "white" {}
        _node_7493 ("node_7493", Range(0, 2)) = 1
        _node_873 ("node_873", Range(0, 1)) = 0
        _node_107 ("node_107", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _MainText; uniform float4 _MainText_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_5290)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_7493)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_873)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_107)
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
                float _node_5290_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_5290 );
                clip(step(distance(i.uv0,float2(0.5,0.5)),_node_5290_var) - 0.5);
////// Lighting:
                float _node_7493_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_7493 );
                float _node_873_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_873 );
                float _node_107_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_107 );
                float2 node_120 = ((i.uv0*_node_7493_var)+float2(_node_873_var,_node_107_var));
                float4 _MainText_var = tex2D(_MainText,TRANSFORM_TEX(node_120, _MainText));
                float3 finalColor = _MainText_var.rgb;
                fixed4 finalRGBA = fixed4(finalColor,1);
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
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_5290)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float _node_5290_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_5290 );
                clip(step(distance(i.uv0,float2(0.5,0.5)),_node_5290_var) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
