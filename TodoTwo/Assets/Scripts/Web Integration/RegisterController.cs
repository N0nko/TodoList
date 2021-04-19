using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class RegisterController : MonoBehaviour
{
    public TMP_InputField firstNameObject, lastNameObject, passwordObject, passwordRepeatObject, emailObject;

    class RegisterForm
    {
        public string username;
        public string password;
        public string email;

        public RegisterForm(string u, string psw, string em)
        {
            this.username = u;
            this.password = psw;
            this.email = em;
        }
    }

    public void Register()
    {
        string username = firstNameObject.text + " " + lastNameObject.text;
        string password = passwordObject.text;
        string email = emailObject.text;

        var register = new RegisterForm(username, password, email);
        var f = JsonUtility.ToJson(register);
        var bytes = System.Text.Encoding.UTF8.GetBytes(f);

        StartCoroutine(RequestController.PostRequest("authentication/register", bytes, ""));
    }
}
