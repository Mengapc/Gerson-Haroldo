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
                Instantiate(Gema[randowPart], infoItem.gema.position, infoItem.gema.rotation);

                randowPart = Random.Range(0, espadaCabo.Count);
                Instantiate(espadaCabo[randowPart], infoItem.cabo.position, infoItem.cabo.rotation);

                randowPart = Random.Range(0, espadaLamina.Count);
                Instantiate(espadaLamina[randowPart], infoItem.lamina.position, infoItem.lamina.rotation);

                break;
            case Armas.ItemType.Staff:
                randowPart = Random.Range(0, Gema.Count);
                Instantiate(Gema[randowPart], infoItem.gema.position, infoItem.gema.rotation);

                randowPart = Random.Range(0, cajadoCabo.Count);
                Instantiate(cajadoCabo[randowPart], infoItem.cabo.position, infoItem.cabo.rotation);
                break;
            case Armas.ItemType.Hammer:
                randowPart = Random.Range(0, Gema.Count);
                Instantiate(Gema[randowPart], infoItem.gema.position, infoItem.gema.rotation);

                randowPart = Random.Range(0, marteloCabo.Count);
                Instantiate(marteloCabo[randowPart], infoItem.cabo.position, infoItem.cabo.rotation);
                break;
        }
    }
}
