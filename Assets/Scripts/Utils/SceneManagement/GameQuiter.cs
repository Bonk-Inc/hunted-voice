using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeppaSquad.Utils
{
    public class GameQuiter : MonoBehaviour
    {
        /// <summary>
        /// Closes the game
        /// </summary>
        public void QuitGame(){
            Application.Quit();
        }
    }
}
