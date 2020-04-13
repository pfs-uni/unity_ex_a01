using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeExample : MonoBehaviour
{
    public int numTimesCalled = 0;

    // Start is called before the first frame update
    void Start()
    {
        numTimesCalled++;
        PrintUpdates();
    }
    
    // Update is called once per frame
    void Update()
    {
        numTimesCalled++;
        if (numTimesCalled%10 ==0){
            PrintUpdates();
            print("Multiplied by 6 it is " + Multiplier(numTimesCalled,6));
        }
    }

    void PrintUpdates(){
        string msg = "Updates: " + numTimesCalled;
        print(msg);
    }
    
    void Awake() {
        print(Talk("Hello, Peter.."));
        print("The factorial of 6 = " + Fac(6));
    }
    
    string Talk(string message) {
        print(message);
        return (message + "blablabla");
    }

    int Multiplier(int value, int numTimes) {
        return (value * numTimes);
    }

    int Fac (int num) {
        if (num < 0) {
            return (0);
        }
        if (num == 0) {
            return(1);
        }
    
    int result = num * Fac(num-1);
    return (result);

    }
}
