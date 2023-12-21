// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33424,y:32722,varname:node_9361,prsc:2|custl-3889-OUT,alpha-7673-OUT;n:type:ShaderForge.SFN_TexCoord,id:2685,x:32018,y:32745,varname:node_2685,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector2,id:7002,x:32018,y:32921,varname:node_7002,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Distance,id:3787,x:32234,y:32826,varname:node_3787,prsc:2|A-2685-UVOUT,B-7002-OUT;n:type:ShaderForge.SFN_Slider,id:3889,x:32924,y:32784,ptovrint:False,ptlb:power,ptin:_power,varname:node_3889,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_OneMinus,id:3027,x:32412,y:32843,varname:node_3027,prsc:2|IN-3787-OUT;n:type:ShaderForge.SFN_RemapRange,id:3462,x:32567,y:33197,varname:node_3462,prsc:2,frmn:0,frmx:1,tomn:-2,tomx:1|IN-3027-OUT;n:type:ShaderForge.SFN_Clamp01,id:7673,x:32807,y:32861,varname:node_7673,prsc:2|IN-9159-OUT;n:type:ShaderForge.SFN_Power,id:9159,x:32625,y:32861,varname:node_9159,prsc:2|VAL-3027-OUT,EXP-9463-OUT;n:type:ShaderForge.SFN_Vector1,id:9463,x:32603,y:33023,varname:node_9463,prsc:2,v1:10;proporder:3889-2425-7543;pass:END;sub:END;*/

Shader "Shader Forge/zdan" {
    Properties {
        _power ("power", Range(0, 10)) = 1
        _node_2425 ("node_2425", Range(0, 1)) = 0
        _node_7543 ("node_7543", Range(0, 1)) = 0
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
                UNITY_DEFINE_INSTANCED_PROP( float, _power)
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
                float _power_var = UNITY_ACCESS_INSTANCED_PROP( Props, _power );
                float3 finalColor = float3(_power_var,_power_var,_power_var);
                float node_3787 = distance(i.uv0,float2(0.5,0.5));
                float node_3027 = (1.0 - node_3787);
                fixed4 finalRGBA = fixed4(finalColor,saturate(pow(node_3027,10.0)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
