using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ApplyShaderToCamera : MonoBehaviour
{
    public Material effectMaterial; // 使用するシェーダーマテリアル

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (effectMaterial != null)
        {
            // シェーダーを適用してレンダリング
            Graphics.Blit(source, destination, effectMaterial);
        }
        else
        {
            // シェーダーなしでそのまま描画
            Graphics.Blit(source, destination);
        }
    }
}
