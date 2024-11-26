using Meta.WitAi.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
	//��J�n�x�s��SaveData
	public virtual void Save(object save, string fileName)
	{
		// �]�w�ɮצW��
		//string fileName = string.Format("{0}_GameData", ID);
		// �N��ƧǦC�Ƭ� JSON �榡
		string savingString = JsonUtility.ToJson(save);
		// �ˬd��Ƨ��O�_�s�b�A���s�b�h�Ы�
		if (System.IO.Directory.Exists(Application.dataPath + "/StreamingAssets/GameData/") == false)
		{
			System.IO.Directory.CreateDirectory(Application.dataPath + "/StreamingAssets/GameData/");
		}
		// �N��Ƽg�J����w���|�� JSON �ɮפ�
		System.IO.File.WriteAllText(Application.dataPath + "/StreamingAssets/GameData/" + fileName, savingString);
	}
}
