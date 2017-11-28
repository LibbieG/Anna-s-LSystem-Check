using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule : MonoBehaviour {

	char a;
	string b;

	Rule(char a_, string b_) {
		a = a_;
		b = b_; 
	}

	char getA() {
		return a;
	}

	string getB() {
		return b;
	}




}
