using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
using Spine;
public class InputAnimation : MonoBehaviour
{
    private void Start()
    {
        // Lấy component SkeletonAnimation từ cùng một game object
        var skeletonAnimation = GetComponent<SkeletonAnimation>();

        if (skeletonAnimation != null && skeletonAnimation.skeletonDataAsset != null)
        {
            var animations = skeletonAnimation.skeletonDataAsset.GetSkeletonData(false).Animations;
            List<string> animationNames = new List<string>();

            foreach (var animation in animations)
            {
                animationNames.Add(animation.Name);
            }

            // In danh sách các tên animation
            //foreach (var animationName in animationNames)
            //{
            //    Debug.Log("Animation: " + animationName);
            //}
        }
    }


}
