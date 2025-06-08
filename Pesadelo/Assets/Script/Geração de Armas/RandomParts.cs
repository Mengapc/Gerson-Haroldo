using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

[DisallowMultipleComponent]

public class RandomParts : MonoBehaviour
{
    // ... (suas listas de partes continuam aqui) ...
    [Header("Partes das armas")]
    [SerializeField] private List<GameObject> Gema;
    [Header("Cajado")]
    [SerializeField] private List<GameObject> cajadoPontaP;
    [SerializeField] private List<GameObject> cajadoCabo;
    [Header("Espada")]
    [SerializeField] private List<GameObject> espadaLamina;
    [SerializeField] private List<GameObject> espadaGuardaP;
    [SerializeField] private List<GameObject> espadaCabo;
    [Header("Martelo")]
    [SerializeField] private List<GameObject> martelocabecaP;
    [SerializeField] private List<GameObject> marteloCabo;
    [Header("VFX_Raridades")]
    [SerializeField] private List<GameObject> vfxObject;

    public GameObject GeneratePrincipalPartArm(Armas.ItemType itemType, Transform armBase)
    {
        // ... (esta função está correta e pode continuar a mesma) ...
        Debug.Log("Gerando parte principal para o tipo: " + itemType);

        GameObject item = null;

        switch (itemType)
        {
            case Armas.ItemType.Sword:
                if (espadaGuardaP != null && espadaGuardaP.Count > 0)
                    item = espadaGuardaP[Random.Range(0, espadaGuardaP.Count)];
                else
                    return null;
                break;

            case Armas.ItemType.Staff:
                if (cajadoPontaP != null && cajadoPontaP.Count > 0)
                    item = cajadoPontaP[Random.Range(0, cajadoPontaP.Count)];
                else
                    return null;
                break;

            case Armas.ItemType.Hammer:
                if (martelocabecaP != null && martelocabecaP.Count > 0)
                    item = martelocabecaP[Random.Range(0, martelocabecaP.Count)];
                else
                    return null;
                break;

            default:
                Debug.LogWarning("Tipo de item não suportado.");
                return null;
        }

        if (item == null) return null;

        GameObject instance = Instantiate(item, armBase);
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localRotation = Quaternion.identity;

        return instance;
    }

    public void GenerationOutherParts(Armas.ItemType itemType, GameObject newItem, Armas.Rarity rarity)
    {
        int randowPart;
        LockParts infoItem = newItem.GetComponent<LockParts>();
        if (infoItem == null)
        {
            Debug.LogError($"O objeto principal '{newItem.name}' não possui o script 'LockParts'!", newItem);
            return;
        }

        GameObject novoVfx;
        switch (itemType)
        {
            case Armas.ItemType.Sword:
                // --- CORREÇÃO APLICADA AQUI ---
                // Instancia a gema DIRETAMENTE como filha do 'lock point' (infoItem.gema).
                randowPart = Random.Range(0, Gema.Count);
                GameObject novaGema = Instantiate(Gema[randowPart], infoItem.gema);
                // Opcional mas recomendado: resetar posição local após instanciar com pai.
                novaGema.transform.localPosition = Vector3.zero;
                novaGema.transform.localRotation = Quaternion.identity;

                // Aninha o VFX na gema
                novoVfx = Instantiate(VfxArms(rarity), novaGema.transform);
                novoVfx.transform.localPosition = Vector3.zero;

                // Cria as outras partes da espada...
                randowPart = Random.Range(0, espadaCabo.Count);
                Instantiate(espadaCabo[randowPart], infoItem.cabo);

                randowPart = Random.Range(0, espadaLamina.Count);
                Instantiate(espadaLamina[randowPart], infoItem.lamina);
                break;

            case Armas.ItemType.Staff:
                // --- CORREÇÃO APLICADA AQUI ---
                randowPart = Random.Range(0, Gema.Count);
                GameObject novaGemaStaff = Instantiate(Gema[randowPart], infoItem.gema);
                novaGemaStaff.transform.localPosition = Vector3.zero;

                randowPart = Random.Range(0, cajadoCabo.Count);
                GameObject novoCaboCajado = Instantiate(cajadoCabo[randowPart], infoItem.cabo);

                novoVfx = Instantiate(VfxArms(rarity), novoCaboCajado.transform);
                novoVfx.transform.localPosition = Vector3.zero;
                break;

            case Armas.ItemType.Hammer:
                // --- CORREÇÃO APLICADA AQUI ---
                randowPart = Random.Range(0, Gema.Count);
                GameObject novaGemaMartelo = Instantiate(Gema[randowPart], infoItem.gema);
                novaGemaMartelo.transform.localPosition = Vector3.zero;

                randowPart = Random.Range(0, marteloCabo.Count);
                GameObject novoCaboMartelo = Instantiate(marteloCabo[randowPart], infoItem.cabo);

                novoVfx = Instantiate(VfxArms(rarity), novoCaboMartelo.transform);
                novoVfx.transform.localPosition = Vector3.zero;
                break;
        }
    }
    private GameObject VfxArms(Armas.Rarity rarity)
    {
        if ((int)rarity >= vfxObject.Count) return null;
        GameObject vfx = vfxObject[(int)rarity];
        return vfx;
    }
}
