using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    private int _itemsCollected = 0;
    

    public int Items
    {
        get { return _itemsCollected; }

        set 
        { 
            _itemsCollected = value; 
            Debug.LogFormat("Items: {0}", _itemsCollected); 
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
}
