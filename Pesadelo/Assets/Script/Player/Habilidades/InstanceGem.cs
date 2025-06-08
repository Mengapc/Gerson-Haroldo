using System.Collections.Generic;
using UnityEngine;
using System;
// A linha abaixo pode n�o ser necess�ria se voc� n�o a estiver usando para mais nada.
// using UnityEngine.Rendering.VirtualTexturing;

public class InstanceGem : MonoBehaviour
{
    [Header("Configura��o da Habilidade")]
    public float cooldownHabilidade = 5f;

    [Header("Refer�ncias")]
    // Este campo agora ser� preenchido automaticamente via c�digo.
    // N�o precisa mais se preocupar em arrast�-lo no Inspector do Prefab.
    public InventBarSelect ib;
    public Gemas Gemas;
    public Gemas.TypeGem typeGem;


    private Dictionary<Armas.ItemType, Dictionary<Armas.Element, Action>> habilidades;


    private float proximoUsoDisponivel = 0f;

    // NOVO M�TODO AWAKE
    private void Awake()
    {
        // Procura na cena inteira por um componente do tipo InventBarSelect
        // e armazena a refer�ncia na vari�vel 'ib'.
        ib = FindObjectOfType<InventBarSelect>();

        // � uma boa pr�tica verificar se a refer�ncia foi encontrada.
        if (ib == null)
        {
            Debug.LogError("N�o foi poss�vel encontrar o 'InventBarSelect' na cena! A habilidade n�o funcionar�.");
        }

        InicializarDicionarioHabilidades();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TentarUsarHabilidade();
        }
    }
    public void TentarUsarHabilidade()
    {
        if (Time.time < proximoUsoDisponivel)
        {
            float tempoRestante = proximoUsoDisponivel - Time.time;
            Debug.Log($"Habilidade em cooldown! Espere mais {tempoRestante:F1} segundos.");
            return;
        }

        // CORRE��O IMPORTANTE: Verifique se 'ib' n�o � nulo ANTES de tentar usar 'ib.equipArm'
        if (ib == null || ib.equipArm == null)
        {
            Debug.LogWarning("Invent�rio ou arma n�o encontrados! N�o � poss�vel usar habilidade.");
            return;
        }

        ItemInstance dadosDaArma = ib.equipArm.GetComponent<ItemInstance>();
        if (dadosDaArma == null)
        {
            Debug.LogError($"A arma equipada '{ib.equipArm.name}' n�o possui o script 'ItemInstance'!");
            return;
        }

        ExecutarHabilidadeEspecial(dadosDaArma.type, dadosDaArma.element);

        proximoUsoDisponivel = Time.time + cooldownHabilidade;
    }

    // O resto do seu c�digo permanece igual...
    private void ExecutarHabilidadeEspecial(Armas.ItemType tipoArma, Armas.Element elemento)
    {
        if (habilidades.TryGetValue(tipoArma, out var habilidadesPorElemento))
        {
            if (habilidadesPorElemento.TryGetValue(elemento, out var habilidade))
            {
                habilidade?.Invoke();
            }
            else
            {
                Debug.LogWarning($"Habilidade n�o encontrada para a combina��o -> Arma: {tipoArma}, Elemento: {elemento}");
            }
        }
    }

    private void InicializarDicionarioHabilidades()
    {
        habilidades = new Dictionary<Armas.ItemType, Dictionary<Armas.Element, Action>>();

        // Habilidades do Martelo
        habilidades[Armas.ItemType.Hammer] = new Dictionary<Armas.Element, Action>
        {
            { Armas.Element.Water, EspecialAtackHammerWhater },
            { Armas.Element.Wind, EspecialAtackHammerWind },
            { Armas.Element.Galaxy, EspecialAtackHammerGalaxy }
        };

        // Habilidades do Cajado
        habilidades[Armas.ItemType.Staff] = new Dictionary<Armas.Element, Action>
        {
            { Armas.Element.Water, EspecialAtackStaffWhater },
            { Armas.Element.Wind, EspecialAtackStaffWind },
            { Armas.Element.Galaxy, EspecialAtackStaffGalaxy }
        };

        // Habilidades da Espada
        habilidades[Armas.ItemType.Sword] = new Dictionary<Armas.Element, Action>
        {
            { Armas.Element.Water, EspecialAtackSwordWhater },
            { Armas.Element.Wind, EspecialAtackSwordWind },
            { Armas.Element.Galaxy, EspecialAtackSwordGalaxy }
        };
    }

    //fun��es para cada tipo de arma

    //Martelo
    //�gua
    public void EspecialAtackHammerWhater()
    {

    }
        //Vento
    public void EspecialAtackHammerWind()
    {

    }
        //Gal�xia
    public void EspecialAtackHammerGalaxy()
    {

    }
    //Cajado
        //�gua
    public void EspecialAtackStaffWhater()
    {

    }
        //Vento
    public void EspecialAtackStaffWind()
    {

    }
        //Gal�xia
    public void EspecialAtackStaffGalaxy()
    {

    }

    //Espada
        //�gua
    public void EspecialAtackSwordWhater()
    {

    }   
        //Vento
    public void EspecialAtackSwordWind()
    {

    }
        //Vento
    public void EspecialAtackSwordGalaxy()
    {

    }


}
