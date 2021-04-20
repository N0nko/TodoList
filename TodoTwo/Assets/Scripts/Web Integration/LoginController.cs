using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LoginController : MonoBehaviour
{
    public TMP_InputField usernameObject, passwordObject;
    public GameObject todoList, mainMenu;

    public SessionController sessionController;

    public UnityEvent loginButtonClickEvent;
    public UnityEvent loginEvent;
    public class Code
    {
        public string code;
    }

    class Token
    {
        public string id;
        public string accessToken;
        public string refreshToken;
        public int accessExpiresIn;
        public int refreshExpiresIn;
        public bool user;
        public string userId;
        public string updatedAt;
        public string createdAt;
    }

    string code;
    string accessToken;

    class LoginForm
    {
        public string username;
        public string password;

        public LoginForm(string u, string psw)
        {
            this.username = u;
            this.password = psw;
        }
    }

    class TokenForm
    {
        public string grantType;
        public string code;

        public TokenForm(string g, string c)
        {
            this.grantType = g;
            this.code = c;
        }
    }

    public void Login()
    {
        string u = usernameObject.text.Trim();
        string psw = passwordObject.text.Trim();
        loginButtonClickEvent.Invoke();
        StartCoroutine(LoginEnumerator(u, psw));
    }

    public string GetAccessToken() => accessToken;
    // Start is called before the first frame update
    IEnumerator LoginEnumerator(string username, string password)
    {
        var login = new LoginForm(username, password);
        var f = JsonUtility.ToJson(login);
        var bytes = System.Text.Encoding.UTF8.GetBytes(f);
        Debug.Log(f);
        //74 65 73 74 33 e2 80 8b
        //74 65 73 74 33
       yield return RequestController.PostRequest("authentication/login", bytes, "");

        string jsonCode = RequestController.GetResponseData();

        try
        {
            code = JsonUtility.FromJson<Code>(jsonCode).code;
        }
        catch
        {
            Debug.LogWarning("Failed to login");
            yield break;
        }
        
        //WWWForm tokenForm = new WWWForm();
       // tokenForm.AddField("grantType", "authorization_code");
        //tokenForm.AddField("code", code);
        TokenForm t = new TokenForm("authorization_code", code);
        var tok = JsonUtility.ToJson(t);
        var tokenBytes = System.Text.Encoding.UTF8.GetBytes(tok);
        yield return RequestController.PostRequest("token", tokenBytes, "");

        string jsonToken = RequestController.GetResponseData();
        accessToken = JsonUtility.FromJson<Token>(jsonToken).accessToken;

        //todoList.SetActive(true);
        //mainMenu.SetActive(false);
        SessionController.SetAccessToken(accessToken);


        yield return new WaitForSeconds(0.5f);
        yield return sessionController.GetLists();
        UISystem.instance.GenerateTaskLists();
        yield return sessionController.GetAllTasks();
        UISystem.instance.MapTasksToLists();

        loginEvent.Invoke();
    }
}


