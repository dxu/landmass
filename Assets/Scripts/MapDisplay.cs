using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{

  public Renderer textureRender;

  public void DrawTexture(Texture2D texture)
  {
    
    // texture.material is only rendered at runtime

    textureRender.sharedMaterial.mainTexture = texture;
    
    textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);


  }
}
