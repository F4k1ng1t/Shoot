using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    private int _itemsCollected = 0;
    public string labelText = "Collect all 3 keys to unlock the gate and escape!";
    public int maxKeys = 3;
    public bool showWinScreen = false;
    public int Items
    {
        get { return _itemsCollected; }

        set 
        { 
            _itemsCollected = value; 
            Debug.LogFormat("Items: {0}", _itemsCollected); 
            if (_itemsCollected >= maxKeys)
            {
                labelText = "You've found all the keys! Go unlock the gate!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Key found, only " + (maxKeys - _itemsCollected) + " more to go.";
            }
        }
        

    }

    private int _playerHP = 10;
    public int HP
    {
        get { return _playerHP; }

        set
        {
            _playerHP = value;
            Debug.LogFormat("Life: {0}", _playerHP);
        }
    }
    private void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health:" + _playerHP);
        GUI.Box(new Rect(20,50,150,25), "Keys Collected:" + _itemsCollected);
        GUI.Label(new Rect(Screen.width / 2, Screen.height - 50,300,50), labelText);
        if(showWinScreen) {
            Cursor.lockState = CursorLockMode.Confined;
            if (GUI.Button(new Rect(Screen.width/2 - 100,Screen.height/2 - 50,200,100),"You Won! (Not really but whatever)"))
            {
                SceneManager.LoadScene(0);
                Time.timeScale = 1f;

            }
        }
    }
}
