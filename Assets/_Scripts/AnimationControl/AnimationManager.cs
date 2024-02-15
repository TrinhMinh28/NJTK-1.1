using Spine.Unity;
using System.Collections;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    private string _AnimationName;
    private bool isAnimationRunning = false;
    float nextTime = 0f;
    int Animcount = 0;
    float animationDuration;
    int indexAnim ;
    float indexcheck = 0;
    private void Awake()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
        Animcount = spineAnimationState.Data.SkeletonData.Animations.Count;
        Debug.Log("Animcount  : " + Animcount.ToString());
    }
    public int GetIndexAnim()
    {
        return indexAnim;
    }
    public void SetIndexAnim(int value)
    {
       // indexcheck += 1;
         indexAnim = value ;
       // Debug.Log("Indexcheck AnimManager :" + indexcheck);
    }
    public void PlayAnimation(string animationName,bool DoTre, bool repeat)
    {
        if (!repeat)
        {
            if (!DoTre)
            {
                if (animationName == GetCurrentAnimationName())
                {
                    if (Time.time > nextTime)
                    {
                        nextTime = Time.time + (animationDuration * 0.7f);
                        StartAnim(animationName, repeat);
                    }
                }
                else
                {
                    StartAnim(animationName, repeat);
                }
            }
            else if (DoTre)
            {
                if (Time.time > nextTime + (animationDuration * 0.7f))
                {
                    nextTime = Time.time + (animationDuration - animationDuration);
                    StartAnim(animationName, repeat);
                }
            }
        }
        else
        {
            StartAnim(animationName, repeat);
        }
        isAnimationRunning = true; // Anim đang chạy
    }
    void TimeDelayAnim(string animationName)
    {
        spineAnimationState.SetAnimation(0, animationName, true);
    }
    void StartAnim(string AnimName,bool repeat)
    {
        GetDurationAnim(AnimName);
        spineAnimationState.SetAnimation(0, AnimName, repeat);
    }
    private string GetCurrentAnimationName()
    {
        if (skeletonAnimation != null)
        {
             _AnimationName = skeletonAnimation.AnimationName;
            return _AnimationName;
        }

        // Trả về một giá trị mặc định nếu không tìm thấy tên animation
        return "DefaulAnimationName";
    }
    void GetDurationAnim( string Name)
    {
        var animation = spineAnimationState.Data.SkeletonData.FindAnimation(Name);
        if (animation != null)
        {
             animationDuration = animation.Duration;
            //Debug.Log("Animation duration: " + animationDuration + " seconds"); độ dài của animation 
        }
    }
}
