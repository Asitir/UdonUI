
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SetMap : UdonSharpBehaviour
{
    public bool isAttack = false;
    bool lateAttack = false;
    [Header("挖掘判断精度")]
    public int cuonst = 20;
    int[] depthGroup;
    float abs = 10;//比例大小
    int life = 1;
    int nowLife = 0;
    //public RenderTexture renderMap2;
    //public Texture2D renderMap;
    [Header("地面厚度")]
    public int beilu = 10;
    Material zid_mate;

    [Header("是否缓存弹孔")]
    public bool isDraw = false;
    bool draw = false;

    [Header("测试用对象")]
    public Transform player, A;
    Transform zid;
    Transform mp;

    MeshRenderer mesh;
    Material mate;
    Transform transpoint, transpoint2;
    void Start()
    {
        abs = (float)cuonst / abs;
        transpoint2 = transform.GetChild(0).GetChild(1);
        transpoint = transform.GetChild(1).GetChild(0);
        mesh = transform.GetChild(1).GetComponent<MeshRenderer>();
        mate = mesh.material;
        zid = transform.GetChild(0).GetChild(3);
        zid_mate = zid.GetComponent<MeshRenderer>().material;
        mp= transform.GetChild(0).GetChild(2);
        depthGroup = new int[cuonst * cuonst];
        //renderMap = new Texture2D(1, 1, TextureFormat.RGB24, false);
        //RenderTexture.active = renderMap2;
    }

    private void Update()
    {
        if (nowLife > 0)
        {
            nowLife--;
            if (nowLife <= 0)
            {
                zid.gameObject.SetActive(false);
            }
        }

        if (isDraw)
        {
            if (!draw)
            {
                mp.localPosition = Vector3.down * 3;
                draw = true;
            }
        }
        else {
            if (draw)
            {
                mp.localPosition = Vector3.zero;

                draw = false;
            }
        }
#if UNITY_EDITOR
        Vector3 pps = transpoint.InverseTransformPoint(player.position);

        if (lateAttack) {
            lateAttack = false;
            isAttack = false;
        }
        if (isAttack)
        {//一次性触发
            if (!lateAttack)
            {
                lateAttack = true;
            }
            Attack();
        }
#else
        Vector3 pps = Networking.LocalPlayer.GetPosition();
#endif
        //Vector3 pps2 = transpoint.InverseTransformPoint(A.position);
        //B.position = transpoint2.TransformPoint(pps2);
        mate.SetFloat("_X", pps.x / 10);
        mate.SetFloat("_Y", pps.z / 10);


        Attack();
    }

    void Attack() {
        Vector3 pps2 = transpoint.InverseTransformPoint(A.position);
        pps2.y = 0.1f;
        zid.position = transpoint2.TransformPoint(pps2);

        //zid.gameObject.SetActive(true);
        //nowLife = life;

    }

    int GetDepth(Vector3 pos)
    {
        Vector3 m_pos = transpoint.InverseTransformPoint(pos);
        int PY = (int)m_pos.z * cuonst;//纵向数量
        int PX = (int)(m_pos.x * abs);//横向单位数量
        if (PX > 0 && PX > 0) {
            int PZ = PX + PY;
            if (PZ < depthGroup.Length) return PZ;
        }
        return -1;
    }
    int SetDepth()
    {
        return 0;
    }
}
