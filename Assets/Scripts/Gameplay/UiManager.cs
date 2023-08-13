using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// UI manager to handle ui related stuff
/// </summary>
public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject panel_Pause;
    [SerializeField] Dropdown dropdownShapes;

    static System.Action<string> OnClickSpawn;
    public static void Register_OnClickSpawn(System.Action<string> _callback) => OnClickSpawn = _callback;


    private void Awake()
    {
        SetCustorMode(true);
        OnDropdownChanged_Quality(2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnClick_Pause(!panel_Pause.activeInHierarchy);

    }

    public void OnClick_Pause(bool isPause)
    {
        panel_Pause.SetActive(isPause);
        Time.timeScale = isPause ? 0 : 1;
        SetCustorMode(!isPause);
    }

    void SetCustorMode(bool _isLocked)
    {
        Cursor.lockState = _isLocked? CursorLockMode.Locked: CursorLockMode.None;
    }

    public void OnClick_Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

    public void OnDropdownChanged_Shape(int _index)
    {
        OnClickSpawn?.Invoke(dropdownShapes.options[_index].text);
        OnClick_Pause(false);
    }

    public void OnDropdownChanged_Quality(int index)
    {
        UniversalAdditionalCameraData cameraData = Camera.main.GetUniversalAdditionalCameraData();
        cameraData.renderPostProcessing = false;
        
        switch (index)
        {
            case 0: //Low
                QualitySettings.SetQualityLevel(0); 
                break;
            case 1: //Mid
                QualitySettings.SetQualityLevel(1); 
                break;
            case 2: //High
                QualitySettings.SetQualityLevel(2); 
                break;
            case 3: // Very High
                QualitySettings.SetQualityLevel(3);
                cameraData.renderPostProcessing = true;
                break;
            default:
                break;
        }
    }


}
