using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks
{
  // We will instantiate the prefab using the name
  // of the prefab.
  public string[] mPlayerPrefabName;

  // We keep a reference to the spawnpoints component.
  // This is required to spawn our player at runtime.
  public PlayerSpawnPoints mSpawnPoints;

    //Get the number referring to the character model the player
    //chose from the multiplayer launcher
    //0 as your hold varibel so stuff dun break
    public int chosenModelNumber;

  // This is the game object created from the prefab name.
  private GameObject mPlayerGameObject;

  // We will create out third-person camera from
  // this script and bind it to the camera at runtime.
  private ThirdPersonCamera mThirdPersonCamera;

  private void Start()
  {
        //USE THIS LATER WHEN YOU CAN PASS THE NUMBERS, IF NOT RELY ON THE RANDOMIZER
        /*if (chosenModelNumber == null)
        {
            chosenModelNumber = Random.Range(0, 2);
            Debug.Log(chosenModelNumber);
            CreatePlayer(chosenModelNumber);
        }
        else
        {
            CreatePlayer(chosenModelNumber);
        }*/

        chosenModelNumber = Random.Range(0, 2);
        Debug.Log(chosenModelNumber);
        CreatePlayer(chosenModelNumber);
  }


  public void CreatePlayer(int playerModel)
  {
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
