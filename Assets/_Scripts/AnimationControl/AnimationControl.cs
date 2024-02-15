using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
public class AnimationControl : MonoBehaviour
{
    /// <summary>
    /// Class này xây nên nhằm chuyên xử lý các even của animation cho mục đích âm thanh.
    /// </summary>
    private SkeletonAnimation skeletonAnimation;
    private AnimationManager animationManager;
    private PlayerControler playerControler;
    protected int indexAnimPlayer = 0;
    private void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        animationManager = GetComponent<AnimationManager>();
        skeletonAnimation.AnimationState.Event += HandleAnimationEvent;
        playerControler = GetComponent<PlayerControler>();
    }
    private void HandleAnimationEvent(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == "Hit")
        {
            // Xử lý sự kiện "hit" ở đây
            Debug.Log("Hit event triggered");
        }
        else if (e.Data.Name == "Move End")
        {
            // Xử lý sự kiện "move end" ở đây
            Debug.Log("Move end event triggered");
        }
    }
    public int getindex()
    {
        return indexAnimPlayer;
    }
    public void setindex(int index)
    {
        indexAnimPlayer = index;
    }
   
    public void AnimJump3()
    {
        animationManager.PlayAnimation("jump3".ToString(), false, false);
        Invoke("AnimJump2", 0.5f);
        indexAnimPlayer = 1;
        playerControler.ideIndex = indexAnimPlayer;
    }
    public void AnimJump2()
    {
        animationManager.PlayAnimation("jump2".ToString(), false, false);
        indexAnimPlayer = 1;
        playerControler.ideIndex = indexAnimPlayer;
    }
    public void AnimJump()
    {
        animationManager.PlayAnimation("jump".ToString(), false, false);
        indexAnimPlayer = 1;
        playerControler.ideIndex = indexAnimPlayer;
    }
    public void AnimIdle()
    {
        animationManager.PlayAnimation("idle".ToString(), false, true);
        Debug.Log("ILDe da dc goi");
        playerControler.ideIndex = 0;
    }
    public void AnimLand()
    {
        animationManager.PlayAnimation("land".ToString(), false, false);
        indexAnimPlayer = 1;
        playerControler.ideIndex = indexAnimPlayer;
    }  
    public void AnimDefault()
    {
        animationManager.PlayAnimation("default".ToString(), false, false);
        indexAnimPlayer = 1;
        playerControler.ideIndex = indexAnimPlayer;
    }
    public void AnimSkid()
    {
        animationManager.PlayAnimation("skid".ToString(), false, false);
        indexAnimPlayer = 1;
        playerControler.ideIndex = indexAnimPlayer;
    }
    public void AnimRun()
    {
        animationManager.PlayAnimation("run".ToString(), false, false);
        indexAnimPlayer = 1;
        playerControler.ideIndex = indexAnimPlayer;
    }
    public void AnimWalk()
    {
        animationManager.PlayAnimation("walk".ToString(), false, false);
        indexAnimPlayer = 1;
        playerControler.ideIndex = indexAnimPlayer;
    }
    public void AnimLeftMouse()
    {
        animationManager.PlayAnimation("pistolFarShoot".ToString(), false, false);
        indexAnimPlayer = 1;
        playerControler.ideIndex = indexAnimPlayer;
    }
    public void AnimRightMouse()
    {
        animationManager.PlayAnimation("meleeSwing1-fullBody".ToString(), false, false);
        indexAnimPlayer = 1;
        playerControler.ideIndex = indexAnimPlayer;
    }
}
