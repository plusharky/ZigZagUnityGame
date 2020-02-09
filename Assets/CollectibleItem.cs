using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CollectibleItem : MonoBehaviour
{
    public Text ScoreLabel;
    // Start is called before the first frame update
    void Start()
    {
        //ScoreLabel = UIBehavio
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    void OnTriggerEnter(Collider other) //подбор "кристаллов"
    {       
        Destroy(this.gameObject); //удаление при сталкновении 
        
       ScoreLabel.text = (int.Parse(ScoreLabel.text.ToString()) + 1).ToString(); //подсчёт очков 
    }
}
