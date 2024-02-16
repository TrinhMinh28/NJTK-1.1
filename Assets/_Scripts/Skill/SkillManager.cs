using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public bool SkillNull;
    private static SkillManager instance;

    public SkillWeapons SkillSelected;
    public CreateSkillBang SkillBangSelected;
    public CreateSkillHoa SkillPhongSelected;
    public CreateSkillPhong SkillHoaSelected;
    public CreateSkillBase SkillBase;
    public static SkillManager Instance
    {
        get
        {
            // Nếu instance đã bị hủy bỏ, tìm lại và gán lại
            if (instance == null)
            {
                instance = FindObjectOfType<SkillManager>();

                // Nếu không tìm thấy, tạo mới instance
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<SkillManager>();
                    singletonObject.name = "SkillManager (Generated)";
                }

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private void Awake()
    {
        // Đảm bảo chỉ có một instance duy nhất
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SkillNull = true;
    }


    public void UpdateUseSkill(SkillWeapons skillweapons)
    {
        if (skillweapons.SkillBang != null || skillweapons.SkillHoa != null || skillweapons.SkillPhong != null)
        {
            SkillSelected = skillweapons;
            SkillNull = false;
        }
        else if (skillweapons.SkillBase != null )
        {
            SkillSelected = skillweapons;
            SkillNull = false;
        }
        else
        {
            Debug.LogError("Chưa thêm Skill cho Vũ khí");
            GameManager.Instance.ShowThongBao("Kỹ năng không khả dụng");
            SkillNull = true;
            SkillSelected = skillweapons;
        }
            SkillBase = SkillSelected.SkillBase;
            SkillBangSelected = SkillSelected.SkillBang;
            SkillPhongSelected = SkillSelected.SkillHoa;
            SkillHoaSelected = SkillSelected.SkillPhong;
        
      

    }
}
