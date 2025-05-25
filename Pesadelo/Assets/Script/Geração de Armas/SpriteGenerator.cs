using UnityEngine;
using System.IO;

public class SpriteGenerator : MonoBehaviour
{
    [Header("Configurações da Câmera")]
    public Camera cameraCaptura;    
    public int larguraSprite = 256;
    public int alturaSprite = 256;
    public bool fundoTransparente = true;
    public Color corDeFundoOpaco = Color.black;

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

        // Verifica se já existe uma renderTexture e a libera antes de criar uma nova
        // Isso pode ser útil se o Start for chamado múltiplas vezes em cenários específicos.
        if (renderTexture != null)
        {
            if (cameraCaptura != null && cameraCaptura.targetTexture == renderTexture)
            {
                cameraCaptura.targetTexture = null;
            }
            renderTexture.Release();
            Destroy(renderTexture);
        }
        renderTexture = new RenderTexture(larguraSprite, alturaSprite, 24);
        cameraCaptura.targetTexture = renderTexture;
    }

    void OnDisable() // Alterado de OnDestroy para OnDisable para cobrir mais casos
    {
        // Limpa a targetTexture da câmera se este script for desabilitado
        // ou se o GameObject for desativado.
        if (cameraCaptura != null && cameraCaptura.targetTexture == renderTexture)
        {
            cameraCaptura.targetTexture = null;
        }
    }

    void OnDestroy()
    {
        // Garante que a targetTexture da câmera seja limpa se ainda estiver definida.
        // OnDisable já deve ter cuidado disso, mas é uma segurança extra.
        if (cameraCaptura != null && cameraCaptura.targetTexture == renderTexture)
        {
            cameraCaptura.targetTexture = null;
        }

        if (renderTexture != null)
        {
            renderTexture.Release();
            Destroy(renderTexture);
            renderTexture = null; // Define como null após destruir
        }
        if (texturaCapturada != null)
        {
            Destroy(texturaCapturada);
            texturaCapturada = null; // Define como null após destruir
        }
    }

    void ConfigurarCameraParaCaptura()
    {
        if (cameraCaptura == null) return; // Adicionada verificação de nulidade

        if (fundoTransparente)
        {
            cameraCaptura.clearFlags = CameraClearFlags.SolidColor;
            cameraCaptura.backgroundColor = new Color(0, 0, 0, 0); 
        }
        else
        {
            cameraCaptura.clearFlags = CameraClearFlags.SolidColor; 
            cameraCaptura.backgroundColor = corDeFundoOpaco;       
        }
    }

    public Sprite CapturarSprite()
    {
        if (cameraCaptura == null || renderTexture == null)
        {
            Debug.LogError("Câmera de captura ou RenderTexture não inicializados ou foram destruídos!");
            // Tenta recriar a RenderTexture se ela for nula mas a câmera existe
            if (cameraCaptura != null && renderTexture == null && enabled && gameObject.activeInHierarchy) {
                Debug.LogWarning("RenderTexture era nula. Tentando recriar...");
                renderTexture = new RenderTexture(larguraSprite, alturaSprite, 24);
                cameraCaptura.targetTexture = renderTexture; // Reatribui à câmera
            } else if (cameraCaptura == null) {
                 return null; // Não pode continuar sem câmera
            } else if (renderTexture == null) {
                return null; // Não pode continuar sem render texture e não pôde recriar
            }
        }

        ConfigurarCameraParaCaptura(); 
        
        cameraCaptura.Render();
        RenderTexture.active = renderTexture;

        if (texturaCapturada == null || texturaCapturada.width != larguraSprite || texturaCapturada.height != alturaSprite || 
            (fundoTransparente && texturaCapturada.format != TextureFormat.RGBA32) || 
            (!fundoTransparente && texturaCapturada.format != TextureFormat.RGB24))
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
