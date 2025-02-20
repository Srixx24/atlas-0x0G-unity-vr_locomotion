using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapTransforms
{
    public Transform vrTarget;
    public Transform IKTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void VRAvatar()
    {
        IKTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        IKTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}
public class AvatarController : MonoBehaviour
{
    [SerializeField] private MapTransforms head;
    [SerializeField] private MapTransforms leftHand;
    [SerializeField] private MapTransforms rightHand;

    // New variables for mount handling
    [SerializeField] public Transform followFrontLeftFoot;
    [SerializeField] public Transform followFrontRightFoot;
    [SerializeField] public Transform followBackLeftFoot;
    [SerializeField]public Transform followBackRightFoot;
    [SerializeField] private Transform mountHipJoint; // Main hip joint of the mount
    [SerializeField] private float turnSmooth;
    [SerializeField] private Transform IKHead;
    [SerializeField] private Vector3 headBodyOffset;
    public CharacterController FollowPlayer;
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        // Position the avatar at the head's IK position offset by the body offset
        transform.position = IKHead.position + headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(IKHead.forward, Vector3.up).normalized, Time.deltaTime * turnSmooth);

        // Update the IK targets
        head.VRAvatar();
        leftHand.VRAvatar();
        rightHand.VRAvatar();
        
        // Update the mount's hip joint position
        if (mountHipJoint != null)
        {
            Vector3 hipOffset = new Vector3(0, 1.0f, 0);
            mountHipJoint.position = transform.position + hipOffset; 
            mountHipJoint.rotation = transform.rotation;
        }
    }
private void OnAnimatorIK(int layerIndex)
    {
        // Update foot positions using IK
        UpdateFootIK(followFrontLeftFoot, AvatarIKGoal.LeftFoot);
        UpdateFootIK(followFrontRightFoot, AvatarIKGoal.RightFoot);
        UpdateFootIK(followBackLeftFoot, AvatarIKGoal.LeftFoot);
        UpdateFootIK(followBackRightFoot, AvatarIKGoal.RightFoot);
    }

    private void UpdateFootIK(Transform footTransform, AvatarIKGoal footGoal) 
    {
        if (animator)
        {
            if (footTransform != null)
            {
                animator.SetIKPositionWeight(footGoal, 1);
                animator.SetIKPosition(footGoal, footTransform.position);
            }
            else
            {
                animator.SetIKPositionWeight(footGoal, 0); // Reset weight if no foot transform
            }
        }
    }
}