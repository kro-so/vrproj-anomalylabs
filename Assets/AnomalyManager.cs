using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.VRTemplate
{
    /// <summary>
    /// Controls the anomalies in the in anomalies card.
    /// </summary>
    public class AnomalyManager : MonoBehaviour
    {
        public GameManager gamemanager;
        public GameObject elevatordoors;
        private Animator anim;
        
        [Serializable]
        class Anomaly
        {
            [SerializeField]
            public GameObject anomalyObject;
        }

        [SerializeField]
        List<Anomaly> m_AnomalyList = new List<Anomaly>();

        int m_CurrentAnomalyIndex = 1;
        System.Random rnd = new System.Random();

        public void Next()
        {
            /* DESCRIPTION:
             * Set current index element false, randomly select an index between 0-AnomalyList max, 
             * then set that index active */

            m_AnomalyList[m_CurrentAnomalyIndex].anomalyObject.SetActive(false);
            m_AnomalyList[1].anomalyObject.SetActive(false); // set normalHall to inactive in case last anomaly activated it as an extra obj

            if (gamemanager.currentFloor > 1)
                m_CurrentAnomalyIndex = rnd.Next(1, m_AnomalyList.Count); // range = 0 - ( AnomalyList.Count - 1 )
            else
                m_CurrentAnomalyIndex = 0;

            //Debug.Log("index " + m_CurrentAnomalyIndex);

            m_AnomalyList[m_CurrentAnomalyIndex].anomalyObject.SetActive(true);
            if (m_CurrentAnomalyIndex <= 5) // activate normalHall for anomalies that can use the same hall model 
                m_AnomalyList[1].anomalyObject.SetActive(true);
        }

        public void LateNext()
        {
            Invoke("Next", (float)1.1);
        }

        public void CheckInput(string name)
        {
            /* Description:
             * Buttons call this function and pass their name as a string. 
             * Function checks the name against the current anomoly index. If incorrect, reset current floor number, increment countNumber.
             * Otherwise, increment both countNumber and decrement floor number
             */

            gamemanager.SwapButtonInteract(); //disable buttons after being pressed (prevents users from spamming buttons to win)

            if ((name.Equals("UP") && m_CurrentAnomalyIndex == 1) || (name.Equals("DOWN") && m_CurrentAnomalyIndex > 1))
            {
                Debug.Log("WRONG! Name: " + name + " Index: " + m_CurrentAnomalyIndex);
                gamemanager.countNumber++;
                //gamemanager.countText.SetText(gamemanager.countNumber.ToString());
                gamemanager.Reset();
            }
            else
            {
                //Debug.Log("Correct! Name: " + name + " Index: " + m_CurrentAnomalyIndex);
                gamemanager.Count();
            }

            PlayAnimationDoors();
            Invoke("Next", (float) 1.1); //delay "scene change" for when the doors are closed
            if(gamemanager.currentFloor > 1) //renable buttons if game is not yet won
                gamemanager.Invoke("SwapButtonInteract", (float)3);
        }

        public void PlayAnimationDoors()
        {
            /* Description:
             * Call this function to play the elevator doors' closing and opening animation */

            anim = elevatordoors.GetComponent<Animator>();
            anim.Play("ElevatorOpen");

        }
    }
    }


