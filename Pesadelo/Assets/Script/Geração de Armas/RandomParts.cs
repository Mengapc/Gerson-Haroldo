using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class RandomParts : MonoBehaviour
{
    [Header("Partes das armas")]
    public List<GameObject> Gema;
    [Header("Cajado")]
    public List<GameObject> cajadoPontaP;
    public List<GameObject> cajadoCabo;
    [Header("Espada")]
    public List<GameObject> espadaLamina;
    public List<GameObject> espadaGuardaP;
    public List<GameObject> espadaCabo;
    [Header("Martelo")]
    public List<GameObject> martelocabecaP;
    public List<GameObject> marteloCabo;

    public GameObject GeneratePrincipalPartArm(Armas.ItemType itemType)
    {
        GameObject item = null;
        switch (itemType)
        {
            case Armas.ItemType.Sword:
                if (espadaGuardaP != null && espadaGuardaP.Count > 0)
                {
                    int randowPart = Random.Range(0, espadaGuardaP.Count);
                    item = espadaGuardaP[randowPart];
                }
                else
                {
                    Debug.LogWarning("A lista espadaGuardaP está vazia ou não foi atribuída!");
                    return null;
                }
                break;
            case Armas.ItemType.Staff:
                if (cajadoPontaP != null && cajadoPontaP.Count > 0)
                {
                    int randowPart = Random.Range(0, cajadoPontaP.Count);
                    item = cajadoPontaP[randowPart];
                }
                else
                {
                    Debug.LogWarning("A lista cajadoPontaP está vazia ou não foi atribuída!");
                    return null;
                }
                break;
            case Armas.ItemType.Hammer:
                if (cajadoPontaP != null && martelocabecaP.Count > 0)
                {
                    int randowPart = Random.Range(0, martelocabecaP.Count);
                    item = martelocabecaP[randowPart];
                }
                else
                {
                    Debug.LogWarning("A lista marteloCabeçaP está vazia ou não foi atribuída!");
                    return null;
                }
                break;
            default:
                Debug.LogWarning("Tipo de item não suportado em GeneratePrincipalPartArm: " + itemType);
                return null;
        }
        return item;
    }
    public void GenerationOutherParts(Armas.ItemType itemType, GameObject newItem)
    {
        int randowPart;
        LockParts infoItem = newItem.GetComponent<LockParts>();
        switch (itemType)
        {
            case Armas.ItemType.Sword:

                randowPart = Random.Range(0, Gema.Count);
                GameObject novaGema = Instantiate(Gema[randowPart], infoItem.gema.position, infoItem.gema.rotation);
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
                novoCaboMartelo.transform.SetParent(infoItem.cabo);
                novoCaboMartelo.transform.localPosition = Vector3.zero;
                novoCaboMartelo.transform.localRotation = Quaternion.identity;

                break;
        }
    }

}
