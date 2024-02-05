using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Reflection;

// ExecuteAlways : ����Ʈ ��� or �÷��� ����� �� ���� �ϱ� ���� ��Ʈ����Ʈ
[ExecuteAlways]
public class RendererFeatureController : MonoBehaviour
{
    [SerializeField]
    private UniversalRenderPipelineAsset pipelineAsset;     // ���� ������� �������������� ����
    [SerializeField]
    private List<ScriptableRendererFeature> rendererFeatures = new List<ScriptableRendererFeature>();

    public string[] defaultRendererSettings;

    private void Start()
    {
        pipelineAsset = (UniversalRenderPipelineAsset)GraphicsSettings.renderPipelineAsset;
        rendererFeatures.Clear();

        FieldInfo fieldInfo = pipelineAsset.GetType().GetField("m_RendererDataList", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        ScriptableRendererData[] rendererDatas = (ScriptableRendererData[])fieldInfo.GetValue(pipelineAsset);
        
        foreach (ScriptableRendererFeature rendererFeature in rendererDatas[0].rendererFeatures)
        {
            ScriptableRendererFeature renderFeature;
            renderFeature = rendererFeature;
            rendererFeature.SetActive(false);
            rendererFeatures.Add(renderFeature);
        }

        foreach(string item in defaultRendererSettings)
        {
            SetRenderer(item);
        }
    }

    // ������ ����
    public void SetRenderer(string name)
    {
        ScriptableRendererFeature rendererFeature = rendererFeatures.Find(item => item.name.Equals(name));
        rendererFeature.SetActive(!rendererFeature.isActive);     
    }


  

}
