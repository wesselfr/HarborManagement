using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class PipeServerTest : MonoBehaviour {

    TcpListener _ServerSocket;
    TcpClient _ClientSocket;

	// Use this for initialization
	void Start () {
        
        _ServerSocket = new TcpListener(8888);
        TcpClient _ClientSocket = default(TcpClient);
        _ServerSocket.Start();
        Debug.Log(">>Server Started");
        _ClientSocket = _ServerSocket.AcceptTcpClient();


	}
	
	// Update is called once per frame
	void Update () {
        try
        {
            NetworkStream networkStream = _ClientSocket.GetStream();
            byte[] bytesFrom = new byte[10025];
            networkStream.Read(bytesFrom, 0, (int)_ClientSocket.ReceiveBufferSize);
            string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
            dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
            Debug.Log(" >> Data from client - " + dataFromClient);
            string serverResponse = "Last Message from client" + dataFromClient;
            byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
            Debug.Log(" >> " + serverResponse);
        }
        catch
        {

        }
	}

    private void OnApplicationQuit()
    {
        _ClientSocket.Close();
        _ServerSocket.Stop();
        Debug.Log(">>Exit");
    }
}
