using System.IO;
using UnityEngine;

public class LogsManager {

	/**
	* Attributes
	*/
	string fileName = "log_1";
	string filePath;
	string filePathDefault;

	long fileLengthMax = 100000;
	int filesMax = 3;

	/**
	* Personal methods
	*/
	public LogsManager() {
		filePathDefault = Application.dataPath + "/Logs/";
		filePath = filePathDefault + fileName + ".txt";

		Debug.Log(filePathDefault);
	}

	public void Log_debug(string TAG, string method, string message) {
		string textLog = System.DateTime.Now.ToUniversalTime() + " | DEBUG | " + TAG + "-->" + method + "() | " + "Message : \"" + message + "\".";
		Debug.Log(textLog);

		WriteFile(textLog);
	}

	public void Log_info(string TAG, string method, string message) {
		string textLog = System.DateTime.Now.ToUniversalTime() + " | INFO  | " + TAG + "-->" + method + "() | " + "Message : \"" + message + "\".";
		Debug.LogAssertion(textLog);

		WriteFile(textLog);
	}

	public void Log_warn(string TAG, string method, string message) {
		string textLog = System.DateTime.Now.ToUniversalTime() + " | WARN  | " + TAG + "-->" + method + "() | " + "Message : \"" + message + "\".";
		Debug.LogWarning(textLog);

		WriteFile(textLog);
	}

	public void Log_error(string TAG, string method, string message) {
		string textLog = System.DateTime.Now.ToUniversalTime() + " | ERROR | " + TAG + "-->" + method + "() | " + "Message : \"" + message + "\".";
		Debug.LogError(textLog);

		WriteFile(textLog);
	}

	private void WriteFile(string logMessage) {
		CheckFolder();
		CheckFile();

		// If the length file is greater than the max replace it (3 files max)
		if (new FileInfo(filePath).Length > fileLengthMax) {

			int fCount = Directory.GetFiles(filePathDefault, "*.txt", SearchOption.TopDirectoryOnly).Length;

			if (fCount == filesMax) {
				File.Delete(filePathDefault + "log_" + fCount + ".txt");
				fCount -= 1;
			}

			for (int i = fCount; i >= 1; i--) {
				string fileBefore = filePathDefault + "log_" + i + ".txt";
				string fileAfter = filePathDefault + "log_" + (i + 1) + ".txt";

				File.Move(fileBefore, fileAfter);
			}

			CheckFile();
		}

		StreamWriter sw = new StreamWriter(filePath, true);
		sw.WriteLine(logMessage);
		sw.Close();
	}

	private void CheckFolder() {
		// if the folder don't existe --> create it
		if (!Directory.Exists(filePathDefault)) {
			Debug.Log("Logs folder don't exists.");

			var folder = Directory.CreateDirectory(filePathDefault);
			Debug.Log("Folder " + folder.Name + " created at " + folder.CreationTimeUtc + ".");

		}
	}

	private void CheckFile() {
		// if the file don't existe --> create it
		if (!File.Exists(filePath)) {
			Debug.Log(fileName + " don't exists.");

			var file = File.Create(filePath);
			file.Dispose();
			Debug.Log("Folder " + file.Name + " created.");
		}
	}
}
