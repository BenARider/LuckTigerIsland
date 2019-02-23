using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GraphicsSettings : MonoBehaviour
{
   private Resolution[] m_resolutions;
  // public TMPro.TMP_Dropdown resolutionDropdown;
    // Use this for initialization
    void Start()
    {
        /*
        resolutionDropdown.ClearOptions();
        m_resolutions = Screen.resolutions;
        int m_currentResolutionIndex = 0;
        //Gets all of the resolutions that are possible
        List<string> m_options = new List<string>();
      
        for (int i = 0; i < m_resolutions.Length; i++)
        {
            if(m_resolutions[i].refreshRate == 60 )
            {
                string m_option = m_resolutions[i].width + " x " + m_resolutions[i].height;
                m_options.Add(m_option);
                if (m_resolutions[i].width == Screen.width && m_resolutions[i].height == Screen.height)
                {
                    m_currentResolutionIndex = i;
                }
            }
        
        }
        resolutionDropdown.AddOptions(m_options);
        resolutionDropdown.value = m_currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        */
        
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = m_resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool m_isFullscreen)
    {
        Screen.fullScreen = m_isFullscreen;
    }
}
