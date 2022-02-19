using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PGGE.MultiPlayer;

public class PlayerManager : MonoBehaviourPunCallbacks
{
  // We will instantiate the prefab using the name
  // of the prefab.
  //converted varible to an array to take in both
  //player models
  public string[] mPlayerPrefabName;

  // We keep a reference to the spawnpoints component.
  // This is required to spawn our player at runtime.
  public PlayerSpawnPoints mSpawnPoints;

    //int that will store the number of the selected
    //player model from GameApp
    public int chosenModelNumber;

  // This is the game object created from the prefab name.
  private GameObject mPlayerGameObject;

  // We will create out third-person camera from
  // this script and bind it to the camera at runtime.
  private ThirdPersonCamera mThirdPersonCamera;

  private void Start()
  {
        //get the number from GameApp, can be either 0 or 1
        //slightly modified the player create function to
        //take in a number and access the array of player models
        chosenModelNumber = FindObjectOfType<GameApp>().sendInt();
        Debug.Log(chosenModelNumber);
        CreatePlayer(chosenModelNumber);
  }


  public void CreatePlayer(int playerModel)
  {
        //instantiates the model that the player chose
    mPlayerGameObject = PhotonNetwork.Instantiate(mPlayerPrefabName[playerModel],
        mSpawnPoints.GetSpawnPoint().position,
        mSpawnPoints.GetSpawnPoint().rotation,
        0);

    mPlayerGameObject.GetComponent<PlayerMovement>().mFollowCameraForward = false;
    mThirdPersonCamera = Camera.main.gameObject.AddComponent<ThirdPersonCamera>();
    mThirdPersonCamera.mPlayer = mPlayerGameObject.transform;
    mThirdPersonCamera.mDamping = 5.0f;
    mThirdPersonCamera.mCameraType = CameraType.Follow_Track_Pos_Rot;
  }
  public void OnClick_LeaveRoom()
  {
    Debug.LogFormat("LeaveRoom");
    PhotonNetwork.LeaveRoom();
  }
  public override void OnLeftRoom()
  {
    Debug.LogFormat("OnLeftRoom()");
    SceneManager.LoadScene("Menu");
  }
}
