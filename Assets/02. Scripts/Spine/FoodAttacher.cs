using UnityEngine;
using Spine.Unity;
using Spine;

public class FoodAttacher : MonoBehaviour
{
    public SkeletonAnimation skel;
    public Sprite foodSprite;     // 음식 프리팹의 스프라이트

    void Start()
    {
        var slot = skel.Skeleton.FindSlot("FoodSlot");
        //var att = foodSprite.ToRegionAttachmentPMAClone();
        //slot.Attachment = att;
        skel.Update(0);
    }
}
