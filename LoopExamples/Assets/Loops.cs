using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loops : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        while (x <= 10) {
            if ( x < 10){
                print("X is equal to " + x + " and is less than 10");
            }
            else if (x == 10) {
                print("X is now equal to 10 (" + x + ")");
            }
            x++;
        }

        //For loops
        for (int i = 0; i<5; i++) {
            print("FOR Loop, i = " + i);
        }

        //For Each Loops
        string str = "Hello";
        foreach( char chr in str) {
            print(chr);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
