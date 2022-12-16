using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selfdestruct : MonoBehaviour
{
   public void Destroyself()
   {
      Destroy(this.gameObject);
   }
}
