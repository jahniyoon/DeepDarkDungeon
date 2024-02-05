using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Reflection;

// ExecuteAlways : 에디트 모드 or 플레이 모드일 때 무언가 하기 위한 어트리뷰트
[ExecuteAlways]
public class RendererFeatureController : MonoBehaviour
{
    [SerializeField]
    private UniversalRenderPipelineAsset pipelineAsset;     // 현재 사용중인 렌더파이프라인 에셋
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

    // 렌더러 세팅
    public void SetRenderer(string name)
    {
        ScriptableRendererFeature rendererFeature = rendererFeatures.Find(item => item.name.Equals(name));
        rendererFeature.SetActive(!rendererFeature.isActive);     
    }


  

}
