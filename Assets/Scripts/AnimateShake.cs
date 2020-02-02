using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateShake : MonoBehaviour
{
    void Update()
    {
        iTween.RotateBy(gameObject, 
            iTween.Hash(
                "z", -0.3f, 
                "loopType", "pingPong", 
                "time", 0.2f,
                "easeType", iTween.EaseType.easeInOutBounce
                ));
    }
}
