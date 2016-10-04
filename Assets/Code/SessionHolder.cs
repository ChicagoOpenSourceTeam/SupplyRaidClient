using System;
using System.Net;

public static class SessionHolder
{
	public static string sessionCookie { get; set;}
	public static CookieContainer cookieContainer = new CookieContainer();
}

