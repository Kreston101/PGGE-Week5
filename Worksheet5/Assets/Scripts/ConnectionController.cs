using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PGGE.MultiPlayer
{
  public class ConnectionController : MonoBehaviourPunCallbacks
  {
    [SerializeField]
    InputField InputName;
    [SerializeField]
    Button ButtonJoinRoom;
    [SerializeField]
    Text TextConnectionStatus;
        //other menu buttons that need to be deactivated
        [SerializeField]
        Button ButtonSelectChar1;
        [SerializeField]
        Button ButtonSelectChar2;
        [SerializeField]
        Button ButtonSelectRand;

        [SerializeField]
    int version;

    private bool mIsConnecting = false;

    [SerializeField]
    byte maxPlayersPerRoom = 5;

    void Awake()
    {
      // this makes sure we can use PhotonNetwork.LoadLevel() on 
      // the master client and all clients in the same 
      // room sync their level automatically.
      PhotonNetwork.AutomaticallySyncScene = true;
      ButtonJoinRoom.onClick.AddListener(
        delegate
        {
          OnClick_JoinButton();
        });
            //Removed and shifted to CharSelector as part of refactoring
            //ButtonSelectChar1.onClick.AddListener(ChooseCharOne);
            //ButtonSelectChar2.onClick.AddListener(ChooseCharTwo);
            //ButtonSelectRand.onClick.AddListener(ChooseRandChar);
    }

    public void OnClick_JoinButton()
    {
      PhotonNetwork.NickName = InputName.text;

      InputName.gameObject.SetActive(false);
      ButtonJoinRoom.gameObject.SetActive(false);
      TextConnectionStatus.gameObject.SetActive(true);
            //buttons added below so that they are deactivated when the othe menu buttons are disabled
            ButtonSelectChar1.gameObject.SetActive(false);
            ButtonSelectChar2.gameObject.SetActive(false);
            ButtonSelectRand.gameObject.SetActive(false);

      PhotonNetwork.GameVersion = Application.version;

      // we check if we are connected or not, we join if we are, 
      // else we initiate the connection to the server.
      if (PhotonNetwork.IsConnected)
      {
        // Attempt joining a random Room. 
        // If it fails, we'll get notified in 
        // OnJoinRandomFailed() and we'll create one.
        PhotonNetwork.JoinRandomRoom();
      }
      else
      {
        // Connect to Photon Online Server.
        mIsConnecting = PhotonNetwork.ConnectUsingSettings();
      }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
      Debug.Log("OnJoinRandomFailed() was called by PUN. " +
          "No random room available" +
          ", so we create one by Calling: " +
          "PhotonNetwork.CreateRoom");
      //Debug.Log("No room. You cannot play!");

      // Failed to join a random room.
      // This may happen if no room exists or 
      // they are all full. In either case, we create a new room.
      PhotonNetwork.CreateRoom(null,
          new RoomOptions
          {
            MaxPlayers = maxPlayersPerRoom
          });
    }
    public override void OnConnectedToMaster()
    {
      if (mIsConnecting)
      {
        Debug.Log("OnConnectedToMaster() was called by PUN");
        PhotonNetwork.JoinRandomRoom();
      }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
      Debug.Log("OnDisconnected() was called by PUN with reason " + cause);
      mIsConnecting = false;
    }
    public override void OnJoinedRoom()
    {
      Debug.Log("OnJoinedRoom() called by PUN. Client is in a room.");
      if (PhotonNetwork.IsMasterClient)
      {
        Debug.Log("We load the default room for multiplayer");

        //SceneManager.LoadScene("MultiPlayerScene1");

        //// Here we use Photon's mechanism of loading a scene
        //// instead of using Unity's method.
        //Dictionary<string, string> rooms = new Dictionary<string, string>();
        //rooms.Add("Romato", "SingaporeSuntecMap");
        //rooms.Add("Tsang", "MarinaBaySandsMap");

        PhotonNetwork.LoadLevel("MultiPlayerScene1");
      }
    }
        //Removed and shifted to CharSelector as part of refactoring
        /*public void ChooseCharOne()
        {
            chosenCharNum = 0;
            FindObjectOfType<GameApp>().passInt(chosenCharNum);
        }
        public void ChooseCharTwo()
        {
            chosenCharNum = 1;
            FindObjectOfType<GameApp>().passInt(chosenCharNum);
        }
        public void ChooseRandChar()
        {
            chosenCharNum = Random.Range(0, 2);
            FindObjectOfType<GameApp>().passInt(chosenCharNum);
        }*/
    }
}