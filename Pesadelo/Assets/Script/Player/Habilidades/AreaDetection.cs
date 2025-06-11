using UnityEngine;

public class AreaDetection : MonoBehaviour // Nome do script que está na sua 'instanceArea'
{
    // Esta variável (flag) vai nos dizer se o inimigo está dentro ou não.
    public bool inimigoEstaNaArea { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("basic_enemy"))
        {
            Debug.Log("Inimigo entrou. A flag agora é TRUE.");
            inimigoEstaNaArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("basic_enemy"))
        {
            Debug.Log("Inimigo saiu. A flag agora é FALSE.");
            inimigoEstaNaArea = false;
        }
    }

}