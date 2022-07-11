using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface class = to describe method that implemented scripts need to have
public interface IDataPersistence
{
    void LoadData(GameData data);//calls any game object, called data, only can read data
    void SaveData(GameData data); //ref = so implementing scripts can modify
}
