using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventBehaviour: StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Xử lý khi vào trạng thái (state)
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Xử lý khi ra khỏi trạng thái (state)
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Xử lý mỗi khung hình khi ở trong trạng thái (state)
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Xử lý khi trạng thái (state) di chuyển
    }

    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Xử lý IK (Inverse Kinematics) cho trạng thái (state)
    }

    public  void OnStateEvent(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorClipInfo[] clipInfo)
    {
        // Thực hiện các hành động tùy chỉnh dựa trên sự kiện
        // Lấy thông tin về sự kiện từ clipInfo[0].clip
        // Ví dụ: AnimationClip clip = clipInfo[0].clip;
        // Sử dụng thông tin trong clip để xác định sự kiện và thực hiện hành động tương ứng
    }
}
