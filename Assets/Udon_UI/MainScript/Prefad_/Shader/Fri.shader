// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33292,y:32704,varname:node_9361,prsc:2|custl-2645-OUT,alpha-8629-OUT;n:type:ShaderForge.SFN_Fresnel,id:4379,x:32325,y:32876,varname:node_4379,prsc:2|EXP-8604-OUT;n:type:ShaderForge.SFN_Power,id:2176,x:32571,y:32903,varname:node_2176,prsc:2|VAL-4379-OUT,EXP-6199-OUT;n:type:ShaderForge.SFN_Slider,id:6199,x:32154,y:33088,ptovrint:False,ptlb:FresnelExp,ptin:_FresnelExp,varname:_node_6199,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:1.860875,max:10;n:type:ShaderForge.SFN_Slider,id:8604,x:31862,y:32898,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_8604,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Color,id:6195,x:32714,y:32705,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_6195,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.7190268,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:2645,x:33031,y:32896,varname:node_2645,prsc:2|A-6195-RGB,B-2022-OUT;n:type:ShaderForge.SFN_DepthBlend,id:437,x:32585,y:33244,varname:node_437,prsc:2|DIST-576-OUT;n:type:ShaderForge.SFN_Slider,id:576,x:32234,y:33245,ptovrint:False,ptlb:Depth,ptin:_Depth,varname:node_576,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6072207,max:1;n:type:ShaderForge.SFN_OneMinus,id:4343,x:32781,y:33244,varname:node_4343,prsc:2|IN-437-OUT;n:type:ShaderForge.SFN_Add,id:2022,x:32809,y:32987,varname:node_2022,prsc:2|A-2176-OUT,B-4343-OUT;n:type:ShaderForge.SFN_Lerp,id:8629,x:33126,y:33210,varname:node_8629,prsc:2|A-2022-OUT,B-497-OUT,T-2359-OUT;n:type:ShaderForge.SFN_Vector1,id:497,x:32931,y:33244,varname:node_497,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:2359,x:32768,y:33434,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:node_2359,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:6199-8604-6195-576-2359;pass:END;sub:END;*/

Shader "Asi/Fri" {
    Properties {
        _FresnelExp ("FresnelExp", Range(1, 10)) = 1.860875
        _Fresnel ("Fresnel", Range(0, 1)) = 1
        _MainColor ("MainColor", Color) = (0,0.7190268,1,1)
        _Depth ("Depth", Range(0, 1)) = 0.6072207
        _Alpha ("Alpha", Range(0, 1)) = 0
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
            uniform sampler2D _CameraDepthTexture;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _FresnelExp)
                UNITY_DEFINE_INSTANCED_PROP( float, _Fresnel)
                UNITY_DEFINE_INSTANCED_PROP( float4, _MainColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _Depth)
                UNITY_DEFINE_INSTANCED_PROP( float, _Alpha)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 projPos : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
                float4 _MainColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _MainColor );
                float _Fresnel_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Fresnel );
                float _FresnelExp_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FresnelExp );
                float _Depth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Depth );
                float node_2022 = (pow(pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel_var),_FresnelExp_var)+(1.0 - saturate((sceneZ-partZ)/_Depth_var)));
                float3 finalColor = (_MainColor_var.rgb*node_2022);
                float _Alpha_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Alpha );
                fixed4 finalRGBA = fixed4(finalColor,lerp(node_2022,0.0,_Alpha_var));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
