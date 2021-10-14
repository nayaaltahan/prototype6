
using System;
using MLAPI;
using UnityEngine;
using UnityEngine.SceneManagement;

using MLAPI.Transports.UNET;
using UnityEngine.UI;

    public class ConnectivityManager : MonoBehaviour
    {
        public InputField inputField;
        public GameObject panel;

        public void StartElephant()
        {
            SetIPAddress();
            NetworkManager.Singleton.StartClient();
            Information.type = Type.Elephant;
            panel.SetActive(false);
        }
        public void StartMonkey(){
            SetIPAddress();
            NetworkManager.Singleton.StartClient();
            Information.type = Type.Monkey;
            panel.SetActive(false);
        }
        public void StartServer(){
            SetIPAddress();
            NetworkManager.Singleton.StartServer();
            Information.type = Type.Server;
            panel.SetActive(false);
        }

        private void SetIPAddress()
        {
            if (inputField.text.Length <= 0)
            {
               NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = "10.26.28.225";
               // NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = "127.0.0.1";
            }
            else
            {
                Debug.Log(inputField.text);
                NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = inputField.text;
            }

            Debug.Log(NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress);
        }
        
    
}