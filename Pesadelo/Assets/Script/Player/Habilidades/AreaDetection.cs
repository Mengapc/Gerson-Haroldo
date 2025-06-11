using UnityEngine;

public class AreaDetection : MonoBehaviour // Nome do script que est� na sua 'instanceArea'
{
    // Esta vari�vel (flag) vai nos dizer se o inimigo est� dentro ou n�o.
    public bool inimigoEstaNaArea { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("basic_enemy"))
        {
            Debug.Log("Inimigo entrou. A flag agora � TRUE.");
            inimigoEstaNaArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("basic_enemy"))
        {
            Debug.Log("Inimigo saiu. A flag agora � FALSE.");
            inimigoEstaNaArea = false;
        }
    }

}