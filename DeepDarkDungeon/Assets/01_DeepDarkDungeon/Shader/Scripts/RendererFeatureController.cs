using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Reflection;

[System.Serializable]
public struct RenderFeatureToggle
{
    public ScriptableRendererFeature feature;
    public bool isEnabled;
}

// ExecuteAlways : 에디트 모드 or 플레이 모드일 때 무언가 하기 위한 어트리뷰트
[ExecuteAlways]
public class RendererFeatureController : MonoBehaviour
{
    [SerializeField]
    private UniversalRenderPipelineAsset pipelineAsset;     // 현재 사용중인 렌더파이프라인 에셋
    [SerializeField]
    private List<RenderFeatureToggle> renderFeatures = new List<RenderFeatureToggle>();

    private void Start()
    {
        pipelineAsset = (UniversalRenderPipelineAsset)GraphicsSettings.renderPipelineAsset;
        renderFeatures.Clear();

        FieldInfo fieldInfo = pipelineAsset.GetType().GetField("m_RendererDataList", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        ScriptableRendererData[] rendererDatas = (ScriptableRendererData[])fieldInfo.GetValue(pipelineAsset);
        
        foreach (ScriptableRendererFeature rendererFeature in rendererDatas[0].rendererFeatures)
        {
            RenderFeatureToggle renderFeature;
            renderFeature.feature = rendererFeature;
            renderFeature.isEnabled = rendererFeature.isActive;
            renderFeatures.Add(renderFeature);
        }
        

    }


    private void Update()
    {
        foreach (RenderFeatureToggle toggleObj in renderFeatures)
        {
            toggleObj.feature.SetActive(toggleObj.isEnabled);
        }
    }

}
