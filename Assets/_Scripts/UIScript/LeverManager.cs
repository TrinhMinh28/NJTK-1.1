using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
#if UNITY_EDITOR
// Mã chỉ dùng trong chế độ Editor
using UnityEditor.SearchService;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeverManager : MonoBehaviour
{
    [SerializeField] private GameObject _LoaderCanvas;
    [SerializeField] private Image _imageProgesBar;
    private float _target; 
    public static LeverManager Instance;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public async void LoadScence (string scanceName)
    {
        _target = 0;
        _imageProgesBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(scanceName);
        scene.allowSceneActivation = false;
        _LoaderCanvas.SetActive(true);
        do
        {
            await Task.Delay(100);
            _target = scene.progress;
        } while (scene.progress < 0.9f);
        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        _LoaderCanvas.SetActive(false);
    }
    public async void UpdateTargetValue()
    {
        _imageProgesBar.fillAmount = 0;
        _LoaderCanvas.SetActive(true);
        Debug.Log("LoadMap");
        float elapsedTime = 0;
        while (elapsedTime < 0.95f)
        {
            await Task.Delay(50);
            elapsedTime += 0.05f;
            _target = elapsedTime;
        }

        await Task.Delay(500);
        _LoaderCanvas.SetActive(false);
    }
    private void Update()
    {
        _imageProgesBar.fillAmount = Mathf.MoveTowards (_imageProgesBar.fillAmount,_target,3 *Time.deltaTime);
    }

}
