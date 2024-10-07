using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   void OnEnable()
   {
        SceneManager.LoadScene("EndScene");
   }
}
