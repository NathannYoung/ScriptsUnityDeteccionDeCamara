using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    GameObject suspendedObject;
    // Start is called before the first frame update
    void Start()
    {
        suspendedObject = new GameObject();
#if UNITY_WEBPLAYER || UNITY_FLASH
 yield return Application.RequestUserAuthorization(UserAuthorization.WebCam );
#endif
        suspendedObject.SetActive(true);
    }

        // Update is called once per frame
        void Update()
        {

        }
    }

