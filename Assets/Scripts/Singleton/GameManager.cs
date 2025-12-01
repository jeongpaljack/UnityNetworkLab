using UnityEngine;
using System;
using UnityEngine.SceneManagement;


namespace Chapter.Singleton
{
    public class GameManager : Singleton<GameManager>
    {
        private DateTime _sessionStartTime;
        private DateTime _sessionEndTime;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _sessionStartTime = DateTime.Now;
            Debug.Log("Game session start @:" + DateTime.Now);
        }

        // Update is called once per frame
        void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;
            TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionStartTime);

            Debug.Log("Game session ended @: "+ DateTime.Now);
            Debug.Log("Game session lasted @: "+  timeDifference);
        }

        void OnGUI()
        {
            if(GUILayout.Button("Next Scene"))
            {
                SceneManager.LoadScene(
                    SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        
    }

}
