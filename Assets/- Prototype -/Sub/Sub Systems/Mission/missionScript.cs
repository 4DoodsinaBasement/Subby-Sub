using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class missionScript : MonoBehaviour
{
	public GameObject MineCount;
	public Text DescriptionText;
	public Text ValueText;
	bool missionComplete = false;
	// Start is called before the first frame update
	void Start()
	{
		DescriptionText.text = "Destroy All Mines!";
	}

	// Update is called once per frame
	void Update()
	{
		if (MineCount.transform.childCount > 0)
		{
			ValueText.text = MineCount.transform.childCount.ToString();
		}
		else
		{
			if (missionComplete == false)
			{
				DescriptionText.text = "Complete!";
				ValueText.text = string.Format("{0} s", Time.time);
				missionComplete = true;
			}
		}
	}
}
