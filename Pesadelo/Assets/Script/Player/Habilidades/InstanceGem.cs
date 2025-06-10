using System.Collections.Generic;
using UnityEngine;
using System;


public class InstanceGem : MonoBehaviour
{
    [Header("Configura��o da Habilidade")]
    public float cooldownHabilidade = 5f;
    public GameObject efectArea;
    public float tempoAréaHammer;

    [Header("Refer�ncias")]
    public InventBarSelect ib;
    public Gemas Gemas;
    public Gemas.TypeGem typeGem;
    private Dictionary<Armas.ItemType, Dictionary<Armas.Element, Action>> habilidades;
    private float proximoUsoDisponivel = 0f;


    private void Awake()
    {

        ib = FindObjectOfType<InventBarSelect>();

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

    private void CreatArea()
    {

    }
}
