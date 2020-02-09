using System;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    LinkedList<GameObject> tile = new LinkedList<GameObject>(); //двусвязный список для тайлов
    Stack<int> X = new Stack<int>(); // координата Х дальнего тайла
    Stack<int> Z = new Stack<int>(); // координата Z дальнего тайла
    GameObject Player; // игрок
    GameObject Way; // первый тайл
    GameObject CloneWay; // клонироване  тайла
    GameObject Capsule; // "кристалл" который собирает игрок
    // Start is called before the first frame update
    void Start()
    {
        Capsule = GameObject.Find("Capsule");
        Way = GameObject.Find("Way");
        Player = GameObject.Find("Player");
        CreateWay(Way); // создаём првый тайл и записываем в список первый элимент
        X.Push(1); Z.Push(1); //начальные координаты
    }

    // Update is called once per frame
    void Update()
    {
        double PlayerX = Player.transform.position.x; //получем координаты Х игрока
        double PlayerZ = Player.transform.position.z; //получем координаты Z игрока
        if (X.Peek() - Math.Truncate(PlayerX) < 7) // если разница в Х между дальним тайлом и игроком
        {
            X.Push(X.Peek() + 1);
            CreateWay(tile.Last.Value); //создаём новый тайл
        }
        if (Z.Peek() - Math.Truncate(PlayerZ) < 7) // если разница в Z между дальним тайлом и игроком
        {
            Z.Push(Z.Peek() + 1);
            CreateWay(tile.Last.Value); //создаём новый тайл
        }
        if (Math.Truncate(PlayerX) - tile.First.Value.transform.position.x > 1
            || Math.Truncate(PlayerZ) - tile.First.Value.transform.position.z > 1) // удаляем тайлы позади
        {
            Delete(); //метод удаления и анимации
        }
    }
    public void CreateWay(GameObject last)
    {
        float tileX = last.transform.position.x; //координаты тайла
        float tileZ = last.transform.position.z;
        Vector3 position = new Vector3(0, 0, 0); 
        System.Random rnd = new System.Random(); 
        switch (rnd.Next(0, 2)) // для случайного построения тайлов  
        {
            case 0: position = new Vector3(tileX, 0, tileZ + 1.0f); break;
            case 1: position = new Vector3(tileX + 1.0f, 0, tileZ); break;
        }
        CloneWay = Instantiate(last, position, Quaternion.identity); //строим новый тайл
        tile.AddLast(CloneWay); //записываем в двусвязный список
        switch (rnd.Next(0, 6)) // строим "кристаллы" поверх тайлов с вероятностью 20%
        {
            case 0: Instantiate(Capsule, new Vector3(position.x, 0.83f, position.z), Quaternion.identity); break;
        }
    }
    public void Delete()
    {
        tile.First.Value.transform.Translate(0, -1f, 0); //анимация падения
        if (tile.First.Value.transform.position.y < -3f) //удаление упавшего тайла
        {
            Destroy(tile.First.Value);
            tile.RemoveFirst();
        }
    }
}
