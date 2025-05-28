using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class RandomParts : MonoBehaviour
{
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
    [SerializeField] private List <GameObject> vfxObject;

    public GameObject GeneratePrincipalPartArm(Armas.ItemType itemType, Transform armBase)
    {
        Debug.Log("Gerando parte principal para o tipo: " + itemType); // Adicione este log

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

        // Instancia como filho da base (sem alterar posição se quiser ajustar depois)
        GameObject instance = Instantiate(item, armBase);
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localRotation = Quaternion.identity;

        return instance;
    }


    public void GenerationOutherParts(Armas.ItemType itemType, GameObject newItem, Armas.Rarity rarity)
    {
        int randowPart;
        LockParts infoItem = newItem.GetComponent<LockParts>();
        GameObject novoVfx;
        switch (itemType)
        {
            case Armas.ItemType.Sword:

                randowPart = Random.Range(0, Gema.Count);
                GameObject novaGema = Instantiate(Gema[randowPart], infoItem.gema.position, infoItem.gema.rotation);
                novoVfx = Instantiate(VfxArms(rarity), novaGema.transform.position, novaGema.transform.rotation);
                novoVfx.transform.SetParent(novaGema.transform);
                novaGema.transform.SetParent(infoItem.gema);
                novaGema.transform.localPosition = Vector3.zero;
                novaGema.transform.localRotation = Quaternion.identity;

                randowPart = Random.Range(0, espadaCabo.Count);
                GameObject novoCaboEspada = Instantiate(espadaCabo[randowPart], infoItem.cabo.position, infoItem.cabo.rotation);
                novoCaboEspada.transform.SetParent(infoItem.cabo);
                novoCaboEspada.transform.localPosition = Vector3.zero;
                novoCaboEspada.transform.localRotation = Quaternion.identity;

                randowPart = Random.Range(0, espadaLamina.Count);
                GameObject novaLaminaEspada = Instantiate(espadaLamina[randowPart], infoItem.lamina.position, infoItem.lamina.rotation);
                novaLaminaEspada.transform.SetParent(infoItem.lamina);
                novaLaminaEspada.transform.localPosition = Vector3.zero;
                novaLaminaEspada.transform.localRotation = Quaternion.identity;

                break;

            case Armas.ItemType.Staff:

                randowPart = Random.Range(0, Gema.Count);
                GameObject novaGemaStaff = Instantiate(Gema[randowPart], infoItem.gema.position, infoItem.gema.rotation);
                novaGemaStaff.transform.SetParent(infoItem.gema);
                novaGemaStaff.transform.localPosition = Vector3.zero;
                novaGemaStaff.transform.localRotation = Quaternion.identity;

                randowPart = Random.Range(0, cajadoCabo.Count);
                GameObject novoCaboCajado = Instantiate(cajadoCabo[randowPart], infoItem.cabo.position, infoItem.cabo.rotation);
                novoVfx = Instantiate(VfxArms(rarity), novoCaboCajado.transform.position, novoCaboCajado.transform.rotation);
                novoVfx.transform.SetParent(novoCaboCajado.transform);
                novoCaboCajado.transform.SetParent(infoItem.cabo);
                novoCaboCajado.transform.localPosition = Vector3.zero;
                novoCaboCajado.transform.localRotation = Quaternion.identity;

                break;

            case Armas.ItemType.Hammer:

                randowPart = Random.Range(0, Gema.Count);
                GameObject novaGemaMartelo = Instantiate(Gema[randowPart], infoItem.gema.position, infoItem.gema.rotation);
                novaGemaMartelo.transform.SetParent(infoItem.gema);
                novaGemaMartelo.transform.localPosition = Vector3.zero;
                novaGemaMartelo.transform.localRotation = Quaternion.identity;

                randowPart = Random.Range(0, marteloCabo.Count);
                GameObject novoCaboMartelo = Instantiate(marteloCabo[randowPart], infoItem.cabo.position, infoItem.cabo.rotation);
                novoVfx = Instantiate(VfxArms(rarity), novoCaboMartelo.transform.position, novoCaboMartelo.transform.rotation);
                novoVfx.transform.SetParent(novoCaboMartelo.transform);
                novoCaboMartelo.transform.SetParent(infoItem.cabo);
                novoCaboMartelo.transform.localPosition = Vector3.zero;
                novoCaboMartelo.transform.localRotation = Quaternion.identity;

                break;
        }
    }
    private GameObject VfxArms(Armas.Rarity rarity)
    {
        
        GameObject vfx = vfxObject[(int)rarity];
            
        return vfx;
    }
}

