using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ApplyShaderToCamera : MonoBehaviour
{
    public Material effectMaterial; // �g�p����V�F�[�_�[�}�e���A��

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (effectMaterial != null)
        {
            // �V�F�[�_�[��K�p���ă����_�����O
            Graphics.Blit(source, destination, effectMaterial);
        }
        else
        {
            // �V�F�[�_�[�Ȃ��ł��̂܂ܕ`��
            Graphics.Blit(source, destination);
        }
    }
}
