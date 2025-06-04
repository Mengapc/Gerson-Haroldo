using Unity.Mathematics;
using UnityEngine;

public class LuzPiscando : MonoBehaviour
{
    [SerializeField]
    private Light luz;

    public float intensidadeMin = 1.0f;
    public float intensidadeMax = 2.0f;

    public float velociadeMin = 3.0f;
    public float velocidadeMax = 7.0f;
    private float velocidade;

    private Color[] corespossiveis =
    {
        new(1f, 0.7f, 0.3f), //amarelo
        new(1f, 0.5f, 0.7f), //rosa
        new(0.5f, 0.0f, 1f), //roxo
        new(0.3f, 0.5f, 1f) //azul
    };

    void Start()
    {
        MudarCorAleatoria();
        velocidade = UnityEngine.Random.Range(velociadeMin, velocidadeMax);
    }

    void Update()
    {
        luz.intensity = Mathf.Lerp(intensidadeMin, intensidadeMax, Mathf.PerlinNoise(Time.time * velocidade, 0.0f));
    }

    void MudarCorAleatoria()
    {
        int indicealeatorio = UnityEngine.Random.Range(0, corespossiveis.Length);
        luz.color = corespossiveis[indicealeatorio];
    }
}
