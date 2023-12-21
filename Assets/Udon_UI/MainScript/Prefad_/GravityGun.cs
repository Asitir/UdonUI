
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UdonUI
{
    public class GravityGun : UdonSharpBehaviour
    {
        [Header("枪口，对象Z轴")]
        public Transform gunpoint;
        [Header("可射击层级")]
        public LayerMask targetLayer;
        [Header("射击光线")]
        public Transform lightLine;
        [Header("击中特效")]
        public ParticleSystem partic;
        [Header("射击距离")]
        [Range(1, 1000)]
        public float FiringRange = 10;

        [Space(30)]
        [Header("强度")]
        public float gunPower = 500;

        private Transform lightLine_s;
        private bool isUseM = false;
        private float use_M = -1, use_S = -1;
        private float lineAnimSpeed = 5;
        private AudioSource audio;
        private bool isAudio = false;
        private void Start()
        {
            //lightLine.localScale = Vector3.zero;
            lightLine.parent = transform.parent;
            lightLine_s = Instantiate(lightLine.gameObject, transform.parent).transform;
            ResetLightLine(lightLine);
            ResetLightLine(lightLine_s);
            foreach (var item in gunpoint.GetComponents<AudioSource>())
            {
                audio = item;
                isAudio = true;
                break;
            }
        }

        private void Update()
        {
            float _deltaTime = Time.deltaTime;
            if (use_M > 0)
            {
                use_M -= _deltaTime;
                if (use_M <= 0)
                {
                    ResetLightLine(lightLine);
                    //lightLine.localScale = Vector3.zero;
                    //lightLine.transform.position = Vector3.down * 500;
                }
                else
                {
                    Vector3 nowScale = Vector3.one * (Mathf.Lerp(lightLine.localScale.x, 0, _deltaTime * lineAnimSpeed));
                    nowScale.z = lightLine.localScale.z;
                    lightLine.localScale = nowScale;
                }
            }

            if (use_S > 0)
            {
                use_S -= _deltaTime;
                if (use_S <= 0)
                {
                    ResetLightLine(lightLine_s);
                    //lightLine_s.localScale = Vector3.zero;
                    //lightLine_s.transform.position = Vector3.down * 500;
                }
                else
                {
                    Vector3 nowScale = Vector3.one * (Mathf.Lerp(lightLine_s.localScale.x, 0, _deltaTime * lineAnimSpeed));
                    nowScale.z = lightLine_s.localScale.z;
                    lightLine_s.localScale = nowScale;
                }
            }
        }

        public override void Interact()
        {
            //base.Interact();
            //OnShot();
        }

        public override void OnPickupUseDown()
        {
            OnShot();
            //base.OnPickupUseDown();
        }

        private void OnShot()
        {
            //isUseM = !isUseM;

            //if (Physics.Raycast(gunpoint.position, gunpoint.forward, out RaycastHit _ray, FiringRange, targetLayer, QueryTriggerInteraction.Ignore))
            //{
            //    foreach (var item in _ray.collider.GetComponents<Rigidbody>())
            //    {
            //        Networking.SetOwner(Networking.LocalPlayer, item.gameObject);
            //        item.useGravity = !item.useGravity;//切换重力
            //        item.AddForceAtPosition(gunpoint.forward * gunPowor, _ray.point);
            //        break;
            //    }
            //}
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SyncFuntion");
        }

        private void ResetLightLine(Transform _target)
        {
            _target.localScale = Vector3.zero;
            _target.transform.position = Vector3.down * 500;
        }
        public void SyncFuntion()
        {
            //if (Networking.LocalPlayer.IsUserInVR()) { }
            if (isAudio)
            {
                audio.Play();
            }
            isUseM = !isUseM;
            if (Physics.Raycast(gunpoint.position, gunpoint.forward, out RaycastHit _ray, FiringRange, targetLayer, QueryTriggerInteraction.Ignore))
            {
                foreach (var item in _ray.collider.GetComponents<Rigidbody>())
                {
                    item.useGravity = !item.useGravity;//切换重力
                    foreach (var item2 in item.GetComponents<UdonBehaviour>())
                    {
                        if (item.useGravity) item2.SendCustomEvent("SetGravityOn");
                        else item2.SendCustomEvent("SetGravityOff");

                        break;
                    }
                    //item.isKinematic = !item.useGravity;
                    {
                        if (Networking.GetOwner(gameObject).isLocal)
                        {
                            if (item.useGravity)
                            {
                                item.isKinematic = false;
                                Networking.SetOwner(Networking.LocalPlayer, item.gameObject);
                                item.AddForceAtPosition(gunpoint.forward * gunPower, _ray.point);
                            }
                            else
                            {
                                item.isKinematic = true;
                            }
                        }
                    }
                    break;
                }

                if (isUseM)
                {
                    lightLine.position = gunpoint.position;
                    lightLine.rotation = gunpoint.rotation;

                    Vector3 _lineScale = Vector3.one;
                    _lineScale.z = (_ray.point - gunpoint.position).magnitude;
                    lightLine.localScale = _lineScale;
                    use_M = 1;
                }
                else
                {
                    lightLine_s.position = gunpoint.position;
                    lightLine_s.rotation = gunpoint.rotation;

                    Vector3 _lineScale = Vector3.one;
                    _lineScale.z = (_ray.point - gunpoint.position).magnitude;
                    lightLine_s.localScale = _lineScale;
                    use_S = 1;
                }

                partic.transform.position = _ray.point;
                partic.Play();
            }
            else
            {
                if (isUseM)
                {
                    lightLine.position = gunpoint.position;
                    lightLine.rotation = gunpoint.rotation;

                    Vector3 _lineScale = Vector3.one;
                    _lineScale.z = FiringRange;
                    lightLine.localScale = _lineScale;
                    use_M = 1;
                }
                else
                {
                    lightLine_s.position = gunpoint.position;
                    lightLine_s.rotation = gunpoint.rotation;

                    Vector3 _lineScale = Vector3.one;
                    _lineScale.z = FiringRange;
                    lightLine_s.localScale = _lineScale;
                    use_S = 1;
                }
            }

        }
    }
}
