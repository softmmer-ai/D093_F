using UnityEngine;
using UnityEngine.SceneManagement;
public class menuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLeveldeJogo;
    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoLeveldeJogo);
    }
    public void Sair()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }
}
