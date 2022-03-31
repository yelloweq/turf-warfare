// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class HealthTrigger : MonoBehaviour
// {
//     public BaseHealth userBase;
//     public HealthbarScript healthbar;
//     GameObject money;
//     public Text message;
//     string originalText;

//     // Start is called before the first frame update
//     void Start()
//     {
//         originalText = message.text;
//         money = GameObject.FindGameObjectWithTag("Player");
//     }
//     /* Checks if the user has enough money and doesn't have maximum health
//        if yes, subtract 500 from currency and increase base health. Otherwise
//        show an error message.*/
//     public void buyHealth()
//     {
//         if (money.GetComponent<CharacterCurrency>().getCurrency() >= 500 && userBase.health < 100)
//         {
//             if (userBase.health >= 80)
//             {
//                 userBase.health = 100;
//                 healthbar.restoreHealth();
//             }
//             else
//             {
//                 userBase.health += 20;
//                 healthbar.increaseHealth(20);
//             }
//             money.GetComponent<CharacterCurrency>().updateCurrency(-500);   //reduces currency by 500
//         }
//         else     //Otherwise it displays the message 'unavailable'
//         {
//             message.text = "Unavailable";
//             message.gameObject.SetActive(true);
//         }
//     }
// }
