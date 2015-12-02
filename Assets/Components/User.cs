using UnityEngine;
using System.Collections.Generic;
using System;
// Author: Yvette Kim
// Date: 12/1/2015

[Serializable]
public class User
{
    private string id; // 565e6069e4b030fba33df683
    private string username; // kinghusky
    private string password; // password
    private string major; // Political Science
    private string year; // second year

    public User()
    {
        this.username = String.Empty;
        this.password = String.Empty;
        this.major = String.Empty;
        this.year = String.Empty;
    }

    public string Id
    {
        get
        {
            return this.id;
        }
        set
        {
            id = value;
        }
    }

    public string Username
    {
        get
        {
            return this.username;
        }
        set
        {
            username = value;
        }
    }

    public string Password
    {
        get
        {
            return this.password;
        }
        set
        {
            password = value;
        }
    }

    public string Major
    {
        get
        {
            return this.major;
        }
        set
        {
            major = value;
        }
    }

    public string Year
    {
        get
        {
            return this.year;
        }
        set
        {
            year = value;
        }
    }
}
