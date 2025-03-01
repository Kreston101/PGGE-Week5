using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE.Patterns;
using UnityEngine.SceneManagement;

public class GameApp : Singleton<GameApp>
{
    public int gameAppIntToPass;

  private bool mPause = false;

  /// <summary>
  /// Function to pause the game.
  /// I have refactored the original property and 
  /// instead imlemented this function.
  /// </summary>
  /// <param name="flag"></param>
  public void PauseGame(bool flag)
  {
    mPause = flag;
    //mOnPause?.Invoke(GamePaused);
    if (mPause)
    {
      Time.timeScale = 0;
    }
    else
    {
      Time.timeScale = 1;
    }
  }

  //public bool GamePaused
  //{
  //  get { return mPause; }
  //  set
  //  {
  //    mPause = value;
  //    //mOnPause?.Invoke(GamePaused);
  //    if (mPause)
  //    {
  //      Time.timeScale = 0;
  //    }
  //    else
  //    {
  //      Time.timeScale = 1;
  //    }
  //  }
  //}

  // Start is called before the first frame update

  void Start()
  {
    SceneManager.LoadScene("Menu");
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      PauseGame(!mPause);
    }
  }

    //function called from CharSelector
    //recives and stores the number of the
    //chosen player model
    public void passInt(int passThis)
    {
        gameAppIntToPass = passThis;
    }
    //functioned called to send the integer to
    //the PlayerManager
    public int sendInt()
    {
        return gameAppIntToPass;
    }
}
