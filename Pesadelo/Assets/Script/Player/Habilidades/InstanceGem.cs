using System.Collections.Generic;
using UnityEngine;
using System;
// A linha abaixo pode não ser necessária se você não a estiver usando para mais nada.
// using UnityEngine.Rendering.VirtualTexturing;

public class InstanceGem : MonoBehaviour
{
    [Header("Configuração da Habilidade")]
    public float cooldownHabilidade = 5f;

    [Header("Referências")]
    // Este campo agora será preenchido automaticamente via código.
    // Não precisa mais se preocupar em arrastá-lo no Inspector do Prefab.
    public InventBarSelect ib;
    public Gemas Gemas;
    public Gemas.TypeGem typeGem;


    private Dictionary<Armas.ItemType, Dictionary<Armas.Element, Action>> habilidades;


    private float proximoUsoDisponivel = 0f;

    // NOVO MÉTODO AWAKE
    private void Awake()
    {
        // Procura na cena inteira por um componente do tipo InventBarSelect
        // e armazena a referência na variável 'ib'.
        ib = FindObjectOfType<InventBarSelect>();

        // É uma boa prática verificar se a referência foi encontrada.
        if (ib == null)
        {
            Debug.LogError("Não foi possível encontrar o 'InventBarSelect' na cena! A habilidade não funcionará.");
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

        // CORREÇÃO IMPORTANTE: Verifique se 'ib' não é nulo ANTES de tentar usar 'ib.equipArm'
        if (ib == null || ib.equipArm == null)
        {
            Debug.LogWarning("Inventário ou arma não encontrados! Não é possível usar habilidade.");
            return;
        }

        ItemInstance dadosDaArma = ib.equipArm.GetComponent<ItemInstance>();
        if (dadosDaArma == null)
        {
            Debug.LogError($"A arma equipada '{ib.equipArm.name}' não possui o script 'ItemInstance'!");
            return;
        }

        ExecutarHabilidadeEspecial(dadosDaArma.type, dadosDaArma.element);

        proximoUsoDisponivel = Time.time + cooldownHabilidade;
    }

    // O resto do seu código permanece igual...
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
                Debug.LogWarning($"Habilidade não encontrada para a combinação -> Arma: {tipoArma}, Elemento: {elemento}");
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

    //funções para cada tipo de arma

    //Martelo
    //Água
    public void EspecialAtackHammerWhater()
    {

    }
        //Vento
    public void EspecialAtackHammerWind()
    {

    }
        //Galáxia
    public void EspecialAtackHammerGalaxy()
    {

    }
    //Cajado
        //Água
    public void EspecialAtackStaffWhater()
    {

    }
        //Vento
    public void EspecialAtackStaffWind()
    {

    }
        //Galáxia
    public void EspecialAtackStaffGalaxy()
    {

    }

    //Espada
        //Água
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
