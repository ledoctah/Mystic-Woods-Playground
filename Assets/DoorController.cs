using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
  public string sceneToLoad;

  bool isInteractionEnabled;
  bool isInteracting;

  private void FixedUpdate()
  {
    if(isInteractionEnabled && isInteracting)
    {
      LoadScene();
    }

    isInteracting = false;
  }

  void OnInteract()
  {
    isInteracting = true;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if(other.tag == "Player")
    {
      isInteractionEnabled = true;

    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if(other.tag == "Player")
    {
      isInteractionEnabled = false;
    }
  }

  private void LoadScene()
  {
    SceneManager.LoadScene(sceneToLoad);
  }
}
