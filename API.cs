using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class API : MonoBehaviour
{

    public GameObject userInfoBox;
    public GameObject userInfoFab;

    void Start()
    {
        StartCoroutine(GetRequest("http://localhost/sqlconnect/indes.php"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                    string rawresponse = webRequest.downloadHandler.text;

                    // ������� �� ������ ��������
                    rawresponse = rawresponse.Trim();

                    string[] users = rawresponse.Split('*');

                    HashSet<string> printedUsers = new HashSet<string>(); // ��������� ��� ������������ ��� ���������� �������������

                    foreach (string user in users)
                    {
                        // ���������� ������ ������
                        if (string.IsNullOrWhiteSpace(user))
                            continue;

                        string[] userinfo = user.Split(',');

                        if (userinfo.Length >= 2) // ���������, ��� � ��� ���������� ������ ��� username � password
                        {
                            // ��������� ���������� ������ ��� ������������
                            string userString = userinfo[0] + "," + userinfo[1];

                            if (!printedUsers.Contains(userString))
                            {
                                // ���� ����� ������������ ��� �� ��� �������, ������� ���������� � ���
                                Debug.Log("Name: " + userinfo[0] + " Password: " + userinfo[1]);
                                printedUsers.Add(userString);

                                GameObject gob = (GameObject)Instantiate(userInfoFab);
                                gob.transform.SetParent(userInfoBox.transform);
                                gob.GetComponent<UserInfo>().userName.text = userinfo[0];
                                gob.GetComponent<UserInfo>().userPassword.text = userinfo[1];
                            }
                        }
                        else
                        {
                            Debug.LogError("Invalid user data format: " + user);
                        }
                    }





                    break;
            }
        }
    }
}
