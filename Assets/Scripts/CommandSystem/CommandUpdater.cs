using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH
{ 
    public class CommandUpdater : MonoBehaviour
    {
        void Update()
        {
            CommandInvoker.Update();
        }
    }
}
