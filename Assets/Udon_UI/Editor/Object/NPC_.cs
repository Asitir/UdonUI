using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Animations;
using VRC.Udon;


namespace UdonUI_Editor
{
    public class NPC_ : Editor
    {
        #region 静态变量
        static Transform[] HumanBones = new Transform[61];
        static HumanBone[] AvatarBones;
        static Transform[] CharacterBone;
        //static List<Transform> ReMoveObj = new List<Transform>();
        #endregion

        #region 赋予NPC骨骼
        //[MenuItem("Asitir_Tool/SetNpcBones", false, 0)]
        static void SetNPCBones()
        {
            int Objects = Selection.gameObjects.Length;
            if (Objects == 1)
            {
                if (Selection.activeGameObject.GetComponent<Animator>())
                {
                    if (Selection.activeGameObject.GetComponent<Animator>().avatar && Selection.activeGameObject.GetComponent<UdonBehaviour>())
                    {
                        SetCharacterValue(Selection.activeGameObject.GetComponent<Animator>().avatar, Selection.activeGameObject.transform);
                    }
                    else
                    {
                        if (EditorUtility.DisplayDialog("注意", "        对象不合法，该对象不存在Avatar或没有挂载Udon脚本", "我知道了", "返回")) { }
                    }
                }
                else
                {
                    if (EditorUtility.DisplayDialog("注意", "        对象不合法，请选中带动画机的角色对象", "我知道了", "返回")) { }
                }
            }
            else
            {
                if (Objects > 1)
                {
                    if (EditorUtility.DisplayDialog("注意", "        请选择单个对象", "我知道了", "返回")) { }
                }
                else
                {
                    if (EditorUtility.DisplayDialog("注意", "        请在场景中选中对象", "我知道了", "返回")) { }
                }
            }
        }
        #endregion

        #region 设置Twist骨骼
        //[MenuItem("GameObject/AvatarBonesPro/Set", false, 0)]
        static void SetTwist()
        {
            int Objects = Selection.gameObjects.Length;
            if (Objects == 1)
            {
                if (Selection.activeGameObject.GetComponent<Animator>())
                {
                    if (Selection.activeGameObject.GetComponent<Animator>().avatar)
                    {
                        ADDTwistCompenct(Selection.activeGameObject.GetComponent<Animator>().avatar, Selection.activeGameObject.transform);
                    }
                    else
                    {
                        if (EditorUtility.DisplayDialog("注意", "        对象不合法，该对象不存在Avatar,请确认当前对象为标准人形骨骼(Humanoid)", "我知道了", "返回")) { }
                    }
                }
                else
                {
                    if (EditorUtility.DisplayDialog("注意", "        对象不合法，该对象不存在Avatar,请确认当前对象为标准人形骨骼(Humanoid)", "我知道了", "返回")) { }
                }
            }
            else
            {
                if (Objects > 1)
                {
                    if (EditorUtility.DisplayDialog("注意", "        请选择单个对象", "我知道了", "返回")) { }
                }
                else
                {
                    if (EditorUtility.DisplayDialog("注意", "        请在场景中选中对象", "我知道了", "返回")) { }
                }
            }
        }
        #endregion

        #region 重置Twist骨骼
        //[MenuItem("GameObject/AvatarBonesPro/Reset", false, 2)]
        static void ReSetTwist()
        {
            int Objects = Selection.gameObjects.Length;
            if (Objects == 1)
            {
                if (Selection.activeGameObject.GetComponent<Animator>())
                {
                    if (Selection.activeGameObject.GetComponent<Animator>().avatar)
                    {
                        RestTwistCompent(Selection.activeGameObject.GetComponent<Animator>().avatar, Selection.activeGameObject.transform);
                    }
                    else
                    {
                        if (EditorUtility.DisplayDialog("注意", "        对象不合法，该对象不存在Avatar", "我知道了", "返回")) { }
                    }
                }
                else
                {
                    if (EditorUtility.DisplayDialog("注意", "        对象不合法，请选中带动画机的角色对象", "我知道了", "返回")) { }
                }
            }
            else
            {
                if (Objects > 1)
                {
                    if (EditorUtility.DisplayDialog("注意", "        请选择单个对象", "我知道了", "返回")) { }
                }
                else
                {
                    if (EditorUtility.DisplayDialog("注意", "        请在场景中选中对象", "我知道了", "返回")) { }
                }
            }

        }
        #endregion

        #region NpcBone
        static void SetCharacterValue(Avatar MyAvatar, Transform Target)
        {
            UdonBehaviour MainUIUdon = Target.GetComponent<UdonBehaviour>();
            AvatarBones = MyAvatar.humanDescription.human;
            CharacterBone = Target.GetComponentsInChildren<Transform>();
            //MainUIUdon.publicVariables.TryGetVariableValue("Mouse_switch", out Mouse_switch);
            //MainUIUdon.publicVariables.TrySetVariableValue("Button", FindHumanBone(HumanBodyBones.LeftHand));
            //MainUIUdon.publicVariables.TrySetVariableValue("Spine", CharacterBone[0]);
            //MainUIUdon.publicVariables.TrySetVariableValue("aa", 12);
            //Transform dda;
            //MainUIUdon.publicVariables.TryGetVariableValue("Hips", out dda);
            //Debug.Log("对象名称: " + dda);
            //Transform BoneObj = FindHumanBone(HumanBodyBones.LeftHand);

            HumanBones[0] = FindHumanBone(HumanBodyBones.Hips);
            HumanBones[1] = FindHumanBone(HumanBodyBones.Spine);
            HumanBones[2] = FindHumanBone(HumanBodyBones.Chest);
            HumanBones[3] = FindHumanBone(HumanBodyBones.Neck);
            HumanBones[4] = FindHumanBone(HumanBodyBones.Head);

            HumanBones[5] = FindHumanBone(HumanBodyBones.LeftUpperLeg);
            HumanBones[6] = FindHumanBone(HumanBodyBones.LeftLowerLeg);
            HumanBones[7] = FindHumanBone(HumanBodyBones.LeftFoot);
            HumanBones[8] = FindHumanBone(HumanBodyBones.RightUpperLeg);
            HumanBones[9] = FindHumanBone(HumanBodyBones.RightLowerLeg);
            HumanBones[10] = FindHumanBone(HumanBodyBones.RightFoot);

            HumanBones[11] = FindHumanBone(HumanBodyBones.LeftShoulder);
            HumanBones[12] = FindHumanBone(HumanBodyBones.LeftUpperArm);
            HumanBones[13] = FindHumanBone(HumanBodyBones.LeftLowerArm);
            HumanBones[14] = FindHumanBone(HumanBodyBones.LeftHand);
            HumanBones[15] = FindHumanBone(HumanBodyBones.RightShoulder);
            HumanBones[16] = FindHumanBone(HumanBodyBones.RightUpperArm);
            HumanBones[17] = FindHumanBone(HumanBodyBones.RightLowerArm);
            HumanBones[18] = FindHumanBone(HumanBodyBones.RightHand);

            HumanBones[19] = CreatHumanBone(HumanBodyBones.LeftUpperArm);
            HumanBones[20] = CreatHumanBone(HumanBodyBones.LeftLowerArm);
            HumanBones[21] = CreatHumanBone(HumanBodyBones.LeftHand);
            HumanBones[22] = CreatHumanBone(HumanBodyBones.RightUpperArm);
            HumanBones[23] = CreatHumanBone(HumanBodyBones.RightLowerArm);
            HumanBones[24] = CreatHumanBone(HumanBodyBones.RightHand);

            HumanBones[25] = CreatHumanBone(HumanBodyBones.LeftUpperLeg);
            HumanBones[26] = CreatHumanBone(HumanBodyBones.LeftLowerLeg);
            HumanBones[27] = CreatHumanBone(HumanBodyBones.LeftFoot);
            HumanBones[28] = CreatHumanBone(HumanBodyBones.RightUpperLeg);
            HumanBones[29] = CreatHumanBone(HumanBodyBones.RightLowerLeg);
            HumanBones[30] = CreatHumanBone(HumanBodyBones.RightFoot);

            HumanBones[31] = FindHumanBone(HumanBodyBones.LeftThumbProximal);
            HumanBones[32] = FindHumanBone(HumanBodyBones.LeftThumbIntermediate);
            HumanBones[33] = FindHumanBone(HumanBodyBones.LeftThumbDistal);
            HumanBones[34] = FindHumanBone(HumanBodyBones.LeftIndexProximal);
            HumanBones[35] = FindHumanBone(HumanBodyBones.LeftIndexIntermediate);
            HumanBones[36] = FindHumanBone(HumanBodyBones.LeftIndexDistal);
            HumanBones[37] = FindHumanBone(HumanBodyBones.LeftMiddleProximal);
            HumanBones[38] = FindHumanBone(HumanBodyBones.LeftMiddleIntermediate);
            HumanBones[39] = FindHumanBone(HumanBodyBones.LeftMiddleDistal);
            HumanBones[40] = FindHumanBone(HumanBodyBones.LeftRingProximal);
            HumanBones[41] = FindHumanBone(HumanBodyBones.LeftRingIntermediate);
            HumanBones[42] = FindHumanBone(HumanBodyBones.LeftRingDistal);
            HumanBones[43] = FindHumanBone(HumanBodyBones.LeftLittleProximal);
            HumanBones[44] = FindHumanBone(HumanBodyBones.LeftLittleIntermediate);
            HumanBones[45] = FindHumanBone(HumanBodyBones.LeftLittleDistal);

            HumanBones[46] = FindHumanBone(HumanBodyBones.RightThumbProximal);
            HumanBones[47] = FindHumanBone(HumanBodyBones.RightThumbIntermediate);
            HumanBones[48] = FindHumanBone(HumanBodyBones.RightThumbDistal);
            HumanBones[49] = FindHumanBone(HumanBodyBones.RightIndexProximal);
            HumanBones[50] = FindHumanBone(HumanBodyBones.RightIndexIntermediate);
            HumanBones[51] = FindHumanBone(HumanBodyBones.RightIndexDistal);
            HumanBones[52] = FindHumanBone(HumanBodyBones.RightMiddleProximal);
            HumanBones[53] = FindHumanBone(HumanBodyBones.RightMiddleIntermediate);
            HumanBones[54] = FindHumanBone(HumanBodyBones.RightMiddleDistal);
            HumanBones[55] = FindHumanBone(HumanBodyBones.RightRingProximal);
            HumanBones[56] = FindHumanBone(HumanBodyBones.RightRingIntermediate);
            HumanBones[57] = FindHumanBone(HumanBodyBones.RightRingDistal);
            HumanBones[58] = FindHumanBone(HumanBodyBones.RightLittleProximal);
            HumanBones[59] = FindHumanBone(HumanBodyBones.RightLittleIntermediate);
            HumanBones[60] = FindHumanBone(HumanBodyBones.RightLittleDistal);

            MainUIUdon.publicVariables.TrySetVariableValue("AllBone", HumanBones);
            Undo.RegisterCompleteObjectUndo(MainUIUdon, "骨骼赋予");
            //Selection.activeGameObject = FindHumanBone(HumanBodyBones.LeftHand).gameObject;

        }
        #endregion

        #region Avatar
        static void ADDTwistCompenct(Avatar MyAvatar, Transform Target)
        {
            //UdonBehaviour MainUIUdon = Target.GetComponent<UdonBehaviour>();
            AvatarBones = MyAvatar.humanDescription.human;
            CharacterBone = Target.GetComponentsInChildren<Transform>();

            for (int i = 0; i < CharacterBone.Length; i++)
            {
                string[] FindName = CharacterBone[i].name.Split('_');
                if (FindName.Length > 0)
                {
                    if (FindName[0] == "AvatarBonsPor")
                    {
                        if (EditorUtility.DisplayDialog("注意", "        检测到场景中已存在AvatarBonsPro,是否重置并清除AvatarBonsPro", "重置", "返回"))
                        {
                            ReSetTwist();
                            if (EditorUtility.DisplayDialog("注意", "        清除成功，如果需要重新设置AvatarBonsPor请手动重新添加AvatarBonsPor", "我知道了", "返回")) { }
                        }
                        return;
                    }
                }
            }

            //设置空对象
            GameObject A = new GameObject();

            Transform ModelHip = FindHumanBone(HumanBodyBones.Hips);

            Transform LeftShoulder = FindHumanBone(HumanBodyBones.LeftShoulder);
            Transform RightShoulder = FindHumanBone(HumanBodyBones.RightShoulder);

            Transform LeftArm = FindHumanBone(HumanBodyBones.LeftUpperArm);//上臂
            Transform LeftLowerArm = FindHumanBone(HumanBodyBones.LeftLowerArm);//小臂
            Transform LeftHand = FindHumanBone(HumanBodyBones.LeftHand);//手掌
            Transform LeftLeg = FindHumanBone(HumanBodyBones.LeftUpperLeg);//大腿
            Transform LeftLowerLeg = FindHumanBone(HumanBodyBones.LeftLowerLeg);//小腿
            Transform RightArm = FindHumanBone(HumanBodyBones.RightUpperArm);
            Transform RightLowerArm = FindHumanBone(HumanBodyBones.RightLowerArm);
            Transform RightHand = FindHumanBone(HumanBodyBones.RightHand);
            Transform RightLeg = FindHumanBone(HumanBodyBones.RightUpperLeg);
            Transform RightLowerLeg = FindHumanBone(HumanBodyBones.RightLowerLeg);

            bool ShoulderAdd = LeftShoulder.parent == RightShoulder.parent;//左右肩膀是否为同一个父级

            if ((!ShoulderAdd) && (LeftShoulder.parent == RightShoulder.parent))
            {//左右肩膀不是同一个父级  但是父级的父级是同一个
                LeftShoulder = LeftShoulder.parent;
                RightShoulder = RightShoulder.parent;
            }

            if (Vector3.Distance(LeftShoulder.position, LeftArm.position) * 10 < Vector3.Distance(LeftArm.position, LeftLowerArm.position))
            {
                if (!EditorUtility.DisplayDialog("注意!!", "        你模型的肩膀太短了，继续设置AvatarBonePor可能会出错，如果希望继续设定请点击‘继续’，否者就点取消", "继续", "取消"))
                {
                    DestroyImmediate(A);
                    return;
                }
            }

            string TwistName = "Twist";
            GameObject[] LeftTwistBones = SqueBones(LeftArm, TwistName, true);//搜索左手捩骨和小臂并排序
            GameObject[] RightTwistBones = SqueBones(RightArm, TwistName, true);//搜索右手捩骨和小臂并排序

            GameObject[] LeftTwistHand = SqueBones(LeftLowerArm, TwistName, true);
            GameObject[] RightTwistHand = SqueBones(RightLowerArm, TwistName, true);

            GameObject[] LeftTwistLeg = SqueBones(LeftLeg, TwistName, true);
            GameObject[] RightTwistLeg = SqueBones(RightLeg, TwistName, true);

            if (LeftTwistBones.Length > 0 && RightTwistBones.Length > 0)
            {
                #region 左大臂
                GameObject[] MainArmList = LeftTwistBones;//主要对象
                Quaternion Rota = Quaternion.LookRotation(LeftLowerArm.position - LeftArm.position, Vector3.up * 100);//辅助轴朝向

                GameObject FindLookRota = Instantiate(A, LeftShoulder.position, LeftShoulder.rotation, LeftShoulder);
                FindLookRota.name = "AvatarBonsPor_Left_Look";
                FindLookRota.AddComponent<LookAtConstraint>();

                GameObject ShulderUp1 = Instantiate(A, LeftShoulder.position, LeftShoulder.rotation, LeftShoulder);
                ShulderUp1.name = "AvatarBonsPor_Left_ShoulderUP";
                FindUpToMove(ShulderUp1.transform, 0.1f, Vector3.up);

                GameObject Look_Target = Instantiate(A, LeftArm.position, LeftArm.rotation, LeftArm);
                Look_Target.name = "AvatarBonsPor_Left_LookTarget";
                FindUpToMove(Look_Target.transform, Vector3.Distance(LeftArm.position, LeftShoulder.position), (LeftLowerArm.position - LeftArm.position));

                GameObject Look_Up_ = Instantiate(A, LeftArm.position, LeftArm.rotation, LeftArm);
                Look_Up_.name = "AvatarBonsPor_Left_Up_";
                Look_Up_.AddComponent<RotationConstraint>();

                GameObject Look_Up = Instantiate(A, LeftArm.position, LeftArm.rotation, Look_Up_.transform);
                Look_Up.name = "AvatarBonsPor_Left_Up";
                Look_Up.transform.localPosition = Vector3.up;

                GameObject MainLook = Instantiate(A, LeftArm.position, LeftArm.rotation, LeftArm);
                MainLook.name = "AvatarBonsPor_Left_MainLook";
                MainLook.AddComponent<LookAtConstraint>();

                GameObject MainRoot = Instantiate(A, LeftLowerArm.position, Rota, LeftArm);
                MainRoot.name = "AvatarBonsPor_Left_MainRoot";

                #region 设置组件变量
                LookAtConstraint SetLook = FindLookRota.GetComponent<LookAtConstraint>();
                RotationConstraint SetRota = Look_Up_.GetComponent<RotationConstraint>();
                LookAtConstraint MainLookRoc = MainLook.GetComponent<LookAtConstraint>();
                SetLook.worldUpObject = ShulderUp1.transform;
                SetLook.useUpObject = true;
                MainLookRoc.worldUpObject = Look_Up.transform;
                MainLookRoc.useUpObject = true;
                ConstraintSource a = new ConstraintSource();
                a.weight = 1;
                a.sourceTransform = Look_Target.transform;
                SetLook.AddSource(a);

                ConstraintSource b = new ConstraintSource();
                b.weight = 1;
                b.sourceTransform = FindLookRota.transform;
                SetRota.AddSource(b);
                SetRota.rotationAtRest = Vector3.zero;
                SetRota.rotationOffset = Vector3.zero;
                SetRota.locked = true;

                ConstraintSource c = new ConstraintSource();
                c.weight = 1;
                c.sourceTransform = LeftLowerArm.transform;
                MainLookRoc.AddSource(c);

                float RotaOffsetWight = (float)1 / (MainArmList.Length);
                //Debug.Log(RotaOffsetWight);
                for (int i = 0; i < MainArmList.Length; i++)
                {
                    RotationConstraint MainConstraint = MainArmList[i].AddComponent<RotationConstraint>();
                    if (i == 0)
                    {
                        ConstraintSource LitA = new ConstraintSource();
                        LitA.sourceTransform = MainLook.transform;
                        LitA.weight = 1;
                        MainConstraint.AddSource(LitA);
                        MainConstraint.rotationAtRest = Vector3.zero;
                        MainConstraint.locked = true;
                    }
                    else
                    {
                        ConstraintSource LitA = new ConstraintSource();
                        ConstraintSource LitB = new ConstraintSource();
                        LitA.sourceTransform = MainLook.transform;
                        LitB.sourceTransform = MainRoot.transform;
                        LitB.weight = i * RotaOffsetWight;
                        LitA.weight = 1 - LitB.weight;
                        MainConstraint.AddSource(LitA);
                        MainConstraint.AddSource(LitB);
                        MainConstraint.rotationAtRest = Vector3.zero;
                        //MainConstraint.rotationOffset = Vector3.zero;
                        MainConstraint.locked = true;

                    }
                    MainConstraint.constraintActive = true;
                }

                SetLook.constraintActive = true;
                SetRota.constraintActive = true;
                MainLookRoc.constraintActive = true;
                #endregion
                #endregion
                #region 右大臂
                MainArmList = RightTwistBones;//主要对象
                Rota = Quaternion.LookRotation(RightLowerArm.position - RightArm.position, Vector3.up * 100);//辅助轴朝向

                FindLookRota = Instantiate(A, RightShoulder.position, RightShoulder.rotation, RightShoulder);
                FindLookRota.name = "AvatarBonsPor_Right_Look";
                FindLookRota.AddComponent<LookAtConstraint>();

                ShulderUp1 = Instantiate(A, RightShoulder.position, RightShoulder.rotation, RightShoulder);
                ShulderUp1.name = "AvatarBonsPor_Right_ShoulderUP";
                FindUpToMove(ShulderUp1.transform, 0.1f, Vector3.up);

                Look_Target = Instantiate(A, RightArm.position, RightArm.rotation, RightArm);
                Look_Target.name = "AvatarBonsPor_Right_LookTarget";
                FindUpToMove(Look_Target.transform, Vector3.Distance(RightArm.position, RightShoulder.position), (RightLowerArm.position - RightArm.position));

                Look_Up_ = Instantiate(A, RightArm.position, RightArm.rotation, RightArm);
                Look_Up_.name = "AvatarBonsPor_Right_Up_";
                Look_Up_.AddComponent<RotationConstraint>();

                Look_Up = Instantiate(A, RightArm.position, RightArm.rotation, Look_Up_.transform);
                Look_Up.name = "AvatarBonsPor_Right_Up";
                Look_Up.transform.localPosition = Vector3.up;

                MainLook = Instantiate(A, RightArm.position, RightArm.rotation, RightArm);
                MainLook.name = "AvatarBonsPor_Right_MainLook";
                MainLook.AddComponent<LookAtConstraint>();

                MainRoot = Instantiate(A, RightLowerArm.position, Rota, RightArm);
                MainRoot.name = "AvatarBonsPor_Right_MainRoot";

                #region 设置组件变量
                SetLook = FindLookRota.GetComponent<LookAtConstraint>();
                SetRota = Look_Up_.GetComponent<RotationConstraint>();
                MainLookRoc = MainLook.GetComponent<LookAtConstraint>();
                SetLook.worldUpObject = ShulderUp1.transform;
                SetLook.useUpObject = true;
                MainLookRoc.worldUpObject = Look_Up.transform;
                MainLookRoc.useUpObject = true;
                a = new ConstraintSource();
                a.weight = 1;
                a.sourceTransform = Look_Target.transform;
                SetLook.AddSource(a);

                b = new ConstraintSource();
                b.weight = 1;
                b.sourceTransform = FindLookRota.transform;
                SetRota.AddSource(b);
                SetRota.rotationAtRest = Vector3.zero;
                SetRota.rotationOffset = Vector3.zero;
                SetRota.locked = true;

                c = new ConstraintSource();
                c.weight = 1;
                c.sourceTransform = RightLowerArm.transform;
                MainLookRoc.AddSource(c);

                RotaOffsetWight = (float)1 / (MainArmList.Length);
                //Debug.Log(RotaOffsetWight);
                for (int i = 0; i < MainArmList.Length; i++)
                {
                    RotationConstraint MainConstraint = MainArmList[i].AddComponent<RotationConstraint>();
                    if (i == 0)
                    {
                        ConstraintSource LitA = new ConstraintSource();
                        LitA.sourceTransform = MainLook.transform;
                        LitA.weight = 1;
                        MainConstraint.AddSource(LitA);
                        MainConstraint.rotationAtRest = Vector3.zero;
                        MainConstraint.locked = true;
                    }
                    else
                    {
                        ConstraintSource LitA = new ConstraintSource();
                        ConstraintSource LitB = new ConstraintSource();
                        LitA.sourceTransform = MainLook.transform;
                        LitB.sourceTransform = MainRoot.transform;
                        LitB.weight = i * RotaOffsetWight;
                        LitA.weight = 1 - LitB.weight;
                        MainConstraint.AddSource(LitA);
                        MainConstraint.AddSource(LitB);
                        MainConstraint.rotationAtRest = Vector3.zero;
                        //MainConstraint.rotationOffset = Vector3.zero;
                        MainConstraint.locked = true;

                    }
                    MainConstraint.constraintActive = true;
                }

                SetLook.constraintActive = true;
                SetRota.constraintActive = true;
                MainLookRoc.constraintActive = true;
                #endregion
                #endregion
            }

            if (LeftTwistHand.Length > 0 && RightTwistHand.Length > 0)
            {
                #region 左手腕
                //Transform hand1 = LeftArm;
                Transform hand2 = LeftLowerArm;
                Transform hand3 = LeftHand;
                GameObject[] MainArmList = LeftTwistHand;

                Quaternion TwistBoneRota = Quaternion.LookRotation(hand3.position - hand2.position, Vector3.up * 100);
                GameObject HandTwistUP = Instantiate(A, hand2.position, TwistBoneRota, hand2);
                HandTwistUP.name = "AvatarBonsPor_Left_HnadTwistUP";
                HandTwistUP.AddComponent<RotationConstraint>();

                GameObject HandTwistUP_ = Instantiate(A, hand2.position, TwistBoneRota, HandTwistUP.transform);
                HandTwistUP_.name = "AvatarBonsPor_Left_HnadTwistUP_";
                HandTwistUP_.transform.localPosition = Vector3.up;

                GameObject HandTwistFindUp = Instantiate(A, hand3.position, TwistBoneRota, hand3);
                HandTwistFindUp.name = "AvatarBonsPor_Left_FindUP";
                HandTwistFindUp.transform.Translate(0, 0, 1);
                HandTwistFindUp.AddComponent<LookAtConstraint>();

                GameObject HandTwistFindUp_ = Instantiate(A, HandTwistFindUp.transform.position, TwistBoneRota, hand3);
                HandTwistFindUp_.name = "AvatarBonsPor_Left_FindUP_";
                HandTwistFindUp_.transform.Translate(0, 1, 0);

                GameObject HandMainTwist = Instantiate(A, hand2.position, TwistBoneRota, hand2);
                HandMainTwist.name = "AvatarBonsPor_Left_MainTwist";
                HandMainTwist.AddComponent<LookAtConstraint>();

                GameObject HandMainTwistRoot = Instantiate(A, hand3.position, TwistBoneRota, hand2);
                HandMainTwistRoot.name = "AvatarBonsPor_Left_MainRoot";

                #region 控制器
                LookAtConstraint SetLook = HandTwistFindUp.GetComponent<LookAtConstraint>();
                RotationConstraint SetRota = HandTwistUP.GetComponent<RotationConstraint>();
                LookAtConstraint MainLookRoc = HandMainTwist.GetComponent<LookAtConstraint>();
                SetLook.worldUpObject = HandTwistFindUp_.transform;
                SetLook.useUpObject = true;
                MainLookRoc.worldUpObject = HandTwistUP_.transform;
                MainLookRoc.useUpObject = true;

                ConstraintSource a = new ConstraintSource();
                a.sourceTransform = hand2;
                a.weight = 1;
                SetLook.AddSource(a);

                ConstraintSource b = new ConstraintSource();
                b.sourceTransform = HandTwistFindUp.transform;
                b.weight = 1;
                SetRota.AddSource(b);
                SetRota.locked = true;

                ConstraintSource c = new ConstraintSource();
                c.sourceTransform = hand3;
                c.weight = 1;
                MainLookRoc.AddSource(c);

                float RotaOffsetWight = (float)1 / (MainArmList.Length);
                for (int i = 0; i < MainArmList.Length; i++)
                {
                    RotationConstraint MainConstraint = MainArmList[i].AddComponent<RotationConstraint>();
                    if (i == MainArmList.Length - 1)
                    {
                        ConstraintSource LitA = new ConstraintSource();
                        LitA.sourceTransform = HandMainTwist.transform;
                        LitA.weight = 1;
                        MainConstraint.AddSource(LitA);
                    }
                    else
                    {
                        ConstraintSource LitA = new ConstraintSource();
                        ConstraintSource LitB = new ConstraintSource();
                        LitA.sourceTransform = HandMainTwist.transform;
                        LitB.sourceTransform = HandMainTwistRoot.transform;
                        float ddc = (i * RotaOffsetWight) + RotaOffsetWight;
                        LitA.weight = ddc;
                        LitB.weight = 1 - ddc;
                        MainConstraint.AddSource(LitA);
                        MainConstraint.AddSource(LitB);
                    }
                    MainConstraint.rotationAtRest = Vector3.zero;
                    MainConstraint.locked = true;
                    MainConstraint.constraintActive = true;
                }

                SetLook.constraintActive = true;
                SetRota.constraintActive = true;
                MainLookRoc.constraintActive = true;
                #endregion
                #endregion
                #region 右手腕
                //Transform hand1 = LeftArm;
                hand2 = RightLowerArm;
                hand3 = RightHand;
                MainArmList = RightTwistHand;

                TwistBoneRota = Quaternion.LookRotation(hand3.position - hand2.position, Vector3.up * 100);
                HandTwistUP = Instantiate(A, hand2.position, TwistBoneRota, hand2);
                HandTwistUP.name = "AvatarBonsPor_Right_HnadTwistUP";
                HandTwistUP.AddComponent<RotationConstraint>();

                HandTwistUP_ = Instantiate(A, hand2.position, TwistBoneRota, HandTwistUP.transform);
                HandTwistUP_.name = "AvatarBonsPor_Right_HnadTwistUP_";
                HandTwistUP_.transform.localPosition = Vector3.up;

                HandTwistFindUp = Instantiate(A, hand3.position, TwistBoneRota, hand3);
                HandTwistFindUp.name = "AvatarBonsPor_Right_FindUP";
                HandTwistFindUp.transform.Translate(0, 0, 1);
                HandTwistFindUp.AddComponent<LookAtConstraint>();

                HandTwistFindUp_ = Instantiate(A, HandTwistFindUp.transform.position, TwistBoneRota, hand3);
                HandTwistFindUp_.name = "AvatarBonsPor_Right_FindUP_";
                HandTwistFindUp_.transform.Translate(0, 1, 0);

                HandMainTwist = Instantiate(A, hand2.position, TwistBoneRota, hand2);
                HandMainTwist.name = "AvatarBonsPor_Right_MainTwist";
                HandMainTwist.AddComponent<LookAtConstraint>();

                HandMainTwistRoot = Instantiate(A, hand3.position, TwistBoneRota, hand2);
                HandMainTwistRoot.name = "AvatarBonsPor_Right_MainRoot";

                #region 控制器
                SetLook = HandTwistFindUp.GetComponent<LookAtConstraint>();
                SetRota = HandTwistUP.GetComponent<RotationConstraint>();
                MainLookRoc = HandMainTwist.GetComponent<LookAtConstraint>();
                SetLook.worldUpObject = HandTwistFindUp_.transform;
                SetLook.useUpObject = true;
                MainLookRoc.worldUpObject = HandTwistUP_.transform;
                MainLookRoc.useUpObject = true;

                a = new ConstraintSource();
                a.sourceTransform = hand2;
                a.weight = 1;
                SetLook.AddSource(a);

                b = new ConstraintSource();
                b.sourceTransform = HandTwistFindUp.transform;
                b.weight = 1;
                SetRota.AddSource(b);
                SetRota.locked = true;

                c = new ConstraintSource();
                c.sourceTransform = hand3;
                c.weight = 1;
                MainLookRoc.AddSource(c);

                RotaOffsetWight = (float)1 / (MainArmList.Length);
                for (int i = 0; i < MainArmList.Length; i++)
                {
                    RotationConstraint MainConstraint = MainArmList[i].AddComponent<RotationConstraint>();
                    if (i == MainArmList.Length - 1)
                    {
                        ConstraintSource LitA = new ConstraintSource();
                        LitA.sourceTransform = HandMainTwist.transform;
                        LitA.weight = 1;
                        MainConstraint.AddSource(LitA);
                    }
                    else
                    {
                        ConstraintSource LitA = new ConstraintSource();
                        ConstraintSource LitB = new ConstraintSource();
                        LitA.sourceTransform = HandMainTwist.transform;
                        LitB.sourceTransform = HandMainTwistRoot.transform;
                        float ddc = (i * RotaOffsetWight) + RotaOffsetWight;
                        LitA.weight = ddc;
                        LitB.weight = 1 - ddc;
                        MainConstraint.AddSource(LitA);
                        MainConstraint.AddSource(LitB);
                    }
                    MainConstraint.rotationAtRest = Vector3.zero;
                    MainConstraint.locked = true;
                    MainConstraint.constraintActive = true;
                }

                SetLook.constraintActive = true;
                SetRota.constraintActive = true;
                MainLookRoc.constraintActive = true;
                #endregion
                #endregion
            }

            if (LeftTwistLeg.Length > 0 && RightTwistLeg.Length > 0)
            {
                #region 左大腿
                Transform LegUper = LeftLeg;
                Transform LegLower = LeftLowerLeg;
                GameObject[] MainArmList = LeftTwistLeg;


                Vector3 ModelDir = MainArmList[0].transform.TransformDirection(0, 1, 0);
                ModelDir.y = 0;

                Quaternion Forward = Quaternion.LookRotation(LegLower.position - LegUper.position, ModelDir);
                GameObject FzLook = Instantiate(A, LegUper.position, Forward, ModelHip);
                FzLook.name = "AvatarBonsPor_Left_FzLook";
                FzLook.transform.Translate(0, 0, -0.1f);
                FzLook.AddComponent<LookAtConstraint>();

                GameObject FzLook_Up = Instantiate(A, FzLook.transform.position, FzLook.transform.rotation, ModelHip);
                FzLook_Up.name = "AvatarBonsPor_Left_FzLookUp";
                FzLook_Up.transform.Translate(0, 0.2f, 0);

                GameObject FzLook_Target = Instantiate(A, LegUper.position, Forward, LegUper);
                FzLook_Target.name = "AvatarBonsPor_Left_FzLookTarget";
                FzLook_Target.transform.Translate(0, 0, 0.1f);

                GameObject Look_Up = Instantiate(A, LegUper.position, Forward, LegUper);
                Look_Up.name = "AvatarBonsPor_Left_FzLookUp";
                Look_Up.AddComponent<RotationConstraint>();

                GameObject Look_Up_ = Instantiate(A, Look_Up.transform.position, Forward, Look_Up.transform);
                Look_Up_.name = "AvatarBonsPor_Left_FzLookUp_";
                Look_Up_.transform.Translate(0, 0.2f, 0);

                GameObject MainLook = Instantiate(A, LegUper.position, Forward, LegUper);
                MainLook.name = "AvatarBonsPor_Left_MainLook";
                MainLook.AddComponent<LookAtConstraint>();

                GameObject MainLookRoot = Instantiate(A, LegLower.position, Forward, LegUper);
                MainLookRoot.name = "AvatarBonsPor_Left_MainLookRoot";

                #region 控制器
                LookAtConstraint SetLook = FzLook.GetComponent<LookAtConstraint>();
                RotationConstraint SetRota = Look_Up.GetComponent<RotationConstraint>();
                LookAtConstraint MainLookRoc = MainLook.GetComponent<LookAtConstraint>();
                SetLook.worldUpObject = FzLook_Up.transform;
                SetLook.useUpObject = true;
                MainLookRoc.worldUpObject = Look_Up_.transform;
                MainLookRoc.useUpObject = true;
                ConstraintSource a = new ConstraintSource();
                a.weight = 1;
                a.sourceTransform = FzLook_Target.transform;
                SetLook.AddSource(a);

                ConstraintSource b = new ConstraintSource();
                b.weight = 1;
                b.sourceTransform = FzLook.transform;
                SetRota.AddSource(b);
                SetRota.rotationAtRest = Vector3.zero;
                SetRota.rotationOffset = Vector3.zero;
                SetRota.locked = true;

                ConstraintSource c = new ConstraintSource();
                c.weight = 1;
                c.sourceTransform = LegLower;
                MainLookRoc.AddSource(c);

                float RotaOffsetWight = (float)1 / (MainArmList.Length);
                //Debug.Log(RotaOffsetWight);
                for (int i = 0; i < MainArmList.Length; i++)
                {
                    RotationConstraint MainConstraint = MainArmList[i].AddComponent<RotationConstraint>();
                    if (i == 0)
                    {
                        ConstraintSource LitA = new ConstraintSource();
                        LitA.sourceTransform = MainLook.transform;
                        LitA.weight = 1;
                        MainConstraint.AddSource(LitA);
                        MainConstraint.rotationAtRest = Vector3.zero;
                        MainConstraint.locked = true;
                    }
                    else
                    {
                        ConstraintSource LitA = new ConstraintSource();
                        ConstraintSource LitB = new ConstraintSource();
                        LitA.sourceTransform = MainLook.transform;
                        LitB.sourceTransform = MainLookRoot.transform;
                        LitB.weight = i * RotaOffsetWight;
                        LitA.weight = 1 - LitB.weight;
                        MainConstraint.AddSource(LitA);
                        MainConstraint.AddSource(LitB);
                        MainConstraint.rotationAtRest = Vector3.zero;
                        //MainConstraint.rotationOffset = Vector3.zero;
                        MainConstraint.locked = true;

                    }
                    MainConstraint.constraintActive = true;
                }

                SetLook.constraintActive = true;
                SetRota.constraintActive = true;
                MainLookRoc.constraintActive = true;
                #endregion
                #endregion
                #region 右大腿
                LegUper = RightLeg;
                LegLower = RightLowerLeg;
                MainArmList = RightTwistLeg;

                ModelDir = MainArmList[0].transform.TransformDirection(0, 1, 0);
                ModelDir.y = 0;

                Forward = Quaternion.LookRotation(LegLower.position - LegUper.position, ModelDir);
                FzLook = Instantiate(A, LegUper.position, Forward, ModelHip);
                FzLook.name = "AvatarBonsPor_Right_FzLook";
                FzLook.transform.Translate(0, 0, -0.1f);
                FzLook.AddComponent<LookAtConstraint>();

                FzLook_Up = Instantiate(A, FzLook.transform.position, FzLook.transform.rotation, ModelHip);
                FzLook_Up.name = "AvatarBonsPor_Right_FzLookUp";
                FzLook_Up.transform.Translate(0, 0.2f, 0);

                FzLook_Target = Instantiate(A, LegUper.position, Forward, LegUper);
                FzLook_Target.name = "AvatarBonsPor_Right_FzLookTarget";
                FzLook_Target.transform.Translate(0, 0, 0.1f);

                Look_Up = Instantiate(A, LegUper.position, Forward, LegUper);
                Look_Up.name = "AvatarBonsPor_Right_FzLookUp";
                Look_Up.AddComponent<RotationConstraint>();

                Look_Up_ = Instantiate(A, Look_Up.transform.position, Forward, Look_Up.transform);
                Look_Up_.name = "AvatarBonsPor_Right_FzLookUp_";
                Look_Up_.transform.Translate(0, 0.2f, 0);

                MainLook = Instantiate(A, LegUper.position, Forward, LegUper);
                MainLook.name = "AvatarBonsPor_Right_MainLook";
                MainLook.AddComponent<LookAtConstraint>();

                MainLookRoot = Instantiate(A, LegLower.position, Forward, LegUper);
                MainLookRoot.name = "AvatarBonsPor_Right_MainLookRoot";

                #region 控制器
                SetLook = FzLook.GetComponent<LookAtConstraint>();
                SetRota = Look_Up.GetComponent<RotationConstraint>();
                MainLookRoc = MainLook.GetComponent<LookAtConstraint>();
                SetLook.worldUpObject = FzLook_Up.transform;
                SetLook.useUpObject = true;
                MainLookRoc.worldUpObject = Look_Up_.transform;
                MainLookRoc.useUpObject = true;
                a = new ConstraintSource();
                a.weight = 1;
                a.sourceTransform = FzLook_Target.transform;
                SetLook.AddSource(a);

                b = new ConstraintSource();
                b.weight = 1;
                b.sourceTransform = FzLook.transform;
                SetRota.AddSource(b);
                SetRota.rotationAtRest = Vector3.zero;
                SetRota.rotationOffset = Vector3.zero;
                SetRota.locked = true;

                c = new ConstraintSource();
                c.weight = 1;
                c.sourceTransform = LegLower;
                MainLookRoc.AddSource(c);

                RotaOffsetWight = (float)1 / (MainArmList.Length);
                //Debug.Log(RotaOffsetWight);
                for (int i = 0; i < MainArmList.Length; i++)
                {
                    RotationConstraint MainConstraint = MainArmList[i].AddComponent<RotationConstraint>();
                    if (i == 0)
                    {
                        ConstraintSource LitA = new ConstraintSource();
                        LitA.sourceTransform = MainLook.transform;
                        LitA.weight = 1;
                        MainConstraint.AddSource(LitA);
                        MainConstraint.rotationAtRest = Vector3.zero;
                        MainConstraint.locked = true;
                    }
                    else
                    {
                        ConstraintSource LitA = new ConstraintSource();
                        ConstraintSource LitB = new ConstraintSource();
                        LitA.sourceTransform = MainLook.transform;
                        LitB.sourceTransform = MainLookRoot.transform;
                        LitB.weight = i * RotaOffsetWight;
                        LitA.weight = 1 - LitB.weight;
                        MainConstraint.AddSource(LitA);
                        MainConstraint.AddSource(LitB);
                        MainConstraint.rotationAtRest = Vector3.zero;
                        //MainConstraint.rotationOffset = Vector3.zero;
                        MainConstraint.locked = true;

                    }
                    MainConstraint.constraintActive = true;
                }

                SetLook.constraintActive = true;
                SetRota.constraintActive = true;
                MainLookRoc.constraintActive = true;
                #endregion
                #endregion
            }

            DestroyImmediate(A);
        }
        static void RestTwistCompent(Avatar MyAvatar, Transform Target)
        {
            AvatarBones = MyAvatar.humanDescription.human;
            CharacterBone = Target.GetComponentsInChildren<Transform>();

            RestTwist(FindHumanBone(HumanBodyBones.LeftUpperArm));
            RestTwist(FindHumanBone(HumanBodyBones.RightUpperArm));
            RestTwist(FindHumanBone(HumanBodyBones.LeftLowerArm));
            RestTwist(FindHumanBone(HumanBodyBones.RightLowerArm));
            RestTwist(FindHumanBone(HumanBodyBones.LeftUpperLeg));
            RestTwist(FindHumanBone(HumanBodyBones.RightUpperLeg));

            GameObject[] ReMove = new GameObject[CharacterBone.Length];
            int ReMoveNumber = 0;
            for (int i = 0; i < CharacterBone.Length; i++)
            {
                string[] FindName = CharacterBone[i].name.Split('_');
                if (FindName.Length > 0)
                {
                    if (FindName[0] == "AvatarBonsPor")
                    {
                        ReMove[ReMoveNumber] = CharacterBone[i].gameObject;
                        ReMoveNumber++;
                    }
                }
            }

            for (int i = 0; i < ReMoveNumber; i++)
                DestroyImmediate(ReMove[i].gameObject);
        }
        #endregion

        #region 自定义方法
        /// <summary>
        /// 搜索到相应的角色骨骼并返回Transform
        /// </summary>
        /// <param name="FindBone"></param>
        /// <returns></returns>
        static Transform FindHumanBone(HumanBodyBones FindBone)
        {
            Transform A = CharacterBone[0];
            string FindName = FindBone.ToString();

            for (int i = 0; i < AvatarBones.Length; i++)
            {
                if (AvatarBones[i].humanName == FindName)
                {
                    for (int t = 0; t < CharacterBone.Length; t++)
                    {
                        if (CharacterBone[t].name == AvatarBones[i].boneName)
                        {
                            A = CharacterBone[t];
                            //Debug.Log("检索完成：" + CharacterBone[t].name);
                            return A;
                        }
                    }
                }
            }
            return A;
        }
        static Transform CreatHumanBone(HumanBodyBones FindBone)
        {
            Transform A = CharacterBone[0];
            string FindName = FindBone.ToString();

            for (int i = 0; i < AvatarBones.Length; i++)
            {
                if (AvatarBones[i].humanName == FindName)
                {
                    for (int t = 0; t < CharacterBone.Length; t++)
                    {
                        if (CharacterBone[t].name == AvatarBones[i].boneName)
                        {
                            A = CharacterBone[t];
                            //Debug.Log("检索完成：" + CharacterBone[t].name);
                            for (int it = 0; it < A.childCount; it++)
                                if (A.GetChild(it).name == "Anim_Bone") return A.GetChild(it);

                            GameObject B = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath(UdonUI_Manager.Instance.Path_Prefad + "GameObject_Null.prefab", typeof(GameObject)), A.position, A.rotation, A);
                            B.name = "Anim_Bone";
                            return B.transform;
                        }
                    }
                }
            }
            return A;
        }

        /// <summary>
        /// 找到相近的世界轴向位移
        /// </summary>
        /// <param name="target">要移动的对象</param>
        /// <param name="dis">移动距离</param>
        /// <param name="FindAxis">要移动的世界轴向</param>
        static void FindUpToMove(Transform target, float dis, Vector3 FindAxis)
        {
            float Dot = 100000;
            //int Axis = 0;

            Vector3 MoveDis = Vector3.zero;
            //Vector3 FindAxis_2 = Vector3.Normalize(FindAxis);

            if (Vector3.Dot(target.up, FindAxis) < Dot)
            {
                Dot = Vector3.Dot(target.up, FindAxis);
                MoveDis = new Vector3(0, 1, 0);
            }
            if (Vector3.Dot(target.up * -1, FindAxis) < Dot)
            {
                Dot = Vector3.Dot(target.up * -1, FindAxis);
                MoveDis = new Vector3(0, -1, 0);
            }
            if (Vector3.Dot(target.right, FindAxis) < Dot)
            {
                Dot = Vector3.Dot(target.right, FindAxis);
                MoveDis = new Vector3(1, 0, 0);
            }
            if (Vector3.Dot(target.right * -1, FindAxis) < Dot)
            {
                Dot = Vector3.Dot(target.right * -1, FindAxis);
                MoveDis = new Vector3(-1, 0, 0);
            }
            if (Vector3.Dot(target.forward, FindAxis) < Dot)
            {
                Dot = Vector3.Dot(target.forward, FindAxis);
                MoveDis = new Vector3(0, 0, 1);
            }
            if (Vector3.Dot(target.forward * -1, FindAxis) < Dot)
            {
                Dot = Vector3.Dot(target.forward * -1, FindAxis);
                MoveDis = new Vector3(0, 0, -1);
            }

            target.Translate(MoveDis * -dis);
        }

        /// <summary>
        /// 按距离排序transform子级数组
        /// </summary>
        /// <param name="MainTrans">输入父级</param>
        /// <returns></returns>
        static Transform[] SqueBones(Transform MainTrans)
        {
            Transform[] RetunObject = new Transform[MainTrans.childCount];
            for (int i = 0; i < MainTrans.childCount; i++)
            {
                float MainDis = Vector3.Distance(MainTrans.position, MainTrans.GetChild(i).position);
                int Number = 0;
                for (int t = 0; t < MainTrans.childCount; t++)
                {
                    if (t != i) if (Vector3.Distance(MainTrans.position, MainTrans.GetChild(t).position) < MainDis) Number++;
                }
                RetunObject[Number] = MainTrans.GetChild(i);
            }
            return RetunObject;
        }

        /// <summary>
        /// 按距离排序transform子级数组 返回Gameobject数组
        /// </summary>
        /// <param name="MainTrans">输入父级</param>
        /// <returns></returns>
        static GameObject[] SqueBones(Transform MainTrans, bool ReturnGameObject)
        {
            GameObject[] RetunObject = new GameObject[MainTrans.childCount];
            for (int i = 0; i < MainTrans.childCount; i++)
            {
                float MainDis = Vector3.Distance(MainTrans.position, MainTrans.GetChild(i).position);
                int Number = 0;
                for (int t = 0; t < MainTrans.childCount; t++)
                {
                    if (t != i) if (Vector3.Distance(MainTrans.position, MainTrans.GetChild(t).position) < MainDis) Number++;
                }
                RetunObject[Number] = MainTrans.GetChild(i).gameObject;
            }
            return RetunObject;
        }

        /// <summary>
        /// 按距离排序transform子级数组  按名字收集对象  返回Gameobject数组
        /// </summary>
        /// <param name="MainTrans">输入父级</param>
        /// <returns></returns>
        static GameObject[] SqueBones(Transform MainTrans, string Name, bool ReturnGameObject)
        {
            GameObject[] MathfObject = new GameObject[MainTrans.childCount];
            int Js = 0;
            for (int i = 0; i < MathfObject.Length; i++)
            {
                string[] name = MainTrans.GetChild(i).name.Split('_');
                if (name[0] == Name)
                {
                    MathfObject[Js] = MainTrans.GetChild(i).gameObject;
                    Js++;
                }
            }

            GameObject[] RetunObject = new GameObject[Js];
            for (int i = 0; i < Js; i++)
            {
                float MainDis = Vector3.Distance(MainTrans.position, MathfObject[i].transform.position);
                int Number = 0;
                for (int t = 0; t < Js; t++)
                {
                    if (t != i) if (Vector3.Distance(MainTrans.position, MathfObject[t].transform.position) < MainDis) Number++;
                }
                RetunObject[Number] = MathfObject[i];
            }
            return RetunObject;
        }

        /// <summary>
        /// 按距离排序transform子级数组  按名字收集对象  返回Transform数组
        /// </summary>
        /// <param name="MainTrans">输入父级</param>
        /// <returns></returns>
        static Transform[] SqueBones(Transform MainTrans, string Name)
        {
            Transform[] MathfObject = new Transform[MainTrans.childCount];
            int Js = 0;
            for (int i = 0; i < MathfObject.Length; i++)
            {
                string[] name = MainTrans.GetChild(i).name.Split('_');
                if (name[0] == Name)
                {
                    MathfObject[Js] = MainTrans.GetChild(i);
                    Js++;
                }
            }

            Transform[] RetunObject = new Transform[Js];
            for (int i = 0; i < Js; i++)
            {
                float MainDis = Vector3.Distance(MainTrans.position, MathfObject[i].position);
                int Number = 0;
                for (int t = 0; t < Js; t++)
                {
                    if (t != i) if (Vector3.Distance(MainTrans.position, MathfObject[t].position) < MainDis) Number++;
                }
                RetunObject[Number] = MathfObject[i];
            }
            return RetunObject;
        }

        /// <summary>
        /// 删除捩骨控制器
        /// </summary>
        /// <param name="MainTrans"></param>
        static void RestTwist(Transform MainTrans)
        {
            for (int i = 0; i < MainTrans.childCount; i++)
                if (MainTrans.GetChild(i).GetComponent<RotationConstraint>()) DestroyImmediate(MainTrans.GetChild(i).GetComponent<RotationConstraint>());
        }
        #endregion
    }
}
