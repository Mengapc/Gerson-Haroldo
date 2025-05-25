using UnityEngine;
using System.IO;

public class SpriteGenerator : MonoBehaviour
{
    [Header("Configura��es da C�mera")]
    public Camera cameraCaptura;    
    public int larguraSprite = 256;
    public int alturaSprite = 256;
    public bool fundoTransparente = true;
    public Color corDeFundoOpaco = Color.black;

    [Header("Configura��es Opcionais de Salvamento")]
    public bool salvarSpriteEmArquivo = false;
    public string prefixoNomeArquivo = "ArmaSprite_";

    private RenderTexture renderTexture;
    private Texture2D texturaCapturada;

    void Start()
    {
        if (cameraCaptura == null)
        {
            Debug.LogError("C�mera de captura n�o definida! O script ser� desabilitado.");
            enabled = false;
            return;
        }

        ConfigurarCameraParaCaptura();

        // Verifica se j� existe uma renderTexture e a libera antes de criar uma nova
        // Isso pode ser �til se o Start for chamado m�ltiplas vezes em cen�rios espec�ficos.
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
        // Limpa a targetTexture da c�mera se este script for desabilitado
        // ou se o GameObject for desativado.
        if (cameraCaptura != null && cameraCaptura.targetTexture == renderTexture)
        {
            cameraCaptura.targetTexture = null;
        }
    }

    void OnDestroy()
    {
        // Garante que a targetTexture da c�mera seja limpa se ainda estiver definida.
        // OnDisable j� deve ter cuidado disso, mas � uma seguran�a extra.
        if (cameraCaptura != null && cameraCaptura.targetTexture == renderTexture)
        {
            cameraCaptura.targetTexture = null;
        }

        if (renderTexture != null)
        {
            renderTexture.Release();
            Destroy(renderTexture);
            renderTexture = null; // Define como null ap�s destruir
        }
        if (texturaCapturada != null)
        {
            Destroy(texturaCapturada);
            texturaCapturada = null; // Define como null ap�s destruir
        }
    }

    void ConfigurarCameraParaCaptura()
    {
        if (cameraCaptura == null) return; // Adicionada verifica��o de nulidade

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
            Debug.LogError("C�mera de captura ou RenderTexture n�o inicializados ou foram destru�dos!");
            // Tenta recriar a RenderTexture se ela for nula mas a c�mera existe
            if (cameraCaptura != null && renderTexture == null && enabled && gameObject.activeInHierarchy) {
                Debug.LogWarning("RenderTexture era nula. Tentando recriar...");
                renderTexture = new RenderTexture(larguraSprite, alturaSprite, 24);
                cameraCaptura.targetTexture = renderTexture; // Reatribui � c�mera
            } else if (cameraCaptura == null) {
                 return null; // N�o pode continuar sem c�mera
            } else if (renderTexture == null) {
                return null; // N�o pode continuar sem render texture e n�o p�de recriar
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
