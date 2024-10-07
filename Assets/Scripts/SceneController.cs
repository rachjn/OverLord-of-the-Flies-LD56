using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   void OnEnable()
   {
      SceneManager.sceneLoaded += GameManager.Instance.OnWinScreen;
      SceneManager.LoadScene("EndScene");
   }
}
