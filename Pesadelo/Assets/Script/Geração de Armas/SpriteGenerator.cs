using UnityEngine;
using System.IO;

public class SpriteGenerator : MonoBehaviour
{
    [Header("Configurações da Câmera")]
    public Camera cameraCaptura;    
    public int larguraSprite = 128;
    public int alturaSprite = 128;
    public bool fundoTransparente = true;

    [Header("Configurações Opcionais de Salvamento")]
    public bool salvarSpriteEmArquivo = false;
    public string prefixoNomeArquivo = "ArmaSprite_";

    private RenderTexture renderTexture;
    private Texture2D texturaCapturada;

    void Start()
    {
        if (cameraCaptura == null)
        {
            Debug.LogError("Câmera de captura não definida! O script será desabilitado.");
            enabled = false;
            return;
        }

        ConfigurarCameraParaCaptura();

        renderTexture = new RenderTexture(larguraSprite, alturaSprite, 24);
        cameraCaptura.targetTexture = renderTexture;
    }

    void OnDestroy()
    {
        if (renderTexture != null)
        {
            renderTexture.Release();
            Destroy(renderTexture);
        }
        if (texturaCapturada != null)
        {
            Destroy(texturaCapturada);
        }
    }

    void ConfigurarCameraParaCaptura()
    {
        if (fundoTransparente)
        {
            cameraCaptura.clearFlags = CameraClearFlags.SolidColor;
            cameraCaptura.backgroundColor = new Color(0, 0, 0, 0);
        }
        else
        {
            cameraCaptura.clearFlags = CameraClearFlags.Skybox;
        }
    }

    public Sprite CapturarSprite()
    {
        if (cameraCaptura == null || renderTexture == null)
        {
            Debug.LogError("Câmera de captura ou RenderTexture não inicializados!");
            return null;
        }

        cameraCaptura.Render();
        RenderTexture.active = renderTexture;

        if (texturaCapturada == null || texturaCapturada.width != larguraSprite || texturaCapturada.height != alturaSprite)
        {
            if (texturaCapturada != null) Destroy(texturaCapturada);
            texturaCapturada = new Texture2D(larguraSprite, alturaSprite, fundoTransparente ? TextureFormat.RGBA32 : TextureFormat.RGB24, false);
        }

        texturaCapturada.ReadPixels(new Rect(0, 0, larguraSprite, alturaSprite), 0, 0);
        texturaCapturada.Apply();

        RenderTexture.active = null;

        Sprite novoSprite = Sprite.Create(texturaCapturada, new Rect(0.0f, 0.0f, larguraSprite, alturaSprite), new Vector2(0.5f, 0.5f), 100.0f);
        novoSprite.name = prefixoNomeArquivo + System.DateTime.Now.ToString("yyyyMMddHHmmssfff");

        return novoSprite;
    }

}